namespace MenuProducerService.Application.DTOs
{
    public class MenuItemRequest
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string MealType { get; set; } = string.Empty;
        public bool Available { get; set; } = true;
        public string Action { get; set; } = "CREATE"; // CREATE or UPDATE
    }
}