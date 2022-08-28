namespace CardStorage.Data.Entities
{
    public class Client : BaseEntity
    {
        public string FirstName { get; set; } = string.Empty;

        public string SecondName { get; set; } = string.Empty;

        public virtual ICollection<ClientCard>? Cards { get; set; }
    }
}
