using System;
using System.Collections.Generic;

namespace BookStore.Models.ViewModels
{
    public partial class User
    {
        public User()
        {
            Carts = new HashSet<Cart>();
            Ordermsts = new HashSet<Ordermst>();
        }

        public int Id { get; set; }
        public string Firstname { get; set; } = null!;
        public string Lastname { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int Roleid { get; set; }

        public virtual Role Role { get; set; } = null!;
        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<Ordermst> Ordermsts { get; set; }
    }
}
