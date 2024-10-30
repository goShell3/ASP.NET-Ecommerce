public class Order {
    public int Id { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.Now;
    public List<Product> OrderItems { get; set; } = new List<Product>();
    public User User { get; set; } = null!;
}