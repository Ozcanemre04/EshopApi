using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eshop.Application.Dtos.Request.Product;
using Eshop.Application.Dtos.Response.Commun;
using Eshop.Application.Dtos.Response.Product;
using Eshop.Application.Interfaces.Service;
using Eshop.Application.Validations.Products;
using Eshop.Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eshop.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet()]
        public async Task<ActionResult<PageDto<ProductDtoResponse>>> GetAllProducts([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {

            try
            {
                return Ok(await _productService.GetAllProductAsync(pageNumber, pageSize));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id:long}")]
        public async Task<ActionResult<ProductDtoResponse>> GetOneProduct([FromRoute] long id)
        {
            try
            {
                return Ok(await _productService.GetOneProductAsync(id));

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
        [HttpDelete("{id:long}")]
        [Authorize(Roles = UserRoles.ADMIN)]
        public async Task<ActionResult<ProductDtoResponse>> DeleteProduct([FromRoute] long id)
        {
            try
            {
                return Ok(await _productService.DeleteProductAsync(id));

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

        [HttpPost]
        [Authorize(Roles = UserRoles.ADMIN)]
        public async Task<ActionResult<ProductDtoResponse>> CreateProduct([FromBody] ProductDtoCreateRequest productDtoCreateRequest)
        {
            try
            {
                var validator = new ProductDtoCreateValidation();
                var ValidationResult = await validator.ValidateAsync(productDtoCreateRequest);
                if (!ValidationResult.IsValid)
                {
                    return BadRequest(ValidationResult.Errors);
                }
                return Ok(await _productService.CreateProductAsync(productDtoCreateRequest));
            }
            catch (Exception ex)
            {
                if(ex.Message.Contains("exist")){
                    return NotFound(ex.Message);
                }
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id:long}")]
        [Authorize(Roles = UserRoles.ADMIN)]
        public async Task<ActionResult<ProductDtoResponse>> UpdateProduct([FromBody] ProductDtoUpdateRequest productDtoUpdateRequest, [FromRoute] long id)
        {
            try
            {
                var validator = new ProductDtoUpdateValidation();
                var ValidationResult = await validator.ValidateAsync(productDtoUpdateRequest);
                if (!ValidationResult.IsValid)
                {
                    return BadRequest(ValidationResult.Errors);
                }
                return Ok(await _productService.UpdateProductAsync(productDtoUpdateRequest, id));
            }
            catch (Exception ex)
            {
                if(ex.Message.Contains("not found")){
                    return NotFound(ex.Message);
                }
                return BadRequest(ex.Message);
            }
        }
    }
}