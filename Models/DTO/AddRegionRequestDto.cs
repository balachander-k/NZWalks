using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO
{
    public class AddRegionRequestDto
    {
        [Required]
        [MinLength(3, ErrorMessage =" Region Code must be a minimum of 3 Characters")]
        [MaxLength(3, ErrorMessage =" Region Code must be a maximum of 3 Characters")]
        public string Code { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = " Name has to be a minimum of 100 Characters")]
        public string Name { get; set; }

        public string? RegionImageURL { get; set; }
    }

}
