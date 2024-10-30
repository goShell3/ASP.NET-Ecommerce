
public class Product {
    public Guid guid { get; set; }
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    // public Category Category { get; set; }
    public List<ProductImage> Images { get; set; } = new List<ProductImage>();
    // public List<ProductReview> Reviews { get; set; } = new List<ProductReview>();

}