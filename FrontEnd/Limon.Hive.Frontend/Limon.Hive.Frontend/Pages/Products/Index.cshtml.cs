using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Limon.Hive.Frontend.Pages.Products;

public class ProductModel : PageModel
{
    public List<Product> Products { get; set; }
    public int CartCount { get; set; } = 4;

    public void OnGet()
    {
        Products = new List<Product>
    {
        new Product { Name = "DJI Phantom 2 Vision+", Price = 499, OriginalPrice = 699, Quantity = 2, ImageUrl = "/images/phantom2.jpg" },
        new Product { Name = "DJI Phantom 4 Multispectral", Price = 1449, ImageUrl = "/images/phantom4multi.jpg" },
        new Product { Name = "DJI Phantom 1 Vision", Price = 739, OriginalPrice = 899, ImageUrl = "/images/phantom1.jpg" },
        new Product { Name = "DJI Phantom 4 PRO", Price = 399, ImageUrl = "/images/phantom4pro.jpg" },
        new Product { Name = "4 Series – Intelligent Flight Battery", Price = 78, ImageUrl = "/images/battery1.jpg" },
        new Product { Name = "4 Series – Intelligent Flight Battery", Price = 98, ImageUrl = "/images/battery2.jpg" },
        new Product { Name = "DJI Phantom 4 PRO", Price = 739, ImageUrl = "/images/phantom4pro2.jpg" },
        new Product { Name = "4 Series – Amazing Flight Battery (5s)", Price = 799, ImageUrl = "/images/battery3.jpg" },
    };
    }
}
public class Product
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public decimal OriginalPrice { get; set; }
    public int Quantity { get; set; } = 0;
    public string ImageUrl { get; set; }
}
