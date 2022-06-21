
using bookStore.Repositories;
using BookStore.Models.Models;
using BookStore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace bookstore.Controllers
{
    [Route("api/publisher")]
    [ApiController]
    public class PublisherController : ControllerBase
    {
        PublisherRepository _publishrepository = new PublisherRepository();

        [Route("list")]
        [HttpGet]
        public IActionResult GetPublisher(int pageIndex = 1, int pageSize = 10, string? keyword = "")
        {
             try
            {
                var publishers = _publishrepository.GetPublisher(pageIndex, pageSize, keyword);
                ListResponse<PublisherModel> listResponse = new ListResponse<PublisherModel>()
                {
                    records = publishers.records.Select(c => new PublisherModel(c)),
                    totalRecords = publishers.totalRecords,
                };
                return Ok(listResponse);

            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetPublisher(int id)
        {
            try
            {
                var users =_publishrepository.GetPublisher(id);

                if (users == null)
                    return StatusCode(HttpStatusCode.NotFound.GetHashCode(), "Please provide correct information");
                PublisherModel publisherModel = new PublisherModel(users);
                return StatusCode(HttpStatusCode.OK.GetHashCode(), publisherModel);
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);
            }
        }

        [HttpPut("update")]
        public IActionResult updatePublisher(PublisherModel model)
        {   
            try
            {
                if (model != null)
                {
                    Publisher publisher = new Publisher()
                    {
                        Id = model.Id,
                        Name = model.Name,
                        Address = model.Address,
                        Contact = model.Contact,
                     };
                    var isUpdated = _publishrepository.updatePublisher(publisher);
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
        public IActionResult AddPublisher(PublisherModel model)
        {   
            if(model==null)
            {
                return BadRequest("please insert correct details");
            }
            Publisher publisher = new Publisher()
            {
                Id = model.Id,
                Name = model.Name,
                Address = model.Address,
                Contact = model.Contact,
            };
            var response = _publishrepository.AddPublisher(publisher);
             PublisherModel publishModel = new PublisherModel(response);

            return Ok(publishModel);
        }
        [HttpDelete("delete")]
        public IActionResult Delete(int id)
        {
            try
            {
                var isDeleted = _publishrepository.deletePublisher(id);
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
