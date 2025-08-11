namespace StokTakipProjesi2.Models
{
    public class Stock
    {
        public int StockID { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Category { get; set; } = null!;
        public int StockCount { get; set; }
        public decimal Price { get; set; }
        public string Owner { get; set; } = null!; 
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

    }
}
