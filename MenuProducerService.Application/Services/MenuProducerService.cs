using MenuProducerService.Application.Interfaces;
using MenuProducerService.Application.Request;
using MenuProducerService.Application.Response;
using MenuProducerService.Infrastructure.MessageBroker;
using MenuProducerService.Infrastructure.Repository;
using MenuProducerService.Infrastructure.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MenuProducerService.Application.Services
{
    public class MenuProducerService : IMenuProducerService
    {
        private readonly IRabbitMQProducer _rabbitMqProducer;
        private readonly IAuthClient _authClient;
        private readonly IMenuRepository _menuRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string MessageSent = "Item enviado com sucesso para a fila.";

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

        public async Task<IActionResult> PublishMenuItemCreateAsync(MenuItemCreateRequest request)
        {
            await ValidateTokenAsync();

            await _rabbitMqProducer.PublishAsync(QueueNames.MenuItemRegistered, request);

            return new OkObjectResult(new MenuItemResponse
            {
                Message = MessageSent
            });
        }

        public async Task<IActionResult> PublishMenuItemUpdateAsync(MenuItemUpdateRequest request)
        {
            await ValidateTokenAsync();

            await _rabbitMqProducer.PublishAsync(QueueNames.MenuItemUpdated, request);

            return new OkObjectResult(new MenuItemResponse
            {
                Message = MessageSent
            });
        }

        public async Task<IActionResult> GetMenuItemByIdAsync(long id)
        {
            await ValidateTokenAsync();

            var item = await _menuRepository.GetMenuItemByIdAsync(id);

            if (item == null)
                return new NotFoundObjectResult(new { message = "Item não encontrado." });

            return new OkObjectResult(item);
        }

        public async Task<IActionResult> GetAllMenuItemsAsync()
        {
            await ValidateTokenAsync();

            var items = await _menuRepository.GetAllMenuItemsAsync();

            if (items == null || !items.Any())
                return new NotFoundObjectResult(new { message = "Nenhum item encontrado." });

            return new OkObjectResult(items);
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
