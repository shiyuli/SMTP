namespace SMTP.Core
{
    public class ClientOptions
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public bool EnableSsl { get; set; }

        public string Username { get; set; }
        public string Password { get; set; }

        public ClientOptions()
        {
            EnableSsl = true;
        }
    }
}
