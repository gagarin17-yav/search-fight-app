namespace SearchFight.Core.Entities
{
    public class KeywordInfo : BaseEntity
    {
        public string Keyword { get; set; }
        public long SumTotalResults { get; set; }
    }
}
