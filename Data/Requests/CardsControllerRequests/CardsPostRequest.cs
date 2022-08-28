namespace CardStorage.Data.Requests.CardsControllerRequests
{
    public class CardsPostRequest
    {
        public DateTime ExpireAt { get; set; }

        public string Number { get; set; } = string.Empty;

        public string CVV { get; set; } = string.Empty;

        public int ClientId { get; set; }
    }
}
