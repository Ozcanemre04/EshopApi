using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eshop.Application.Dtos.Request.Category;
using Eshop.Application.Dtos.Response.Category;
using Eshop.Application.Interfaces.Service;
using Eshop.Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eshop.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet()]
        public async Task<ActionResult<IEnumerable<CategoryDtoResponse>>> GetAllCategories()
        {

            return Ok(await _categoryService.GetAllCategoriesAsync());
        }

        [HttpGet("{id:long}")]
        public async Task<ActionResult<CategoryDtoResponse>> GetOneCategory([FromRoute] long id)
        {
            try
            {
                return Ok(await _categoryService.GetOneCategoryAsync(id));

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
        public async Task<ActionResult<string>> DeleteCategory([FromRoute] long id)
        {
            try
            {
                return Ok(await _categoryService.DeleteCategoryAsync(id));

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
        public async Task<ActionResult<CategoryDtoResponse>> CreateCategory([FromBody] CategoryDtoCreateRequest categoryDtoCreateRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                return Ok(await _categoryService.CreateCategoryAsync(categoryDtoCreateRequest));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id:long}")]
        [Authorize(Roles = UserRoles.ADMIN)]
        public async Task<ActionResult<CategoryDtoResponse>> UpdateCategory([FromBody] CategoryDtoUpdateRequest categoryDtoUpdateRequest, [FromRoute] long id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                return Ok(await _categoryService.UpdateCategoryAsync(categoryDtoUpdateRequest, id));
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("found")){
                    return NotFound(ex.Message);
                }
                return BadRequest(ex.Message);
            }
        }
    }
}