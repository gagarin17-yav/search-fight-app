using System.Collections.Generic;

namespace SearchFight.Core.Entities
{
    public class SearchResult : BaseEntity
    {
        public string TotalWinners { get; set; }
        public List<EngineKeyword> KeywordResults { get; set; }
        public List<SearchEngineQuery> SearchEngineResults { get; set; }
    }
}
