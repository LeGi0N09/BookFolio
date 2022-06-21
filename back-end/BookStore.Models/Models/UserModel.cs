
using BookStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models.Models
{
    public class UserModel
    {  public UserModel() { }
        public UserModel(User user)
        {
            id = user.Id;
            firstName = user.Firstname;
            lastName = user.Lastname;
            email = user.Email;
            roleId = user.Roleid;
        }
        public int id { get; set; }
        public string firstName { get; set; } = null!;
        public string lastName { get; set; } = null!;
        public string email { get; set; } = null!;
        public string password { get; set; } = null!;
        public int roleId { get; set; }
    }
}
