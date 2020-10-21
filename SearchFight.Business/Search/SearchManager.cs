using SearchFight.Business.Utilities;
using SearchFight.Core.Entities;
using SearchFight.Core.Enumerations;
using SearchFight.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using static SearchFight.Core.Enumerations.Enumerations;

namespace SearchFight.Business.Search
{
    public class SearchManager
    {
        private static readonly Lazy<SearchManager> _instance = new Lazy<SearchManager>(() => new SearchManager(), LazyThreadSafetyMode.PublicationOnly);
        private readonly List<SearchEngineInfo> _searchEngines;
        public static SearchManager Instance
        {
            get { return _instance.Value; }
        }
        public SearchManager()
        {
            //Create search engine list
            _searchEngines = new List<SearchEngineInfo>
            {
                new SearchEngineInfo { Id = (int)Enumerations.SearchEngine.Google, Name = nameof(Enumerations.SearchEngine.Google) },
                new SearchEngineInfo { Id = (int)Enumerations.SearchEngine.Bing, Name = nameof(Enumerations.SearchEngine.Bing) }
            };
        }
        /// <summary>
        /// Perform a search using a list of keywords
        /// </summary>
        /// <param name="keywords">List of keywords</param>
        /// <returns>SearchResult object</returns>
        public SearchResult Search(List<string> keywords)
        {
            IService serviceProvider;
            string searchCriteria;
            SearchEngineQuery serviceResult;
            List<KeywordInfo> keywordList = new List<KeywordInfo>();
            List<SearchEngineQuery> collectionResults = new List<SearchEngineQuery>();
            List<EngineKeyword> collectionWinners = new List<EngineKeyword>();
            SearchResult searchResult;
            string keyWordWinners;
            string totalWinners;
            int seqKeyword = 0;
            
            //Validating keywords
            if (keywords == null || keywords.Count == 0)
            {
                //Collect validation mesage
                searchResult = new SearchResult
                {
                    Status = (short)ProcessStatus.Error,
                    ErrorMessage = "Keywords were not found to perform a search"
                };
                return searchResult;
            }
            if (keywords.FindAll(x => string.IsNullOrWhiteSpace(x)).Count > 0)
            {
                //Collect validation mesage
                searchResult = new SearchResult
                {
                    Status = (short)ProcessStatus.Error,
                    ErrorMessage = "Any keyword in the list cannot be empty or null"
                };
                return searchResult;
            }
            try
            {
                //Processing queries for each seach engine            
                foreach (string keyword in keywords)
                {
                    seqKeyword++;
                    keywordList.Add(
                        new KeywordInfo { Id = seqKeyword, Keyword = keyword }
                        );
                    searchCriteria = SearchUtilities.RemoveExtraBlankSpaces(keyword);
                    foreach (SearchEngineInfo searchEngine in _searchEngines)
                    {
                        serviceProvider = SearchFactory.GetService(searchEngine.Id);
                        serviceResult = serviceProvider.Search(searchCriteria).GetAwaiter().GetResult();
                        serviceResult.KeywordId = seqKeyword;
                        serviceResult.Keyword = keyword;
                        collectionResults.Add(serviceResult);
                    }
                }
                //Get winners for each search engine
                foreach (var item in _searchEngines)
                {
                    keyWordWinners = SearchUtilities.GetWinners(collectionResults, item.Name);
                
                    collectionWinners.Add(new EngineKeyword
                    {
                        SearchEngine = item.Name,
                        KeywordWinners = keyWordWinners
                    });
                }
                //Get total winners
                totalWinners = SearchUtilities.GetTotalWinners(collectionResults, keywordList);
                //Build output with search results and winners
                searchResult = new SearchResult
                {
                    Status = (short)ProcessStatus.Success,
                    SearchEngineResults = collectionResults,
                    KeywordResults = collectionWinners,
                    TotalWinners = totalWinners
                };
            }
            catch (Exception ex)
            {
                //Collect error information
                searchResult = new SearchResult
                {
                    Status = (short)ProcessStatus.Error,
                    ErrorMessage = ex.Message
                };
            }
            return searchResult;
        }
    }
}
