namespace NzWalks.Models.DTOs
{
    public class AddWalkDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int LengthInKm { get; set; }

        public string? ImageUrl { get; set; }

        public Guid RegionId { get; set; }
        public Guid DifficultyId { get; set; }
    }
}
