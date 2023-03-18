using System.ComponentModel.DataAnnotations;

namespace SharedDTO.ControllerDtos
{
    public class ProductDto
    {

        public long Id { get; set; }
        public string Name { get; set; }
        public int Cost { get; set; }
        public int AmountAvailable { get; set; }
        public long SellerId { get; set; }
    }
}
