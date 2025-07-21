using MenuProducerService.Application.DTOs;
using MenuProducerService.Domain.Entities;

namespace MenuProducerService.Application.Interfaces
{
    public interface IMenuProducerService
    {
        Task PublishMenuItemAsync(MenuItemRequest request, string token);
        Task<MenuItem?> GetMenuItemByIdAsync(string id, string token);
        Task<IEnumerable<MenuItem>?> GetAllMenuItemsAsync(string token);
    }
}
