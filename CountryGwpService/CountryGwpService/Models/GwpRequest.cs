using System.ComponentModel.DataAnnotations;

namespace CountryGwpService.Models
{
    public class GwpRequest
    {
        [Required]
        public string Country { get; set; }
        [Required]
        public List<string> Lob { get; set; }
    }
}
