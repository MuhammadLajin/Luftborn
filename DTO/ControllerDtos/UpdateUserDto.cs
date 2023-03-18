using System.ComponentModel.DataAnnotations;

namespace SharedDTO.ControllerDtos
{
    public class UpdateUserDto
    {
        [Range(1, int.MaxValue, ErrorMessage = "Value must be greater than {0}")]
        public long Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Password { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Value must be greater than {0}")]
        public int deposit { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Value must be greater than {0}")]
        public long RoleId { get; set; }
    }
}
