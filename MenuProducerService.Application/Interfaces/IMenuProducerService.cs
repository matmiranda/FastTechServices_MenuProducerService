using MenuProducerService.Application.Request;
using MenuProducerService.Application.Response;
using MenuProducerService.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace MenuProducerService.Application.Interfaces
{
    public interface IMenuProducerService
    {
        Task<IActionResult> GetAllMenuItemsAsync();
        Task<MenuItemResponse> PublishMenuItemCreateAsync(MenuItemRequest request);

        Task<MenuItemResponse> PublishMenuItemUpdateAsync(MenuItemRequest request);
        Task<MenuItem?> GetMenuItemByIdAsync(string id);
        Task ValidateTokenAsync();        
    }
}
