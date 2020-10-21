using SearchFight.Core.Interfaces;
using SearchFight.Core.Services;
using static SearchFight.Core.Enumerations.Enumerations;

namespace SearchFight.Business.Search
{
    public static class SearchFactory
    {
        /// <summary>
        /// Get service instance based on search engine identifier
        /// </summary>
        /// <param name="searchService">search engine identifier</param>
        /// <returns>Service instance</returns>
        public static IService GetService(int searchService)
        {
            switch (searchService)
            {
                case (int)SearchEngine.Google:
                    return GoogleService.Instance;
                case (int)SearchEngine.Bing:
                    return BingService.Instance;
                default:
                    return GoogleService.Instance;
            }
        }
    }
}
