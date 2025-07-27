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
        //[Authorize(Roles = "GERENTE")]
        public async Task<IActionResult> Post([FromBody] MenuItemRequest request)
        {
            return Ok(await _menuProducerService.PublishMenuItemCreateAsync(request));
        }

        [HttpPut]
        [Authorize]
        [Authorize(Roles = "GERENTE")]
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
            return await _menuProducerService.GetAllMenuItemsAsync();
        }
    }
}
