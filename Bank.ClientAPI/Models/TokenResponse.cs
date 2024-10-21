namespace Bank.ClientAPI.Models
{
    public sealed class TokenResponse
    {
        public required string TokenType { get; set; }
        public required string AccessToken {  get; set; }
        public int ExpiresIn { get; set; }
        public required string RefreshToken { get; set; }
    }
}
