using System.ComponentModel.DataAnnotations;

namespace ASP_NET_hw2.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        public string? ImagePath { get; set; } // new
    }
}
