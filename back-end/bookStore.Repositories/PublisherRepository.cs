
using BookStore.Models.Models;
using BookStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookStore.Repositories
{
    public class PublisherRepository : BaseRepository
    {
        public ListResponse<Publisher> GetPublisher(int pageIndex, int pageSize, string keyword)
        {
            keyword = keyword?.ToLower()?.Trim();
            var query = _context.Publishers.Where(c => keyword == null || c.Name.ToLower().Contains(keyword)).AsQueryable();
            int totalRecords = query.Count();
            List<Publisher> publishers = query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            return new ListResponse<Publisher>()
            {
                records = publishers,
                totalRecords = totalRecords
            };
        }
        public Publisher GetPublisher(int id)
        {
            if (id > 0)
            {
                return _context.Publishers.Where(w => w.Id == id).FirstOrDefault();
            }
            return null;
        }
       
        public bool updatePublisher(Publisher model)
        {
            PublisherRepository _publishrepository = new PublisherRepository();
            if (model.Id > 0)
            {

                var user = _publishrepository.GetPublisher(model.Id);
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
        public Publisher AddPublisher(Publisher publisher)
        {
            var entry = _context.Add(publisher);
            _context.SaveChanges();
            return entry.Entity;
        }

        public bool deletePublisher(int id)
        {
            var user = _context.Publishers.FirstOrDefault(c => c.Id == id);
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
