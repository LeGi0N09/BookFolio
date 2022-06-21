using System;
using System.Collections.Generic;

namespace BookStore.Models.ViewModels
{
    public partial class Ordermst
    {
        public Ordermst()
        {
            Orderdtls = new HashSet<Orderdtl>();
        }

        public int Id { get; set; }
        public int Userid { get; set; }
        public DateTime Orderdate { get; set; }
        public decimal Totalprice { get; set; }

        public virtual User User { get; set; } = null!;
        public virtual ICollection<Orderdtl> Orderdtls { get; set; }
    }
}
