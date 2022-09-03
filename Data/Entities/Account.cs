namespace CardStorage.Data.Entities
{
    public class Account : BaseEntity
    {
        public string Email { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string PasswordSalt { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;
    }
}
