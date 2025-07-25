using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MenuProducerService.Application.Interfaces;
using MenuProducerService.Application.Request;

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
            await _menuProducerService.PublishMenuItemCreateAsync(request);
            return Ok(new { message = "Item enviado com sucesso para a fila." });
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Put([FromBody] MenuItemRequest request)
        {
            await _menuProducerService.PublishMenuItemUpdateAsync(request);
            return Ok(new { message = "Item atualizado com sucesso na fila." });
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> Get(string id)
        {
            var result = await _menuProducerService.GetMenuItemByIdAsync(id);

            if (result == null)
                return NotFound(new { message = "Item não encontrado." });

            return Ok(result);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var result = await _menuProducerService.GetAllMenuItemsAsync();

            if (result == null || !result.Any())
                return NotFound(new { message = "Nenhum item encontrado." });

            return Ok(result);
        }

    }
}
