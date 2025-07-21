
using MenuProducerService.Application.DTOs;
using MenuProducerService.Application.Interfaces;
using MenuProducerService.Domain.Entities;
using MenuProducerService.Infrastructure.MessageBroker;
using MenuProducerService.Infrastructure.Repository;
using MenuProducerService.Infrastructure.Security;

namespace MenuProducerService.Application.Services
{
    public class MenuProducerService : IMenuProducerService
    {
        private readonly IRabbitMQProducer _rabbitMqProducer;
        private readonly IAuthClient _authClient;
        private readonly IMenuRepository _menuRepository;

        public MenuProducerService(IRabbitMQProducer rabbitMqProducer, IAuthClient authClient, IMenuRepository menuRepository)
        {
            _rabbitMqProducer = rabbitMqProducer;
            _authClient = authClient;
            _menuRepository = menuRepository;
        }

        public async Task PublishMenuItemAsync(MenuItemRequest request, string token)
        {
            var isValid = await _authClient.ValidateTokenAsync(token);
            if (!isValid)
                throw new UnauthorizedAccessException("Invalid token");

            await _rabbitMqProducer.PublishAsync("menuProducer", request);
        }

        public async Task<MenuItem?> GetMenuItemByIdAsync(string id, string token)
        {
            var isValid = await _authClient.ValidateTokenAsync(token);
            if (!isValid)
                throw new UnauthorizedAccessException("Invalid token");

            return await _menuRepository.GetMenuItemByIdAsync(id);
        }

        public async Task<IEnumerable<MenuItem>?> GetAllMenuItemsAsync(string token)
        {
            var isValid = await _authClient.ValidateTokenAsync(token);
            if (!isValid)
                throw new UnauthorizedAccessException("Invalid token");

            return await _menuRepository.GetAllMenuItemsAsync();
        }         
    }
}
