using MenuProducerService.Domain.Entities;

namespace MenuProducerService.Infrastructure.Repository
{
    public interface IMenuRepository
    {
        Task<MenuItem?> GetMenuItemByIdAsync(string id);
        Task<IEnumerable<MenuItem>> GetAllMenuItemsAsync();
    }
}
