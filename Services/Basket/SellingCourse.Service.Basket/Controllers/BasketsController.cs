﻿using FreeCourses.Shared.ControllerBases;
using FreeCourses.Shared.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SellingCourse.Service.Basket.Dtos;
using SellingCourse.Service.Basket.Services;

namespace SellingCourse.Service.Basket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketsController : CustomBaseController
    {
        private readonly IBasketService _basketService;
        private readonly ISharedIdentityService _sharedIdentityService;

        public BasketsController(IBasketService basketService, ISharedIdentityService sharedIdentityService)
        {
            _basketService = basketService;
            _sharedIdentityService = sharedIdentityService;
        }
        [HttpGet]
        public async Task<IActionResult> GetBasket() => CreateActionResultInstance(await _basketService.GetBasket(_sharedIdentityService.GetUserId));
        [HttpPost]
        public async Task<IActionResult> SaveOrUpdateBasket(BasketDto basketDto)
        => CreateActionResultInstance(await _basketService.SaveOrUpdate(basketDto));
        [HttpDelete]
        public async Task<IActionResult> DeleteBasket() => CreateActionResultInstance(await _basketService.Delete(_sharedIdentityService.GetUserId));
 
    }
}
