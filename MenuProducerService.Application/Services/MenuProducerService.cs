using MenuProducerService.Application.Interfaces;
using MenuProducerService.Application.Request;
using MenuProducerService.Domain.Entities;
using MenuProducerService.Infrastructure.MessageBroker;
using MenuProducerService.Infrastructure.Repository;
using MenuProducerService.Infrastructure.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace MenuProducerService.Application.Services
{
    public class MenuProducerService : IMenuProducerService
    {
        private readonly IRabbitMQProducer _rabbitMqProducer;
        private readonly IAuthClient _authClient;
        private readonly IMenuRepository _menuRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MenuProducerService(
            IRabbitMQProducer rabbitMqProducer, 
            IAuthClient authClient, 
            IMenuRepository menuRepository, 
            IHttpContextAccessor httpContextAccessor)
        {
            _rabbitMqProducer = rabbitMqProducer;
            _authClient = authClient;
            _menuRepository = menuRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task PublishMenuItemCreateAsync(MenuItemRequest request)
        {
            request.Action = "MENU_ITEM_REGISTERED";

            await ValidateTokenAsync();

            await _rabbitMqProducer.PublishAsync(QueueNames.MenuItemRegistered, request);
        }

        public async Task PublishMenuItemUpdateAsync(MenuItemRequest request)
        {
            request.Action = "MENU_ITEM_UPDATE";

            await ValidateTokenAsync();

            await _rabbitMqProducer.PublishAsync(QueueNames.MenuItemUpdated, request);
        }

        public async Task<MenuItem?> GetMenuItemByIdAsync(string id)
        {
            await ValidateTokenAsync();

            return await _menuRepository.GetMenuItemByIdAsync(id);
        }

        public async Task<IEnumerable<MenuItem>?> GetAllMenuItemsAsync()
        {
            await ValidateTokenAsync();

            return await _menuRepository.GetAllMenuItemsAsync();
        }

        public async Task ValidateTokenAsync()
        {
            var headers = _httpContextAccessor.HttpContext?.Request?.Headers;

            if (headers == null || !headers.TryGetValue("Authorization", out var token))
                throw new UnauthorizedAccessException("Token não encontrado no header.");

            if (!token.ToString().StartsWith("Bearer "))
                throw new UnauthorizedAccessException("Formato inválido do token.");

            var cleanToken = token.ToString().Replace("Bearer ", "");
            var isValid = await _authClient.ValidateTokenAsync(cleanToken);

            if (!isValid)
                throw new UnauthorizedAccessException("Token inválido.");
        }

    }
}
