namespace ShopDomain.Models;

public class Product
{
    private static int _nextId = 0;
    public int Id { get; set; }
    public string? Title { get; set; }
    public float Price { get; set; }
    public Product(string? title, float price)
    {
        Title = title;
        Price = price;
        Id = _nextId++;
    }

}
