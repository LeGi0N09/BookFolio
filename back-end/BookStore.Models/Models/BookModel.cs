using BookStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models.Models
{
    public class BookModel
    {
        public BookModel() { }
        public BookModel(Book book)
        {
            id = book.Id;
            name = book.Name;
            price = book.Price;
            description = book.Description;
            categoryId = book.Categoryid;
            publisherId = book.Publisherid;
            quantity = book.Quantity;
            base64image = book.Base64image;
        }

        public int id { get; set; }
        public string name { get; set; } = null!;
        public decimal price { get; set; }
        public string? description { get; set; }
        public string? base64image { get; set; }
        public int categoryId { get; set; }
        public int publisherId { get; set; }
        public int? quantity { get; set; }



    }
}
