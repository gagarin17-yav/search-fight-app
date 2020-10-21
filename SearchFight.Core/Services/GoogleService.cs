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
    public class GoogleService : IService
    {
        private static readonly Lazy<GoogleService> _instance = new Lazy<GoogleService>(() => new GoogleService(), LazyThreadSafetyMode.PublicationOnly);

        public static GoogleService Instance
        {
            get { return _instance.Value; }
        }

        public GoogleService()
        {

        }

        /// <summary>
        /// Perform a Google search based on a search criteria
        /// </summary>
        /// <param name="searchCriteria">Search criteria</param>
        /// <returns>SearchEngineQuery object</returns>
        public async Task<SearchEngineQuery> Search(string searchCriteria)
        {
            string cxCode = Settings.Default.GoogleCxCode;
            string apiKey = Settings.Default.GoogleApiKey;
            string urlAddress = Settings.Default.GoogleURLBase;
            string searchResponse;
            ResponseGoogle objResponse;
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
                    client.BaseAddress = new Uri(urlAddress);
                    HttpResponseMessage response = await client.GetAsync($"{Settings.Default.GoogleServicePath}?key={apiKey}&cx={cxCode}&q={searchCriteria}");
                    response.EnsureSuccessStatusCode();
                    //Process content of response
                    searchResponse = await response.Content.ReadAsStringAsync();
                    objResponse = JsonConvert.DeserializeObject<ResponseGoogle>(searchResponse);
                    //Collect data
                    result.Status = (short)ProcessStatus.Success;
                    result.SearchEngine = nameof(SearchEngine.Google);
                    result.TotalResults = long.Parse(objResponse.SearchInformation.TotalResults);
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
