
using BookStore.Models;
using BookStore.Models.Models;
using BookStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookStore.Repositories
{
    public class UserRepositories : BaseRepository
    {
        //  postgresContext _context = new postgresContext();

        public ListResponse<User> GetUsers(int pageIndex, int pageSize, string keyword)
        {

            if (pageIndex > 0)
            {
                keyword = keyword?.ToLower().Trim();
                var users = _context.Users.Where(w => w.Firstname.ToLower().Contains(keyword) || w.Lastname.ToLower().Contains(keyword)).AsQueryable();
                int totalRecords = users.Count();
                List<User> userList = users.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

                return new ListResponse<User>()
                {
                    records = userList,
                    totalRecords = totalRecords
                };
            }
            return null;
        }

        public User Login(User user)
        {
            return _context.Users.FirstOrDefault(c => c.Email.Equals(user.Email.ToLower()) && c.Password.Equals(user.Password));
        }

        public User Register(User user)
        {
            var entry = _context.Users.Add(user);
            _context.SaveChanges();
            return entry.Entity;
        }

        public User GetUser(int id)
        {
            if (id > 0)
            {
                return _context.Users.Where(w => w.Id == id).FirstOrDefault();
            }
            return null;
        }
       /* public User Getpass(string pass)
        {
            if (pass!=null)
            {
                return _context.Users.Where(w => w.Password == pass).FirstOrDefault();
            }
            return null;
        }*/
        public bool updateUser(User model)
        {
            if (model.Id > 0)
            {
                UserRepositories _repository = new UserRepositories();
                var user = _repository.GetUser(model.Id);
                if (user == null)
                {
                    return false;
                }

                _context.Update(model);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool deleteUser(int id)
        {
            UserRepositories _repository = new UserRepositories();
            var user = _repository.GetUser(id);
            if (user != null)
            {
                _context.Remove(user);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}

