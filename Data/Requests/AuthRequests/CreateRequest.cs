namespace CardStorage.Data.Requests.AuthRequests
{
    public class CreateRequest
    {
        public string UserName { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
    }
}
