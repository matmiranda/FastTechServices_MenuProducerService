using MenuProducerService.Application.Request;
using MenuProducerService.Domain.Entities;
using Microsoft.Extensions.Primitives;

namespace MenuProducerService.Application.Interfaces
{
    public interface IMenuProducerService
    {
        Task PublishMenuItemCreateAsync(MenuItemRequest request);
        Task PublishMenuItemUpdateAsync(MenuItemRequest request);
        Task<MenuItem?> GetMenuItemByIdAsync(string id);
        Task<IEnumerable<MenuItem>?> GetAllMenuItemsAsync();
        Task ValidateTokenAsync();
    }
}
