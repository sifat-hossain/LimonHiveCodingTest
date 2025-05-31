using Limon.Hive.Frontend.Entities;
using Limon.Hive.Frontend.Pages.Carts;
using Limon.Hive.Frontend.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;
using System.Text.Json;

namespace Limon.Hive.Frontend.Pages.Products;

public class ProductModel : PageModel
{

    private readonly HttpClient _client;

    private readonly IWebHostEnvironment _env;
    public ProductModel(IHttpClientFactory clientFactory, IWebHostEnvironment env)
    {
        _client = clientFactory.CreateClient("ApiClient");
        _env = env;
    }



    public List<Product> Products { get; set; }
    public int TotalProduct { get; set; }
    public int TotalProductQuatity { get; set; }

    [BindProperty(SupportsGet = true)]
    public int PageSize { get; set; } = 10;

    [BindProperty(SupportsGet = true)]
    public int PageNumber { get; set; } = 1;

    [BindProperty(SupportsGet = true)]
    public string? SearchQuery { get; set; }
    public async Task OnGetAsync()
    {
        int skip = (PageNumber - 1) * PageSize;

        HttpResponseMessage response = new();

        if (string.IsNullOrWhiteSpace(SearchQuery))
        {
            response = await _client.GetAsync($"Product?skip={skip}&take={PageSize}");
        }
        else
        {
            response = await _client.GetAsync($"Product/GetProductByName/{SearchQuery}");
        }

        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<ProductResponse>(json, options: new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            Products = result.Products;
            TotalProduct = result.TotalProductCount;
            TotalProductQuatity = result.TotalProductQuatity;
        }

        // Temporary: simulate user cart
        LoadCartFromSession(); // Replace with actual cart retrieval logic
    }

    [BindProperty]
    public Product NewProduct { get; set; }

    [BindProperty]
    public IFormFile ImageFile { get; set; }
    public async Task<IActionResult> OnPostAddProductAsync()
    {
        NewProduct.Id = Guid.NewGuid();
        NewProduct.IsDeleted = false;

        if (ImageFile != null)
        {
            var fileName = NewProduct.Id.ToString() + Path.GetExtension(ImageFile.FileName);
            var filePath = Path.Combine(_env.WebRootPath, "ProductImage", fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await ImageFile.CopyToAsync(stream);
            }

            NewProduct.ImageUrl = "/ProductImage/" + fileName;
        }

        var response = await _client.PostAsJsonAsync("Product", NewProduct);

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

    public List<Cart>? CartItems { get; set; }
    public async Task<IActionResult> OnPostAddToCartAsync(Guid productId, int quantity)
    {
        var response = await _client.GetFromJsonAsync<Product>($"Product/{productId}");

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
                    ProductQuantity = quantity,
                    FinalPrice = IsDiscounted(response) ? response.Price - (response.Price * 0.25M) : response.Price,
                    CreatedDate = DateTime.UtcNow,
                    CustomerId = Guid.NewGuid()
                });

                HttpContext.Session.SetString("Cart", JsonSerializer.Serialize(cart));
            }
        }

        return RedirectToPage();
    }

    public IActionResult OnPostRemoveFromCart(Guid productId)
    {

        var cartJson = HttpContext.Session.GetString("Cart");

        List<Cart> cart = JsonSerializer.Deserialize<List<Cart>>(cartJson);

        var itemToRemove = cart.FirstOrDefault(c => c.ProductId == productId);

        if (itemToRemove != null)
        {
            cart.Remove(itemToRemove);
            HttpContext.Session.SetString("Cart", JsonSerializer.Serialize<List<Cart>>(cart));
        }

        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostPlaceOrderAsync()
    {
        var cartJson = HttpContext.Session.GetString("Cart");

        List<Cart> cart = JsonSerializer.Deserialize<List<Cart>>(cartJson);

        var cartPayload = new CartRequest
        {
            CartCreateCommands = cart.Select(item => new CartCreateCommand
            {
                Id = Guid.NewGuid(),
                ProductId = item.Product.Id,
                FinalPrice = item.FinalPrice,
                ProductQuantity = item.ProductQuantity,
                CreatedDate = DateTime.UtcNow,
                CustomerId = Guid.Parse("b1604360-b2f6-4635-8afd-f262ba07a246")
            }).ToList()
        };

        var jsonContent = new StringContent(
            JsonSerializer.Serialize(cartPayload),
            Encoding.UTF8,
            "application/json");

        var response = await _client.PostAsync("Cart", jsonContent);

        if (response.IsSuccessStatusCode)
        {
            HttpContext.Session.Clear();
            TempData["Success"] = "Cart submitted successfully!";
        }
        else
        {
            TempData["Error"] = "Failed to submit cart.";
        }

        return RedirectToPage(); // Or redirect elsewhere
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
}
