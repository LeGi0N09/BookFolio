
using bookStore.Repositories;
using BookStore.Models;
using BookStore.Models.Models;
using BookStore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace bookstore.Controllers
{
    [Route("api/User")]
    [ApiController]
    public class UserController : ControllerBase
    {
        UserRepositories _repository = new UserRepositories();
        DemoAES obj = new DemoAES();
        [HttpGet("list")]
        // [ProducesResponseType(typeof(ListResponse<UserModel>), (int)HttpStatusCode.OK)]

        public IActionResult GetUsers(int pageIndex = 1, int pageSize = 10, string? keyword = "")
        {
            try
            {
                var users = _repository.GetUsers(pageIndex, pageSize, keyword);
                ListResponse<UserModel> listResponse = new ListResponse<UserModel>()
                {
                    records = users.records.Select(c => new UserModel(c)),
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
        //  [ProducesResponseType(typeof(UserModel), (int)HttpStatusCode.OK)]
        // [ProducesResponseType(typeof(NotFoundObjectResult), (int)HttpStatusCode.NotFound)]

        public IActionResult GetUser(int id)
        {
            try
            {
                var users = _repository.GetUser(id);

                if (users == null)
                    return StatusCode(HttpStatusCode.NotFound.GetHashCode(), "Please provide correct information");
                UserModel userModel = new UserModel(users);
                return StatusCode(HttpStatusCode.OK.GetHashCode(), userModel);
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);
            }
        }

        [HttpPut("update")]
        //    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        //  [ProducesResponseType(typeof(NotFoundObjectResult), (int)HttpStatusCode.NotFound)]
        // [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
        public IActionResult Update(UserModel model)
        {
            try
            {
                if (model != null)
                {
                    User user = new User()
                    {
                        Id = model.id,
                        Firstname = model.firstName,
                        Lastname = model.lastName,
                        Email = model.email,
                        Password = obj.ComputeMD5Hash(model.password),
                        Roleid = model.roleId,
                    };
                    var isUpdated = _repository.updateUser(user);
                    if (isUpdated)
                    {
                        return StatusCode(HttpStatusCode.OK.GetHashCode(), user);
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

        [HttpDelete("delete/{id}")]
        //      [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        //      [ProducesResponseType(typeof(NotFoundObjectResult), (int)HttpStatusCode.NotFound)]

        public IActionResult Delete(int id)
        {
            
                var isDeleted = _repository.deleteUser(id);
                if (isDeleted)
                {
                    return StatusCode(HttpStatusCode.OK.GetHashCode(), "User detail deleted successfully");
                }
                else
                    return StatusCode(HttpStatusCode.NotFound.GetHashCode(), "Please provide correct information");
            
           
            
               
        }
    }
}
