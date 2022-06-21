using bookStore.Repositories;
using BookStore.Models.Models;
using BookStore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace bookstore.Controllers
{
    [ApiController]
    [Route("api/order")]
    public class orderController : ControllerBase
    {
        orderRepository _order = new orderRepository();
        [HttpPost]
        [Route("add")]
        public IActionResult add(finalOrderModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }
            Order ord = new Order()
            {
                Id = model.id,
                Userid = model.userId,
                Date = model.orderDate,
                Cartids = model.cartIds,
            };
            var res = _order.add(ord);
            finalOrderModel finalord = new finalOrderModel(res);
            return Ok(finalord);
        }
    }
}