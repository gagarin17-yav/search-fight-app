namespace SearchFight.Core.Entities
{
    public class SearchEngineQuery : BaseEntity
    {
        public string SearchEngine { get; set; }
        public long TotalResults { get; set; }
        public int KeywordId { get; set; }
        public string Keyword { get; set; }
    }
}
