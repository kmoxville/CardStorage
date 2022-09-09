namespace CardStorage.Data.Responses.AuthResponses
{
    public class LoginResponse : Response
    {
        public AuthStatuses Status;

        public string SessionToken { get; set; } = string.Empty;
    }
}
