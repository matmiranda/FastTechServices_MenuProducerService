using Dapper;
using MenuProducerService.Domain.Entities;
using MenuProducerService.Infrastructure.Database;

namespace MenuProducerService.Infrastructure.Repository
{
    public class MenuRepository : IMenuRepository
    {
        private readonly DapperContext _context;
        public MenuRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<MenuItem?> GetMenuItemByIdAsync(string id)
        {
            using var connection = _context.CreateConnection();
            var sql = "SELECT * FROM menuitems where Id = @Id";
            return await connection.QueryFirstOrDefaultAsync<MenuItem>(sql, new { Id = id });
        }
        public async Task<IEnumerable<MenuItem>> GetAllMenuItemsAsync()
        {
            using var connection = _context.CreateConnection();
            var sql = "SELECT * FROM menuitems";
            return await connection.QueryAsync<MenuItem>(sql);
        }
    }
}
