using OtpNet;

namespace AuthenticatorApp.Services
{
    public class TotpService
    {
        private readonly byte[] _secretKey;
        private readonly Totp _totp;

        public TotpService()
        {
            // Generate a random secret key (in a real app, this would be persisted per user)
            _secretKey = KeyGeneration.GenerateRandomKey(20);
            _totp = new Totp(_secretKey, step: 30); // 30-second step (standard TOTP)
        }

        public string GenerateCode()
        {
            return _totp.ComputeTotp(DateTime.UtcNow);
        }

        public int GetRemainingSeconds()
        {
            return _totp.RemainingSeconds(DateTime.UtcNow);
        }

        public bool VerifyCode(string code)
        {
            long timeStepMatched;
            return _totp.VerifyTotp(code, out timeStepMatched, window: null);
        }

        public string GetSecretKeyBase32()
        {
            return Base32Encoding.ToString(_secretKey);
        }
    }
}
