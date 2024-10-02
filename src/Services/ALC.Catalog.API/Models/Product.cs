using ALC.Core.DomainObjects;

namespace ALC.Catalog.API.Models
{
    public class Product: Entity, IAggregationRoot
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public bool Active { get; set; }
        public DateTime CreatedAt { get; set; }
        public int StockQuantity { get; set; }
    }
}
