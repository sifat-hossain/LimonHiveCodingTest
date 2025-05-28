using Limon.Hive.Frontend.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace Limon.Hive.Frontend.Pages.Products;

public class ProductModel : PageModel
{
    private readonly IHttpClientFactory _clientFactory;

    public List<Product> Products { get; set; }
    public List<Cart>? CartItems { get; set; }

    public ProductModel(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
    }

    public async Task OnGetAsync()
    {
        var client = _clientFactory.CreateClient();
        var response = await client.GetAsync("https://localhost:7183/api/Product");

        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            Products = JsonSerializer.Deserialize<List<Product>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

        // Temporary: simulate user cart
        LoadCartFromSession(); // Replace with actual cart retrieval logic
    }

    [BindProperty]
    public Product NewProduct { get; set; }

    public async Task<IActionResult> OnPostAddProductAsync()
    {
        NewProduct.Id = Guid.NewGuid();
        NewProduct.IsDeleted = false;

        var client = _clientFactory.CreateClient();
        var response = await client.PostAsJsonAsync("https://localhost:7183/api/Product", NewProduct);

        if (response.IsSuccessStatusCode)
        {
            TempData["SuccessMessage"] = "Product added successfully.";
            return RedirectToPage(); // Refresh the list
        }
        else
        {
            ModelState.AddModelError(string.Empty, "Failed to add product.");
            return Page();
        }
    }

    public async Task<IActionResult> OnPostAsync(Guid productId)
    {
        // Implement logic to add product to cart
        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostAddToCartAsync(Guid productId)
    {
        var client = _clientFactory.CreateClient();
        var response = await client.GetFromJsonAsync<Product>($"https://localhost:7183/api/Product/{productId}");

        if (response != null)
        {
            var cartJson = HttpContext.Session.GetString("Cart");
            List<Cart>? cart = string.IsNullOrEmpty(cartJson)
                ? new List<Cart>()
                : JsonSerializer.Deserialize<List<Cart>>(cartJson);

            if (!cart.Any(c => c.ProductId == productId))
            {
                cart.Add(new Cart
                {
                    Id = Guid.NewGuid(),
                    ProductId = productId,
                    Product = response,
                    FinalPrice = IsDiscounted(response) ? response.Price * 0.9m : response.Price,
                    CreatedDate = DateTime.UtcNow,
                    CustomerId = Guid.NewGuid()
                });

                HttpContext.Session.SetString("Cart", JsonSerializer.Serialize(cart));
            }
        }

        return RedirectToPage();
    }

    private static bool IsDiscounted(Product product)
    {
        return product.DiscountStartDate <= DateTime.UtcNow && DateTime.UtcNow <= product.DiscountEndDate;
    }

    private void LoadCartFromSession()
    {
        var cartJson = HttpContext.Session.GetString("Cart");

        if (!string.IsNullOrEmpty(cartJson))
        {
            CartItems = JsonSerializer.Deserialize<List<Cart>>(cartJson);
        }
    }
    private Guid GetCustomerId()
    {
        // Replace with actual user logic
        return Guid.Parse("11111111-1111-1111-1111-111111111111");
    }
}
