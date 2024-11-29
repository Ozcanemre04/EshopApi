using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Eshop.Application.Dtos.Request.BasketProduct;
using Eshop.Application.Dtos.Request.Product;
using Eshop.Application.Dtos.Response.BasketProduct;
using Eshop.Application.Dtos.Response.Product;
using Eshop.Application.Interfaces.Repository;
using Eshop.Application.Interfaces.Service;
using Eshop.Domain.Entities;

namespace Eshop.Application.Services
{
    public class BasketProductService : IBasketProductService
    {

        private readonly IBasketProductRepository _basketProductRepository;
        private readonly IBasketRepository _basketRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        public BasketProductService(IBasketProductRepository basketProductRepository, IMapper mapper,
        IProductRepository productRepository, ICurrentUserService currentUserService, IBasketRepository basketRepository)
        {
            _basketProductRepository = basketProductRepository;
            _productRepository = productRepository;
            _currentUserService = currentUserService;
            _mapper = mapper;
            _basketRepository = basketRepository;

        }

        public async Task<BasketProductDtoResponse> CreateBasketProductAsync(BasketProductDtoCreateRequest basketProductDtoCreateRequest)
        {
            var user = _currentUserService.UserId;
            var product = await _productRepository.GetOneAsync(basketProductDtoCreateRequest.ProductId) ?? throw new KeyNotFoundException("product doesn't exist");
            var Basket = await _basketRepository.GetOneAsync(user) ?? throw new KeyNotFoundException("basket doesn't exist");
            var BasketProductExist = await _basketProductRepository.GetOneByBasketAndProductAsync(Basket.Id, product.Id);
            

            if (BasketProductExist != null)
            {
                BasketProductExist.Quantity++;
                BasketProductExist.TotalPrice = BasketProductExist.Quantity * product.Price;
                await _basketProductRepository.UpdateAsync(BasketProductExist);
                return _mapper.Map<BasketProductDtoResponse>(BasketProductExist);

            }
            var BasketProduct = _mapper.Map<BasketProduct>(basketProductDtoCreateRequest);
            BasketProduct.BasketId = Basket.Id;
            BasketProduct.TotalPrice = BasketProduct.Quantity * product.Price;
            await _basketProductRepository.CreateAsync(BasketProduct);
            return _mapper.Map<BasketProductDtoResponse>(BasketProduct);
        }


        public async Task<string> DeleteBasketProductAsync(long id)
        {
            var user = _currentUserService.UserId;
            var basketProduct = await _basketProductRepository.GetOneAsync(id)??throw new KeyNotFoundException("product is not found");
            if (basketProduct.Basket.UserId != user){
                throw new UnauthorizedAccessException("unauthorized");
            }
            return await _basketProductRepository.DeleteAsync(id) ? "product is deleted" : throw new KeyNotFoundException("product is not found");
        }

        public async Task<BasketDtoResponse> GetAllBasketProductAsync()
        {
            var user = _currentUserService.UserId;
            var BasketProductRepo = await _basketRepository.GetAllAsync(user);
            var dto = _mapper.Map<BasketDtoResponse>(BasketProductRepo);
            return dto;

        }


        public async Task<BasketProductDtoResponse> IncreaseQuantityAsync(long id)
        {
            var BasketProduct = await _basketProductRepository.GetOneAsync(id) ?? throw new KeyNotFoundException("the Product in Basket is not found");
            var product = await _productRepository.GetOneAsync(BasketProduct.ProductId) ?? throw new KeyNotFoundException("product doesn't exist");
            var user = _currentUserService.UserId;
            if (BasketProduct.Basket!.UserId != user)
            {
                throw new UnauthorizedAccessException("Unauthorized");
            }
            BasketProduct.Quantity++;
            BasketProduct.UpdatedDate = DateTime.UtcNow;
            BasketProduct.TotalPrice = BasketProduct.Quantity * product.Price;
            await _basketProductRepository.UpdateAsync(BasketProduct);
            return _mapper.Map<BasketProductDtoResponse>(BasketProduct);

        }
        public async Task<BasketProductDtoResponse> DecreaseQuantityAsync(long id)
        {
            var BasketProduct = await _basketProductRepository.GetOneAsync(id) ?? throw new KeyNotFoundException("the Product in Basket is not found");
            var product = await _productRepository.GetOneAsync(BasketProduct.ProductId) ?? throw new KeyNotFoundException("product doesn't exist");
            var user = _currentUserService.UserId;
            if (BasketProduct.Basket.UserId != user)
            {
                throw new UnauthorizedAccessException("Unauthorized");
            }
            BasketProduct.Quantity--;
            BasketProduct.UpdatedDate = DateTime.UtcNow;
            BasketProduct.TotalPrice = BasketProduct.Quantity * product.Price;
            if (BasketProduct.Quantity == 0)
            {
                await DeleteBasketProductAsync(BasketProduct.Id);
                return _mapper.Map<BasketProductDtoResponse>(BasketProduct);

            }
            else
            {
                await _basketProductRepository.UpdateAsync(BasketProduct);
                return _mapper.Map<BasketProductDtoResponse>(BasketProduct);

            }
        }
    }
}