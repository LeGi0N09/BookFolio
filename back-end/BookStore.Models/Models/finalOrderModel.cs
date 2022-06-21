using BookStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models.Models
{
    public class finalOrderModel
    {
        public finalOrderModel()
        {

        }
        public finalOrderModel(Order model)
        {
            id = model.Id;
            userId = model.Userid;
            orderDate = model.Date;
            cartIds = model.Cartids;
        }
        public int id { get; set; }
        public int userId { get; set; }
        public string? orderDate { get; set; }
        public int[] cartIds { get; set; } = null!;
    }
}