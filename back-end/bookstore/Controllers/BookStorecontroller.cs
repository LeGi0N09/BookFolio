
using bookStore.Repositories;
using BookStore.Models;
using BookStore.Models.Models;
using BookStore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace bookstore.Controllers
{
    [Route("api/public")]
    [ApiController]
  
    public class BookStorecontroller : ControllerBase
    {
        UserRepositories _repository = new UserRepositories();
        DemoAES obj=new DemoAES();


        [HttpPost("login")]
      //  [ProducesResponseType(typeof(LoginModel), (int)HttpStatusCode.OK)]
       // [ProducesResponseType(typeof(NotFoundObjectResult), (int)HttpStatusCode.NotFound)]
        //[ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
        public IActionResult Login(LoginModel model)
        {    if(model == null)
            {
                return BadRequest("Model is null");
            }
             
            try
            {
                User user = new User()
                {
                    Email = model.Email,
                    Password = obj.ComputeMD5Hash(model.Password)
                };
                User response = _repository.Login(user);
                if (response == null)
                    return StatusCode(HttpStatusCode.NotFound.GetHashCode(), "User Not Found");
                UserModel userModel = new UserModel(response);
                return StatusCode(HttpStatusCode.OK.GetHashCode(), userModel);
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);
            }
        }

        [HttpPost("Register")]
        [ProducesResponseType(typeof(RegisterModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(NotFoundObjectResult), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
        public IActionResult Register(RegisterModel model)
        {
            try
            {
                User user = new User()
                {
                    Email = model.Email,
                    Password = obj.ComputeMD5Hash(model.Password),
                    Firstname = model.Firstname,
                    Lastname = model.Lastname,
                    Roleid = model.Roleid,
                };
                User response = _repository.Register(user);
                if (response == null)
                {
                    return StatusCode(HttpStatusCode.BadRequest.GetHashCode(), "Bad Request");
                }
                RegisterModel registerModel = new RegisterModel(response);
                return StatusCode(HttpStatusCode.OK.GetHashCode(), registerModel);
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);
            }
        }
    }
}
