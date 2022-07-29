namespace ReactMoviesWebApi.DTO
{
    public class AuthenticationResponseDTO
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
