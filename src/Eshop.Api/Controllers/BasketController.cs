using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eshop.Application.Dtos.Request.BasketProduct;
using Eshop.Application.Dtos.Response.BasketProduct;
using Eshop.Application.Dtos.Response.Commun;
using Eshop.Application.Interfaces.Service;
using Eshop.Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eshop.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = UserRoles.USER)]
    public class BasketController : ControllerBase
    {
        private readonly IBasketProductService _basketProductService;
        public BasketController(IBasketProductService basketProductService)
        {
            _basketProductService = basketProductService;

        }

        [HttpGet()]
        public async Task<ActionResult<BasketDtoResponse>> GetAllProductInBasketAsync()
        {
            try
            {
            return Ok(await _basketProductService.GetAllBasketProductAsync());
                
            }
            catch (Exception ex)
            {
                if(ex.Message.Contains("doesn't exist")){
                    return NotFound(ex.Message);
                }
                return BadRequest(ex.Message);
                
            }

        }

        [HttpDelete("{id:long}")]
        public async Task<ActionResult<MessageDto>> DeleteProductInBasketAsync([FromRoute] long id)
        {
            try
            {
                return Ok(await _basketProductService.DeleteBasketProductAsync(id));

            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("not found"))
                {
                    return NotFound(ex.Message);

                }
                
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("increase/{id:long}")]
        public async Task<ActionResult<BasketProductDtoResponse>> IncreaseQuantityProductInBasketAsync([FromRoute] long id)
        {
            try
            {
                return Ok(await _basketProductService.IncreaseQuantityAsync(id));

            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("not found")||ex.Message.Contains("doesn't exist"))
                {
                    return NotFound(ex.Message);

                }
                else if (ex.Message.Contains("Unauthorized")){
                    return Unauthorized(ex.Message);
                }
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("decrease/{id:long}")]
        public async Task<ActionResult<BasketProductDtoResponse>> DecreaseQuantityProductInBasketAsync([FromRoute] long id)
        {
            try
            {
                return Ok(await _basketProductService.DecreaseQuantityAsync(id));

            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("not found")||ex.Message.Contains("doesn't exist"))
                {
                    return NotFound(ex.Message);

                }
                else if (ex.Message.Contains("Unauthorized")){
                    return Unauthorized(ex.Message);
                }
                return BadRequest(ex.Message);
            }
        }
        [HttpPost()]
        public async Task<ActionResult<BasketProductDtoResponse>> CreateProductInBasketAsync([FromBody] BasketProductDtoCreateRequest basketProductDtoCreateRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                return Ok(await _basketProductService.CreateBasketProductAsync(basketProductDtoCreateRequest));

            }
            catch (Exception ex)
            {
                
                 if(ex.Message.Contains("exist")){
                    return NotFound(ex.Message);
                }
                return BadRequest(ex.Message);
            }
        }
    }
}