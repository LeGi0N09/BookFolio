using System;
using System.Collections.Generic;

namespace BookStore.Models.ViewModels
{
    public partial class Book
    {
        public Book()
        {
            Carts = new HashSet<Cart>();
            Orderdtls = new HashSet<Orderdtl>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public string? Base64image { get; set; }
        public int Categoryid { get; set; }
        public int Publisherid { get; set; }
        public int? Quantity { get; set; }

        public virtual Category Category { get; set; } = null!;
        public virtual Publisher Publisher { get; set; } = null!;
        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<Orderdtl> Orderdtls { get; set; }
    }
}
