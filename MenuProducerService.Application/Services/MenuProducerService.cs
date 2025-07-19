
using MenuProducerService.Application.DTOs;
using MenuProducerService.Application.Interfaces;
using MenuProducerService.Infrastructure.MessageBroker;
using MenuProducerService.Infrastructure.Security;

namespace MenuProducerService.Application.Services
{
    public class MenuProducerService : IMenuProducerService
    {
        private readonly IRabbitMQProducer _rabbitMqProducer;
        private readonly IAuthClient _authClient;

        public MenuProducerService(IRabbitMQProducer rabbitMqProducer, IAuthClient authClient)
        {
            _rabbitMqProducer = rabbitMqProducer;
            _authClient = authClient;
        }

        public async Task PublishMenuItemAsync(MenuItemRequest request, string token)
        {
            var isValid = await _authClient.ValidateTokenAsync(token);
            if (!isValid)
                throw new UnauthorizedAccessException("Invalid token");

            await _rabbitMqProducer.PublishAsync("menuProducer", request);
        }
    }
}
