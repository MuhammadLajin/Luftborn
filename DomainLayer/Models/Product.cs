namespace DomainLayer.Models
{
    public class Product : BaseEntity
    {
        
        public string Name { get; set; }
        public int Cost { get; set; }
        public int AmountAvailable { get; set; }
        public long SellerId { get; set; }
        public virtual User Seller { get; set; }
    }
}
