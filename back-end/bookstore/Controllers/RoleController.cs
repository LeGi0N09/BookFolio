using bookStore.Repositories;
using BookStore.Models.Models;
using BookStore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace bookstore.Controllers
{
    
    [Route("api/role")]
    [ApiController]
   
        public class RoleController : ControllerBase
        {
            RoleRepository _rolerepository = new RoleRepository();
            [Route("list")]
            [HttpGet]

            [ProducesResponseType(typeof(ListResponse<RoleModel>), (int)HttpStatusCode.OK)]
            public IActionResult GetRoles(int pageIndex = 1, int pageSize = 10, string? keyword = "")
            {
                var roles = _rolerepository.GetRoles(pageIndex, pageSize, keyword);
                ListResponse<RoleModel> listResponse = new ListResponse<RoleModel>()
                {
                    records = roles.records.Select(c => new RoleModel(c)),
                    totalRecords = roles.totalRecords,
                };
                return Ok(listResponse);
            }


            [HttpGet("{id}")]
            [ProducesResponseType(typeof(ListResponse<RoleModel>), (int)HttpStatusCode.OK)]
            public IActionResult GetRole(int id)
            {
                var roles = _rolerepository.GetRole(id);
                RoleModel rolemodel = new RoleModel(roles);
                return Ok(rolemodel);
            }

            [HttpPost("add")]
            [ProducesResponseType(typeof(ListResponse<RoleModel>), (int)HttpStatusCode.OK)]
            [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
            public IActionResult AddCategory(RoleModel model)
            {
                if (model == null)
                {
                    return BadRequest("Model is null");
                }
                Role role = new Role()
                {
                    Id = model.Id,
                    Name = model.Name
                };
                var response = _rolerepository.AddCategory(role);
                 RoleModel roleModel = new RoleModel(response);

                return Ok(response);
            }

            [HttpPut("update")]
            [ProducesResponseType(typeof(RoleModel), (int)HttpStatusCode.OK)]
            [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
            public IActionResult UpdateCategory(RoleModel model)
            {
                if (model == null)
                {
                    return BadRequest("Model is null");
                }
                Role role = new Role()
                {
                    Id = model.Id,
                    Name = model.Name
                };
                var response = _rolerepository.UpdateRole(role);
                RoleModel roleModel = new RoleModel(response);

                return Ok(roleModel);
            }

            [HttpDelete("delete /{id}")]
            [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
            [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
            public IActionResult DeleteRole(int id)
            {
                if (id > 0)
                    return BadRequest("id is null");
                var response = _rolerepository.DeleteRole(id);
                return Ok(response);

            }
        }
    }

