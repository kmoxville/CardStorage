namespace CardStorage.Utils
{
    public class MySQLOptions
    {
        public static string Position { get; set; } = "ConnectionStrings:" + nameof(MySQLOptions);

        public string Server { get; set; } = string.Empty;

        public string Scheme { get; set; } = string.Empty;

        public string User { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string GetConnectionString()
        {
            return $"server={Server};user={User};password={Password};database={Scheme}";
        }
    }
}
