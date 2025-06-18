using System.ComponentModel.DataAnnotations;

namespace NzWalks.Models.DTOs
{
    public class LoginrequestDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
