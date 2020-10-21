namespace SearchFight.Core.Entities
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public short Status { get; set; }
        public string ErrorMessage { get; set; }
    }
}
