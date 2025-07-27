using Dapper;
using MenuProducerService.Domain.Entities;
using MenuProducerService.Infrastructure.Database;
using System.Text.Json;

namespace MenuProducerService.Infrastructure.Repository
{
    public class MenuRepository : IMenuRepository
    {
        private readonly DapperContext _context;
        public MenuRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<MenuItem?> GetMenuItemByIdAsync(long id)
        {
            using var connection = _context.CreateConnection();
            var sql = "SELECT * FROM menu_db.menu_items WHERE id = @Id";
            var rawItem = await connection.QueryFirstOrDefaultAsync<dynamic>(sql, new { Id = id });

            if (rawItem == null) return null;

            return new MenuItem
            {
                Id = rawItem.id,
                Name = rawItem.name,
                Description = rawItem.description,
                Price = rawItem.price,
                MealTypeId = rawItem.meal_type_id,
                Available = rawItem.available,
                Image_Url = rawItem.image_url,
                Calories = rawItem.calories,
                Created_At = rawItem.created_at,
                Updated_At = rawItem.updated_at,
                Tags = JsonSerializer.Deserialize<List<string>>(rawItem.tags ?? "[]")
            };
        }

        public async Task<IEnumerable<MenuItem>> GetAllMenuItemsAsync()
        {
            using var connection = _context.CreateConnection();
            var sql = "SELECT * FROM menu_db.menu_items";
            var rawItems = await connection.QueryAsync<dynamic>(sql);

            var items = new List<MenuItem>();

            foreach (var raw in rawItems)
            {
                items.Add(new MenuItem
                {
                    Id = raw.id,
                    Name = raw.name,
                    Description = raw.description,
                    Price = raw.price,
                    MealTypeId = raw.meal_type_id,
                    Available = raw.available,
                    Image_Url = raw.image_url,
                    Calories = raw.calories,
                    Created_At = raw.created_at,
                    Updated_At = raw.updated_at,
                    Tags = JsonSerializer.Deserialize<List<string>>(raw.tags ?? "[]")
                });
            }

            return items;
        }
    }
}
