using bookStore.Repositories;
using BookStore.Models.Models;
using BookStore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace bookstore.Controllers
{
    [Route("api/book")]
    [ApiController]
    public class BookController : ControllerBase
    {
        BookRepository _bookrepository = new BookRepository();
        [HttpGet("list")]
        [ProducesResponseType(typeof(ListResponse<BookModel>), (int)HttpStatusCode.OK)]
        public IActionResult GetBooks(int pageIndex = 1, int pageSize = 10, string? keyword = "")
        {
            try
            {
                var users = _bookrepository.GetBooks(pageIndex, pageSize, keyword);
                ListResponse<BookModel> listResponse = new ListResponse<BookModel>()
                {
                    records = users.records.Select(c => new BookModel(c)),
                    totalRecords = users.totalRecords,
                };
                return Ok(listResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BookModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(NotFoundObjectResult), (int)HttpStatusCode.NotFound)]
        public IActionResult GetBook(int id)
        {
            try
            {
                var users = _bookrepository.GetBook(id);

                if (users == null)
                    return StatusCode(HttpStatusCode.NotFound.GetHashCode(), "Please provide correct information");
                BookModel bookModel = new BookModel(users);
                return StatusCode(HttpStatusCode.OK.GetHashCode(), bookModel);
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);
            }
        }

        [HttpPut("update")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(NotFoundObjectResult), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
        public IActionResult updateBook(BookModel model)
        {
            try
            {
                if (model != null)
                {
                    Book book = new Book()
                    {
                        Id = model.id,
                        Name = model.name,
                        Price= model.price,
                        Description = model.description,
                         Categoryid = model.categoryId,
                        Publisherid = model.publisherId,
                        Quantity = model.quantity,
                        Base64image=model.base64image,
                    };
                    var isUpdated = _bookrepository.updateBook(book);
                    if (isUpdated)
                    {
                        return StatusCode(HttpStatusCode.OK.GetHashCode(), "User detail updated successfully");
                    }
                    else
                    {
                        return StatusCode(HttpStatusCode.NotFound.GetHashCode(), "User Not Found");
                    }
                }
                return StatusCode(HttpStatusCode.NotFound.GetHashCode(), "Please provide correct information");
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);
            }



        }

        [HttpPost("add")]
        [ProducesResponseType(typeof(BookModel), (int)HttpStatusCode.OK)]
       
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
        public IActionResult AddBook(BookModel model)
        {
            
                if (model == null)
                {
                    return BadRequest("please insert correct details");
                }
                Book book = new Book()
                {
                    Id = model.id,
                    Name = model.name,
                    Price = model.price,
                    Description = model.description,
                    Categoryid = model.categoryId,
                    Publisherid = model.publisherId,
                    Quantity = model.quantity,
                    Base64image = model.base64image,
                };
                var response = _bookrepository.AddBook(book);
                BookModel bookModel = new BookModel(response);
                
                return Ok(bookModel);
            
           
            
        }

        [HttpDelete]
        [Route("delete/{id}")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(NotFoundObjectResult), (int)HttpStatusCode.NotFound)]
        public IActionResult Delete(int id)
        {
            try
            {
                var isDeleted = _bookrepository.deleteBook(id);
                if (isDeleted)
                {
                    return StatusCode(HttpStatusCode.OK.GetHashCode(), "User detail deleted successfully");
                }
                else
                    return StatusCode(HttpStatusCode.NotFound.GetHashCode(), "Please provide correct information");
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);
            }
        }
    }
}
