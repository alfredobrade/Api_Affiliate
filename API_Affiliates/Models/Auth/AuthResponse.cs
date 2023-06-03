namespace API_Affiliates.Models.Auth
{
    public class AuthResponse
    {
        public string Token { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
