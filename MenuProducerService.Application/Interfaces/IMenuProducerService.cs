using MenuProducerService.Application.DTOs;

namespace MenuProducerService.Application.Interfaces
{
    public interface IMenuProducerService
    {
        Task PublishMenuItemAsync(MenuItemRequest request, string token);
    }
}
