
using Eshop.Application.Dtos.Request.Review;
using Eshop.Application.Dtos.Response.Review;
using Eshop.Application.Interfaces.Service;
using Eshop.Application.Validations.Reviews;
using Eshop.Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eshop.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;
        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpGet()]
        public async Task<ActionResult<IEnumerable<ReviewDtoResponse>>> GetAllReviews()
        {

            return Ok(await _reviewService.GetAllReviewsAsync());
        }

        [HttpDelete("{id:long}")]
        [Authorize(Roles = UserRoles.USER)]
        public async Task<ActionResult<ReviewDtoResponse>> DeleteReview([FromRoute] long id)
        {
            try
            {
                return Ok(await _reviewService.DeleteReviewAsync(id));

            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("exist"))
                {
                    return NotFound(ex.Message);

                }
                else if(ex.Message.Contains("Unauthorized")){
                    return Unauthorized(ex.Message);
                }
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.USER)]
    public async Task<ActionResult<ReviewDtoResponse>> CreateReview([FromBody] ReviewDtoCreateRequest reviewDtoCreateRequest)
        {
            try
            {
                var validator = new ReviewDtoCreateValidation();
                var ValidationResult = await validator.ValidateAsync(reviewDtoCreateRequest);
                if (!ValidationResult.IsValid)
                {
                    return BadRequest(ValidationResult.Errors);
                }
                return Ok(await _reviewService.CreateReviewAsync(reviewDtoCreateRequest));
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("exist"))
                {
                    return NotFound(ex.Message);
                }
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id:long}")]
        [Authorize(Roles = UserRoles.USER)]
        public async Task<ActionResult<ReviewDtoResponse>> UpdateReview([FromBody] ReviewDtoUpdateRequest reviewDtoUpdateRequest, [FromRoute] long id)
        {
            try
            {
                var validator = new ReviewDtoUpdateValidation();
                var ValidationResult = await validator.ValidateAsync(reviewDtoUpdateRequest);
                if (!ValidationResult.IsValid)
                {
                    return BadRequest(ValidationResult.Errors);
                }
                return Ok(await _reviewService.UpdateReviewAsync(reviewDtoUpdateRequest, id));
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("you can't edit other user review"))
                {
                    return Unauthorized(ex.Message);
                }
                if (ex.Message.Contains("found"))
                {
                    return NotFound(ex.Message);
                }
                return BadRequest(ex.Message);
            }
        }
    }
}