using Newtonsoft.Json;
using SearchFight.Core.Entities;
using SearchFight.Core.Interfaces;
using SearchFight.Core.Properties;
using SearchFight.Core.Responses;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using static SearchFight.Core.Enumerations.Enumerations;

namespace SearchFight.Core.Services
{
    public class BingService : IService
    {
        private static readonly Lazy<BingService> _instance = new Lazy<BingService>(() => new BingService(), LazyThreadSafetyMode.PublicationOnly);

        public static BingService Instance
        {
            get { return _instance.Value; }
        }
        public BingService()
        {

        }

        /// <summary>
        /// Perform a Bing search based on a search criteria
        /// </summary>
        /// <param name="searchCriteria">keyword</param>
        /// <returns>SearchEngineQuery object</returns>
        public async Task<SearchEngineQuery> Search(string searchCriteria)
        {
            string urlBase = Settings.Default.BingURLBase;
            string customConfigurationId = Settings.Default.BingCustomConfiguration;
            string accessKey = Settings.Default.BingAccessKey;
            string searchResponse;
            ResponseBing objResponse;
            SearchEngineQuery result = new SearchEngineQuery();

            using (var client = new HttpClient())
            {
                try
                {
                    //Validation
                    if (string.IsNullOrWhiteSpace(searchCriteria))
                    {
                        throw new Exception("Search criteria is empty or null");
                    }
                    //Call service engine service
                    client.BaseAddress = new Uri(urlBase);
                    client.DefaultRequestHeaders.Add(Settings.Default.BingServiceHeader, accessKey);
                    var response = await client.GetAsync($"{Settings.Default.BingServicePath}?q={searchCriteria}&customconfig={customConfigurationId}");
                    response.EnsureSuccessStatusCode();
                    //Process content of response
                    searchResponse = await response.Content.ReadAsStringAsync();
                    objResponse = JsonConvert.DeserializeObject<ResponseBing>(searchResponse);
                    //Collect data
                    result.Status = (short)ProcessStatus.Success;
                    result.SearchEngine = nameof(SearchEngine.Bing);
                    result.TotalResults = objResponse.WebPages.TotalEstimatedMatches;
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return result;
        }
    }
}
