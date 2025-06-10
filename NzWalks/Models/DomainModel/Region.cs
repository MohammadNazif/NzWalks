namespace NzWalks.Models.DomainModel
{
    public class Region
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Code { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
