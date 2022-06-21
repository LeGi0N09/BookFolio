
using BookStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookStore.Repositories
{
    public class BaseRepository
    {
       protected readonly postgresContext _context = new postgresContext();
    }
}
