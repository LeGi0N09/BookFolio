using BookStore.Models.Models;
using BookStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookStore.Repositories
{
    public  class BookRepository : BaseRepository
    {
        public ListResponse<Book> GetBooks(int pageIndex, int pageSize, string keyword)
        {

            if (pageIndex > 0)
            {
                keyword = keyword?.ToLower()?.Trim();
                var users = _context.Books.Where(w => keyword == null || w.Name.ToLower().Contains(keyword)).AsQueryable();
                int totalRecords = users.Count();
                List<Book> bookList = users.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

                return new ListResponse<Book>()
                {
                    records = bookList,
                    totalRecords = totalRecords
                };
            }
            return null;
        }

        public Book GetBook(int id)
        {
            if (id > 0)
            {
                return _context.Books.Where(w => w.Id == id).FirstOrDefault();
            }
            return null;
        }

        public bool updateBook(Book model)
        {
            if (model.Id > 0)
            {
                BookRepository _bookrepository = new BookRepository();
                var user = _bookrepository.GetBook(model.Id);
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

        public Book AddBook(Book book)
        {
            var entry = _context.Add(book);
            _context.SaveChanges();
            return entry.Entity;
        }

        public bool deleteBook(int id)
        {
            var user = _context.Books.FirstOrDefault(c => c.Id == id);
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
