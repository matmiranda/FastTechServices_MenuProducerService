using MenuProducerService.Application.Request;
using Microsoft.AspNetCore.Mvc;

namespace MenuProducerService.Application.Interfaces
{
    public interface IMenuProducerService
    {
        Task<IActionResult> GetAllMenuItemsAsync();
        Task<IActionResult> PublishMenuItemCreateAsync(MenuItemCreateRequest request);
        Task<IActionResult> PublishMenuItemUpdateAsync(MenuItemUpdateRequest request);
        Task<IActionResult> GetMenuItemByIdAsync(long id);
        Task ValidateTokenAsync();        
    }
}
