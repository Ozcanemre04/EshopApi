using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eshop.Application.Dtos.Request.Review;
using Eshop.Application.Dtos.Response.Review;

namespace Eshop.Application.Interfaces.Service
{
    public interface IReviewService
    {
        Task<IEnumerable<ReviewDtoResponse>> GetAllReviewsAsync();
        Task<ReviewDtoResponse> CreateReviewAsync(ReviewDtoCreateRequest reviewDtoCreateRequest);
        Task<string> DeleteReviewAsync(long id);
        Task<ReviewDtoResponse> UpdateReviewAsync(ReviewDtoUpdateRequest reviewDtoUpdateRequest,long id);
    }
}