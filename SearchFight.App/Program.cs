using SearchFight.Business.Search;
using SearchFight.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using static SearchFight.Core.Enumerations.Enumerations;

namespace SearchFight.App
{
    class Program
    {
        static void Main(string[] args)
        {
            int keywordId = 0;
            string textResult = string.Empty;
            //Validate input parameters
            if (args.Length == 0)
            {
                Console.WriteLine("Please enter at least one seach criteria");
                return;
            }
            //Call Search Manager to proceed with searching
            SearchManager searchEngineManager = SearchManager.Instance;
            SearchResult searchResult = searchEngineManager.Search(args.ToList());
            
            //Validate status
            if (searchResult.Status == (short)ProcessStatus.Error)
            {
                Console.WriteLine("An error occurred during the execution");
                Console.WriteLine("Error Message: " + searchResult.ErrorMessage);
                return;
            }

            //Get search results
            List<SearchEngineQuery> results = searchResult.SearchEngineResults;
                        
            //Show seach results by search engine
            for (int i = 0; i < results.Count; i++)
            {
                if (results[i].KeywordId == keywordId)
                {
                    textResult += " " + results[i].SearchEngine + 
                        ": " + results[i].TotalResults.ToString();
                }
                else
                {
                    if (keywordId != 0)
                    { Console.WriteLine(textResult); }
                    textResult = results[i].Keyword + ": " +
                        results[i].SearchEngine + ":" +
                        results[i].TotalResults.ToString();
                    
                    keywordId = results[i].KeywordId;
                }
                if (i == (results.Count - 1))
                { Console.WriteLine(textResult); }
            }
            //Show winner(s) for each search engine
            searchResult.KeywordResults.ForEach(x => Console.WriteLine(x.SearchEngine + " winner(s): " + x.KeywordWinners));
            //Show total winners(s)
            Console.WriteLine("Total Winner(s): " + searchResult.TotalWinners);
        }
    }
}
