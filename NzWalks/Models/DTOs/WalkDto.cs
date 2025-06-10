namespace NzWalks.Models.DTOs
{
    public class WalkDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int LengthInKm { get; set; }

        public string? ImageUrl { get; set; }
        public regionDto Region { get; set; }
        
        public DifficultyDto Difficulty { get; set; }
    }
}
