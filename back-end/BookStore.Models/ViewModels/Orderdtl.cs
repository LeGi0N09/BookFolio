using System;
using System.Collections.Generic;

namespace BookStore.Models.ViewModels
{
    public partial class Orderdtl
    {
        public int Id { get; set; }
        public int Bookid { get; set; }
        public int Quantity { get; set; }
        public decimal Totalprice { get; set; }
        public int Ordermstid { get; set; }
        public decimal Price { get; set; }

        public virtual Book Book { get; set; } = null!;
        public virtual Ordermst Ordermst { get; set; } = null!;
    }
}
