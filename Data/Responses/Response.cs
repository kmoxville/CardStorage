namespace CardStorage.Data.Responses
{
    public abstract class Response
    {
        public bool Success { get; set; }

        public string? ErrorMessage { get; set; }
    }
}
