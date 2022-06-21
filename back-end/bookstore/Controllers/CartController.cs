using BookStore.Models.Models;
using BookStore.Models.ViewModels;
using BookStore.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("api/cart")]
    public class CartController : ControllerBase
    {
        CartRepository _cartrepository = new CartRepository();
        [HttpGet]
        [Route("list")]
        [ProducesResponseType(typeof(ListResponse<CartModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(NotFoundObjectResult), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]

        public IActionResult GetCarts(int pageIndex = 1, int pageSize = 10, int Userid = 0)
        {
            try
            {
                if (pageIndex > 0)
                {
                    var cart = _cartrepository.GetCarts(pageIndex, pageSize, Userid);
                    if (cart == null)
                    {
                        return StatusCode(HttpStatusCode.NotFound.GetHashCode(), "Cart item not found!");
                    }

                    return StatusCode(HttpStatusCode.OK.GetHashCode(), cart);
                }
                return StatusCode(HttpStatusCode.BadRequest.GetHashCode(), "Please insert details properly!");
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);

            }
        }


        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(CartModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(NotFoundObjectResult), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
        public IActionResult GetCart(int id)
        {
            try
            {
                if (id > 0)
                {
                    var carts = _cartrepository.GetCart(id);
                    if (carts == null)
                    {
                        return StatusCode(HttpStatusCode.NotFound.GetHashCode(), "Cart item not found!");
                    }
                    CartModel cartModel = new CartModel(carts);
                    return StatusCode(HttpStatusCode.OK.GetHashCode(), cartModel);
                }
                return StatusCode(HttpStatusCode.BadRequest.GetHashCode(), "Please insert details properly!");
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);

            }
        }

        [HttpPost]
        [Route("add")]
        [ProducesResponseType(typeof(CartModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
        public IActionResult AddCart(CartModel model)
        {
            try
            {
                if (model == null)
                {
                    return StatusCode(HttpStatusCode.BadRequest.GetHashCode(), "Please insert details properly!");
                }
                Cart cart = new Cart()
                {
                    Id = model.Id,
                    Bookid = model.Bookid,
                    Userid = model.Userid,
                    Quantity = 1
                };

                var response = _cartrepository.AddCart(cart);
                CartModel cartModel = new CartModel(response);
                return StatusCode(HttpStatusCode.OK.GetHashCode(), cartModel);
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);

            }
        }

        [HttpPut]
        [Route("update")]
        [ProducesResponseType(typeof(CartModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(NotFoundObjectResult), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
        public IActionResult UpdateCart(CartModel model)
        {
            try
            {
                if (model == null)
                {
                    return StatusCode(HttpStatusCode.BadRequest.GetHashCode(), "Please insert details properly!");
                }
                Cart cart = new Cart()
                {

                    Id = model.Id,
                    Bookid = model.Bookid,
                    Userid = model.Userid,
                    Quantity = model.Quantity,
                };

                var response = _cartrepository.UpdateCart(cart);
                if (response == null)
                {
                    return StatusCode(HttpStatusCode.NotFound.GetHashCode(), "Cart item not found!");
                }
                CartModel cartModel = new CartModel(response);
                return StatusCode(HttpStatusCode.OK.GetHashCode(), cartModel);
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);

            }
        }

        [HttpDelete]
        [Route("delete")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
        public IActionResult DeleteCategory(int id)
        {
            try
            {
                if (id > 0)
                {
                    var response = _cartrepository.DeleteCart(id);
                    return StatusCode(HttpStatusCode.OK.GetHashCode(), response);
                }
                return StatusCode(HttpStatusCode.BadRequest.GetHashCode(), "Please insert correct details!");
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);

            }
        }



    }
}