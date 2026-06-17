using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Eshop.Application.Dtos.Request.Review;
using Eshop.Application.Dtos.Response.Commun;
using Eshop.Application.Dtos.Response.Review;
using Eshop.Application.Interfaces.Repository;
using Eshop.Application.Interfaces.Service;
using Eshop.Domain.Entities;

namespace Eshop.Application.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;
        private readonly ICurrentUserService _currentUserService;

        public ReviewService(IReviewRepository reviewRepository, IMapper mapper,
         IProductRepository productRepository, ICurrentUserService currentUserService)
        {
            _reviewRepository = reviewRepository;
            _mapper = mapper;
            _productRepository = productRepository;
            _currentUserService = currentUserService;
        }
        public async Task<ReviewDtoResponse> CreateReviewAsync(ReviewDtoCreateRequest reviewDtoCreateRequest)
        {
            var user = _currentUserService.UserId;
            var product = await _productRepository.GetOneAsync(reviewDtoCreateRequest.ProductId) ?? throw new KeyNotFoundException("product doesn't exist");
            var review = _mapper.Map<Review>(reviewDtoCreateRequest);
            review.UserId = user;
            await _reviewRepository.CreateAsync(review);
            return _mapper.Map<ReviewDtoResponse>(review);
        }

        public async Task<MessageDto> DeleteReviewAsync(long id)
        {

            var user = _currentUserService.UserId;
            var review = await _reviewRepository.GetOneAsync(id) ?? throw new KeyNotFoundException("review is not found");
            if (user != review.UserId)
            {
                throw new UnauthorizedAccessException("Unauthorized");
            }
            else
            {
                return await _reviewRepository.DeleteAsync(id) ? new MessageDto{Message="review is deleted"} : throw new KeyNotFoundException("review is not found");
            }
        }

        public async Task<IEnumerable<ReviewDtoResponse>> GetAllReviewsAsync(long productId)
        {
            var reviews = await _reviewRepository.GetAllAsync(productId);
            return reviews.Select(review => _mapper.Map<ReviewDtoResponse>(review)).ToList();
        }

        public async Task<ReviewDtoResponse> UpdateReviewAsync(ReviewDtoUpdateRequest reviewDtoUpdateRequest, long id)
        {
            var user = _currentUserService.UserId;
            var review = await _reviewRepository.GetOneAsync(id) ?? throw new KeyNotFoundException("review is not found");
            if (user != review.UserId)
            {
                throw new UnauthorizedAccessException("you can't edit other user review");
            }
            review.UpdatedDate = DateTime.UtcNow;
            _mapper.Map(reviewDtoUpdateRequest, review);
            await _reviewRepository.UpdateAsync(review);
            return _mapper.Map<ReviewDtoResponse>(review);
        }
    }
}