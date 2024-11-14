namespace api.DTOs.Property
{
    public class DisplaySimplePropertyDTO
    {
        public string Id { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Slug { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Address { get; set; } = null!;
        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool ForSale { get; set; }
        public bool ForRent { get; set; }
        public int Bedrooms { get; set; }
        public int Bathrooms { get; set; }
        public bool IsFurnished { get; set; }
        public int Area { get; set; } = 0;
        public int YearOfConstruction { get; set; }
        public string OwnerId { get; set; } = null!;
        public string OwnerName { get; set; } = null!;
    }
}