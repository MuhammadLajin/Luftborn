using System.Collections.Generic;

namespace DomainLayer.Models
{
    public class User : BaseEntity
    {
        public User()
        {
            Products = new HashSet<Product>();
        }
        public string Name { get; set; }
        public string Password { get; set; }
        public int deposit { get; set; }
        public long RoleId { get; set; }
        public virtual Role Role { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
