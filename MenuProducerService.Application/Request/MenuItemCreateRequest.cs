using MenuProducerService.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace MenuProducerService.Application.Request
{
    public class MenuItemCreateRequest
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [MaxLength(150, ErrorMessage = "O nome deve ter no máximo 150 caracteres.")]
        public required string Name { get; set; }

        [MaxLength(1000, ErrorMessage = "A descrição deve ter no máximo 1000 caracteres.")]
        public required string Description { get; set; }

        [Required(ErrorMessage = "O preço é obrigatório.")]
        [Range(0.00, double.MaxValue, ErrorMessage = "O preço deve ser um valor positivo.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "O tipo de refeição é obrigatório.")]
        [EnumDataType(typeof(MealTypeId), ErrorMessage = "Tipo de refeição inválido.")]
        public MealTypeId MealTypeId { get; set; }

        public bool Available { get; set; } = true;

        [Url(ErrorMessage = "A URL da imagem não é válida.")]
        [MaxLength(255, ErrorMessage = "A URL da imagem deve ter no máximo 255 caracteres.")]
        public required string ImageUrl { get; set; }

        public required List<string> Tags { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "As calorias devem ser um valor positivo.")]
        public int? Calories { get; set; }
    }
}