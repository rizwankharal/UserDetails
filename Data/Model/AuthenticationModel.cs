using System.ComponentModel.DataAnnotations;

namespace UserDetails.Data.Model
{
    public class AuthenticationModel
    {
        public string Token { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }
    }
}
