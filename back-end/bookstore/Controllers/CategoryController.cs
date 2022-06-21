
using bookStore.Repositories;
using BookStore.Models.Models;
using BookStore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace bookstore.Controllers
{
    [Route("api/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        CategoryRepository _categoryrepository = new CategoryRepository();
        [Route("list")]
        [HttpGet]

        [ProducesResponseType(typeof(ListResponse<CategoryModel>), (int)HttpStatusCode.OK)]
        public IActionResult GetCategories(int pageIndex = 1, int pageSize = 10, string? keyword = "")
        {
            var categories = _categoryrepository.GetCategories(pageIndex, pageSize, keyword);
            ListResponse<CategoryModel> listResponse = new ListResponse<CategoryModel>()
            {
                records = categories.records.Select(c => new CategoryModel(c)),
                totalRecords = categories.totalRecords,
            };
            return Ok(listResponse);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ListResponse<CategoryModel>), (int)HttpStatusCode.OK)]
        public IActionResult GetCategory(int id)
        {
            var categories = _categoryrepository.GetCategory(id);
            CategoryModel categoryModel = new CategoryModel(categories);
            return Ok(categoryModel);
        }

        [HttpPost("add")]
        [ProducesResponseType(typeof(ListResponse<CategoryModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
        public IActionResult AddCategory(CategoryModel model)
        {
            if (model == null)
            {
                return BadRequest("Model is null");
            }
            Category category = new Category()
            {
                Id = model.Id,
                Name = model.Name
            };
            var response = _categoryrepository.AddCategory(category);
            // CategoryModel categoryModel = new CategoryModel(response);

            return Ok(response);
        }

        [HttpPut("update")]
        [ProducesResponseType(typeof(CategoryModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
        public IActionResult UpdateCategory(CategoryModel model)
        {
            if (model == null)
            {
                return BadRequest("Model is null");
            }
            Category category = new Category()
            {
                Id = model.Id,
                Name = model.Name
            };
            var response = _categoryrepository.UpdateCategory(category);
            CategoryModel categoryModel = new CategoryModel(response);

            return Ok(categoryModel);
        }

        [HttpDelete("delete /{id}")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
        public IActionResult DeleteCategory(int id)
        {
            if (id>0)
                return BadRequest("id is null");
            var response = _categoryrepository.DeleteCategory(id);
            return Ok(response);

        }
    }
}
