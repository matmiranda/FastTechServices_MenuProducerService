// MenuProducerService.Api/Controllers/MenuProducerController.cs
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
            await _menuProducerService.PublishMenuItemAsync(request, token);
            return Ok(new { message = "Item enviado com sucesso para a fila." });
        }
    }
}
