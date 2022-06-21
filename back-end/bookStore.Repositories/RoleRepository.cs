using BookStore.Models.Models;
using BookStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookStore.Repositories
{
    public  class RoleRepository : BaseRepository
    {
        public ListResponse<Role> GetRoles(int pageIndex, int pageSize, string keyword)
        {
            keyword = keyword?.ToLower()?.Trim();
            var query = _context.Roles.Where(c => keyword == null || c.Name.ToLower().Contains(keyword)).AsQueryable();
            int totalRecords = query.Count();
            List<Role> roles = query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            return new ListResponse<Role>()
            {
                records = roles,
                totalRecords = totalRecords
            };
        }
        public Role GetRole(int id)
        {
            if (id > 0)
            {
                return _context.Roles.FirstOrDefault(c => c.Id == id);
            }
            return null;
        }

        public Role AddCategory(Role role)
        {
            var entry = _context.Add(role);
            _context.SaveChanges();
            return entry.Entity;
        }
        public Role UpdateRole(Role role)
        {
            var entry = _context.Update(role);
            _context.SaveChanges();
            return entry.Entity;
        }

        public bool DeleteRole(int id)
        {
            var role = _context.Roles.FirstOrDefault(c => c.Id == id);
            if (role == null)
                return false;
            _context.Roles.Remove(role);
            _context.SaveChanges();
            return true;
        }

    }
}
