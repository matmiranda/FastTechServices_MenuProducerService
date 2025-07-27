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
        public async Task<IActionResult> Post([FromBody] MenuItemCreateRequest request)
        {
            return await _menuProducerService.PublishMenuItemCreateAsync(request);
        }

        [HttpPut]
        //[Authorize(Roles = "GERENTE")]
        public async Task<IActionResult> Put([FromBody] MenuItemUpdateRequest request)
        {
            return await _menuProducerService.PublishMenuItemUpdateAsync(request);
        }

        [HttpGet("{id}")]
        //[Authorize]
        public async Task<IActionResult> Get(long id)
        {
            return await _menuProducerService.GetMenuItemByIdAsync(id);
        }

        [HttpGet]
        //[Authorize]
        public async Task<IActionResult> GetAll()
        {
            return await _menuProducerService.GetAllMenuItemsAsync();
        }
    }
}
