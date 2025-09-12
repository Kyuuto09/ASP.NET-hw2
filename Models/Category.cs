﻿using System.ComponentModel.DataAnnotations;

namespace ASP_NET_hw2.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string Name { get; set; } = string.Empty;

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
