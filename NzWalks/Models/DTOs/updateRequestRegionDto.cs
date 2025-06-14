using System.ComponentModel.DataAnnotations;

namespace NzWalks.Models.DTOs
{
    public class updateRequestRegionDto
    {
        [Required]
        [MaxLength(100, ErrorMessage = "Name should be of max 100 Character")]
        public string Name { get; set; }
        [Required]
        [MinLength(3, ErrorMessage = "Code should be of three length")]
        [MaxLength(3, ErrorMessage = "Code should be of three length")]
        public string Code { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
