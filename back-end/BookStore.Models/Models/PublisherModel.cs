using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models.ViewModels
{
    public class PublisherModel
    {    
        public PublisherModel()
        { }
        public PublisherModel(Publisher publisher)
        {
            Id = publisher.Id;
            Name = publisher.Name;
            Address = publisher.Address;
            Contact = publisher.Contact;
        }
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Contact { get; set; } = null!;
    }
}
