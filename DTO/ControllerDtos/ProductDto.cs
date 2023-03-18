using System.ComponentModel.DataAnnotations;

namespace SharedDTO.ControllerDtos
{
    public class ProductDto
    {

        [Range(1, int.MaxValue, ErrorMessage = "Value must be greater than {0}")]
        public long Id { get; set; }
        
        [Required(AllowEmptyStrings = false)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Name { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Value must be greater than {0}")]
        public int Cost { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Value must be greater than {0}")]
        public int AmountAvailable { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Value must be greater than {0}")]
        public long SellerId { get; set; }
    }
}
