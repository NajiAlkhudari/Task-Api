namespace TaskAlbayan.Common.Dtos
{
    public class TokenResponseDto
    {
        public string Token { get; set; }
        public DateTime ExpiresAt { get; set; }
        public int Role { get; set; }



    }
}