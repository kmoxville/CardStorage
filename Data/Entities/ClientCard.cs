using System.ComponentModel.DataAnnotations;

namespace CardStorage.Data.Entities
{
    public class ClientCard : BaseEntity
    {
        public DateTime ExpireAt { get; set; }

        public string Number { get; set; } = string.Empty;

        public string CVV { get; set; } = string.Empty;

        public virtual Client? Client { get; set; }

        public int ClientId { get; set; }
    }
}
