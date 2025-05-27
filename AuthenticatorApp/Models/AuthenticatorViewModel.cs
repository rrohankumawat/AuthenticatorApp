namespace AuthenticatorApp.Models
{
    public class AuthenticatorViewModel
    {
        public string? CurrentCode { get; set; }
        public int RemainingSeconds { get; set; }
        public string? SecretKey { get; set; }
        public string? InputCode { get; set; }
        public bool? IsValid { get; set; }
        public bool IsAlertVisible { get; set; }
    }
}
