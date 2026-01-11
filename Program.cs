using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ClientApp.Services;

builder.Services.AddScoped<ProductService>();

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
});

products = await Http.GetFromJsonAsync<Product[]>("/api/productlist");
private List<Product>? _cachedProducts;
public async Task<List<Product>> GetProductsAsync()
{
    if (_cachedProducts == null)
        _cachedProducts = await Http.GetFromJsonAsync<List<Product>>("products");
    return _cachedProducts;
}


// In-memory product list for demo purposes
var products = new List<Product>
{
    new Product { Id = 1, Name = "Laptop", Price = 1200.00m, Quantity = 10 },
    new Product { Id = 2, Name = "Mouse", Price = 25.50m, Quantity = 50 },
    new Product { Id = 3, Name = "Keyboard", Price = 45.00m, Quantity = 30 }
};

var app = builder.Build();
var products = await Http.GetFromJsonAsync<List<Product>>("products");

// Enable Swagger UI
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Minimal API endpoints
app.MapGet("/api/products", () =>
{
    return products;
});

app.MapGet("/api/products/{id}", (int id) =>
{
    var product = products.FirstOrDefault(p => p.Id == id);
    return product is not null ? Results.Ok(product) : Results.NotFound();
});

app.Run();

// Product model
record Product
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public decimal Price { get; init; }
    public int Quantity { get; init; }
}
// Define models
public record Category(int Id, string Name);
public record Product(int Id, string Name, double Price, int Stock, Category Category);

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Endpoint
app.MapGet("/api/productlist", () =>
{
    var products = new List<Product>
    {
        new Product(1, "Laptop", 1200.50, 25, new Category(101, "Electronics")),
        new Product(2, "Headphones", 50.00, 100, new Category(102, "Accessories"))
    };

    return Results.Json(products);
});

app.Run();

