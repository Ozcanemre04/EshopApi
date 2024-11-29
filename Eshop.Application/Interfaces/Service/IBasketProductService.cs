

using Eshop.Application.Dtos.Request.BasketProduct;
using Eshop.Application.Dtos.Response.BasketProduct;

namespace Eshop.Application.Interfaces.Service
{
    public interface IBasketProductService
    {
        Task<BasketDtoResponse> GetAllBasketProductAsync();
        Task<BasketProductDtoResponse> CreateBasketProductAsync(BasketProductDtoCreateRequest basketProductDtoCreateRequest);
        Task<string> DeleteBasketProductAsync(long id);
        Task<BasketProductDtoResponse> IncreaseQuantityAsync(long id);
        Task<BasketProductDtoResponse> DecreaseQuantityAsync(long id);
    }
}