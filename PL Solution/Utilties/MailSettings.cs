namespace PL_Solution.Utilties
{
    // ==> This class is used to store the mail settings form the (appsettings.json) file
    public class MailSettings
    {
        public string Email { get; set; } = null!;
        public string DisplayName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Host { get; set; } = null!;
        public int Port { get; set; }
    }
}
