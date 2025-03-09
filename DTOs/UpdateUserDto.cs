namespace SoundScape.DTOs
{
    public class UpdateUserDto
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public int? BirthDay { get; set; }
        public int? BirthMonth { get; set; }
        public int? BirthYear { get; set; }
        public string? Gender { get; set; }
        public string? AvatarUrl { get; set; }
    }
}
