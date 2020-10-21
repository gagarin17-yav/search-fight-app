using SearchFight.Core.Entities;
using System.Collections.Generic;
using System.Linq;

namespace SearchFight.Business.Utilities
{
    public static class SearchUtilities
    {
        /// <summary>
        /// Remove extra blank spaces from string value
        /// </summary>
        /// <param name="inputValue">String value</param>
        /// <returns>String</returns>
        public static string RemoveExtraBlankSpaces(string inputValue)
        {
            string[] arrValues = inputValue.Split(' ').Where(s => !string.IsNullOrWhiteSpace(s)).ToArray();
            return string.Join(" ", arrValues);
        }
         
        /// <summary>
        /// Determine if a list is empty or null
        /// </summary>
        /// <typeparam name="T">Generic</typeparam>
        /// <param name="list">List of elements of type T</param>
        /// <returns>Boolean</returns>
        public static bool IsEmptyList<T>(List<T> list)
        {
            if (list == null)
                return true;
            return !list.Any();
        }

        /// <summary>
        /// Return winner(s) for a search engine results
        /// </summary>
        /// <param name="searchEngineQueryList">Search results</param>
        /// <param name="searchEngine">Search engine name</param>
        /// <returns>String</returns>
        public static string GetWinners(List<SearchEngineQuery> searchEngineQueryList, string searchEngine)
        {
            long maxValue = 0;
            List<string> winnerList = new List<string>();

            if (!IsEmptyList(searchEngineQueryList))
            {
                maxValue  = searchEngineQueryList.Where(x => x.SearchEngine == searchEngine)
                    .Max(z => z.TotalResults);
                winnerList = searchEngineQueryList.Where(z => z.SearchEngine == searchEngine && z.TotalResults == maxValue)
                    .Select(x => x.Keyword)
                    .ToList();
            }

            return string.Join("/", winnerList.ToArray());
        }

        /// <summary>
        /// Return total winner(s) based on search results
        /// </summary>
        /// <param name="searchEngineInfolist">Search Results</param>
        /// <param name="keywordList">List of keywords</param>
        /// <returns>String</returns>
        public static string GetTotalWinners(List<SearchEngineQuery> searchEngineQueryList, List<KeywordInfo> keywordList)
        {
            List<string> totalWinnersList = new List<string>();
            List<KeywordInfo> keywordResults = new List<KeywordInfo>();
            long maxSumResults = 0;

            if (!IsEmptyList(searchEngineQueryList))
            {
                foreach (KeywordInfo item in keywordList)
                {
                    item.SumTotalResults = searchEngineQueryList.Where(x => x.KeywordId == item.Id).Sum(z => z.TotalResults);
                    if (item.SumTotalResults > maxSumResults)
                    {
                        maxSumResults = item.SumTotalResults;
                    }
                }
                totalWinnersList = keywordList.Where(x => x.SumTotalResults == maxSumResults).Select(z => z.Keyword).ToList();
            }

            return string.Join("/",totalWinnersList.ToArray());
        }
    }
}
