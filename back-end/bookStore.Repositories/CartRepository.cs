using bookStore.Repositories;
using BookStore.Models.Models;
using BookStore.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Repositories
{
    public class CartRepository : BaseRepository
    {
        public ListResponse<GetCartModel> GetCarts(int pageIndex, int pageSize, int Userid)
        {
            var query = _context.Carts.AsQueryable();
            ListResponse<GetCartModel> result = new ListResponse<GetCartModel>();
            List<GetCartModel> getCartModels = new List<GetCartModel>();

            query = query.Where(cart => (cart.Userid == Userid)).Skip((pageIndex - 1) * pageSize).Take(pageSize);

            foreach (Cart cart in query.ToList())
            {
                GetCartModel getCartModel = new GetCartModel();
                getCartModel.Id = cart.Id;
                getCartModel.Userid = cart.Userid;

                Book book = _context.Books.Where(b => b.Id == cart.Bookid).FirstOrDefault();
                BookModel bookModel = new BookModel(book);
                getCartModel.Book = bookModel;
                getCartModel.Quantity = cart.Quantity;
                getCartModels.Add(getCartModel);
            }
            result.totalRecords = getCartModels.Count();
            result.records = getCartModels;
            return result;
        }

        public Cart GetCart(int id)
        {
            return _context.Carts.FirstOrDefault(c => c.Id == id);
        }

        public Cart AddCart(Cart cart)
        {
            if (_context.Carts.Any(c => c.Id != cart.Id && c.Bookid == cart.Bookid && c.Userid == cart.Userid))
                throw new Exception($"Book with id {cart.Bookid} already added in cart. Update the quantity of added item in the cart.");
            else
            {
                var entry = _context.Carts.Add(cart);
                _context.SaveChanges();
                return entry.Entity;
            }
        }

        public Cart UpdateCart(Cart cart)

        {
            CartRepository _cartrepository = new CartRepository();

            var cartInDb = _cartrepository.GetCart(cart.Id);


            cartInDb.Quantity = cart.Quantity;
            var update = _context.Carts.Update(cartInDb);
            _context.SaveChanges();
            return update.Entity;

        }

        public bool DeleteCart(int id)
        {
            CartRepository _cartrepository = new CartRepository();
            var cart = _cartrepository.GetCart(id);
            if (cart == null)
                return false;
            _context.Carts.Remove(cart);
            _context.SaveChanges();
            return true;
        }
    }
}