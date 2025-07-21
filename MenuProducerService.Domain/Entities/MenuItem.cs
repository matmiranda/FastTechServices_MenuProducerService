
namespace MenuProducerService.Domain.Entities
{
    public class MenuItem
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal Price { get; set; }

        // ENUM no banco: 'LANCHES', 'SOBREMESAS', 'BEBIDAS'
        public string MealType { get; set; } = "LANCHES";

        public bool Available { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}