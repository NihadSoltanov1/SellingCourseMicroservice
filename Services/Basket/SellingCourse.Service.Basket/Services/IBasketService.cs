using FreeCourses.Shared.Dtos;
using SellingCourse.Service.Basket.Dtos;

namespace SellingCourse.Service.Basket.Services
{
    public interface IBasketService
    {
        Task<Response<BasketDto>> GetBasket(string userId);
        Task<Response<bool>> SaveOrUpdate(BasketDto basketDto);
        Task<Response<bool>> Delete(string userId);
    }
}
