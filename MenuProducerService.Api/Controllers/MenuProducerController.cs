using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MenuProducerService.Application.DTOs;
using MenuProducerService.Application.Interfaces;

namespace MenuProducerService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MenuProducerController : ControllerBase
    {
        private readonly IMenuProducerService _menuProducerService;

        public MenuProducerController(IMenuProducerService menuProducerService)
        {
            _menuProducerService = menuProducerService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] MenuItemRequest request)
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            request.Action = "CREATE"; // <-- Define ação para criação
            await _menuProducerService.PublishMenuItemAsync(request, token);
            return Ok(new { message = "Item enviado com sucesso para a fila." });
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "GERENTE")]
        public async Task<IActionResult> Put(Guid id, [FromBody] MenuItemRequest request)
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            request.Id = id;
            request.Action = "UPDATE"; // <-- Define ação para atualização
            await _menuProducerService.PublishMenuItemAsync(request, token);
            return Ok(new { message = "Item atualizado com sucesso na fila." });
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "GERENTE")]
        public async Task<IActionResult> Get(string id)
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            var result = await _menuProducerService.GetMenuItemByIdAsync(id, token);

            if (result == null)
                return NotFound(new { message = "Item não encontrado." });

            return Ok(result);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            var result = await _menuProducerService.GetAllMenuItemsAsync(token);

            if (result == null || !result.Any())
                return NotFound(new { message = "Nenhum item encontrado." });

            return Ok(result);
        }

    }
}
