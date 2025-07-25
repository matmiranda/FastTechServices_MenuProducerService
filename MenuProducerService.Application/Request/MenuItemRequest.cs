using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MenuProducerService.Application.Request
{
    public class MenuItemRequest
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        [Required(ErrorMessage = "O tipo de refeição é obrigatório.")]
        [RegularExpression("LANCHES|SOBREMESAS|BEBIDAS", ErrorMessage = "MealType deve ser LANCHES, SOBREMESAS ou BEBIDAS.")]
        public string MealType { get; set; } = string.Empty;
        public bool Available { get; set; } = true;
        public string? Action { get; set; }
        [JsonIgnore]
        public required string Token { get; set; }
    }
}