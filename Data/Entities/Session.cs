namespace CardStorage.Data.Entities
{
    public class Session : BaseEntity
    {
        public Account Account { get; set; } = null!;

        public int AccountId { get; set; }

        public string Token { get; set; } = string.Empty;
    }
}
