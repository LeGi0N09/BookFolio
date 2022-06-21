
using BookStore.Models.Models;
using BookStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookStore.Repositories
{
    public class CategoryRepository : BaseRepository
    {
        public ListResponse<Category> GetCategories(int pageIndex, int pageSize, string keyword)
        {
            keyword = keyword?.ToLower()?.Trim();
            var query = _context.Categories.Where(c => keyword == null || c.Name.ToLower().Contains(keyword)).AsQueryable();
            int totalRecords = query.Count();
            List<Category> categories = query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            return new ListResponse<Category>()
            {
                records = categories,
                totalRecords = totalRecords
            };
        }

        public Category GetCategory(int id)
        {
            if(id>0)
            {
                return _context.Categories.FirstOrDefault(c => c.Id == id);
            }
            return null;
        }         

        public Category AddCategory(Category category)
        {
            var entry = _context.Add(category);
            _context.SaveChanges();
            return entry.Entity;
        }

        public Category UpdateCategory(Category category)
        {   
            var entry = _context.Update(category);
            _context.SaveChanges();
            return entry.Entity;
        }

        public bool DeleteCategory(int id)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
                return false;
            _context.Categories.Remove(category);
            _context.SaveChanges();
            return true;
        }
    }
}
