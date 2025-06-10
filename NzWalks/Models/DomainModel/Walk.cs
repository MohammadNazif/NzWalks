namespace NzWalks.Models.DomainModel
{
    public class Walk
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int LengthInKm { get; set; }

        public string? ImageUrl { get; set; }

        public Guid RegionId { get; set; }
        public Guid DifficultyId { get; set; }

        //Navigation property
        public Difficulty Difficulty { get; set; }
        public Region Region { get; set; }
    }
}
