
namespace MenuProducerService.Domain.Entities
{
    public class MenuItem
    {
        public ulong Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public byte MealTypeId { get; set; }
        public bool Available { get; set; }
        public string? Image_Url { get; set; }
        public List<string>? Tags { get; set; }
        public uint? Calories { get; set; }
        public DateTime Created_At { get; set; }
        public DateTime Updated_At { get; set; }
    }

}