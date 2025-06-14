using System.ComponentModel.DataAnnotations;

namespace NzWalks.Models.DTOs
{
    public class updatewalkDto
    {
        [Required]
        [MaxLength(3, ErrorMessage = "Name should be of Max 100 length")]
        public string Name { get; set; }

        [Required]
        [MaxLength(1000, ErrorMessage = "Description should be of Max 1000 length")]
        public string Description { get; set; }
        [Required]
        [Range(0, 50)]
        public int LengthInKm { get; set; }

        public string? ImageUrl { get; set; }
        [Required]
        public Guid RegionId { get; set; }
        [Required]
        public Guid DifficultyId { get; set; }

    }
}
