using System.Security.Cryptography;
using System.Text;

namespace Application.Tokens.VerificationToken
{
    public class VerifySignatureToken
    {
        public bool TokenIsValid;

        private readonly string _accessToken;
        private readonly string _secret;

        public VerifySignatureToken(string accessToken, string secret)
        {
            _accessToken = accessToken.Replace("\"", "");
            _secret = secret;
            VerifySignature();
        }

        public void VerifySignature()
        {
            string[] parts = _accessToken.Split(".".ToCharArray());
            string header = parts[0];
            string payload = parts[1];
            string signature = parts[2];

            byte[] bytesToSign = Encoding.UTF8.GetBytes(string.Join(".", header, payload));
            byte[] bytesToSecret = Encoding.UTF8.GetBytes(_secret);

            HMACSHA256 alg = new HMACSHA256(bytesToSecret);
            byte[] hash = alg.ComputeHash(bytesToSign);

            string computedSignature = Base64UrlEncode(hash);

            TokenIsValid = signature == computedSignature;
        }
        private string Base64UrlEncode(byte[] input)
        {
            var output = Convert.ToBase64String(input);
            output = output.Split('=')[0];
            output = output.Replace('+', '-');
            output = output.Replace('/', '_');
            return output;
        }
    }
}
