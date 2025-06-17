using GerenciamentoDePedidos.Data;
using GerenciamentoDePedidos.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(); // Use MVC (not just API controllers)
builder.Services.AddScoped<DapperContext>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

var app = builder.Build();

// Configure middleware
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// Set default route to Main/Index
app.MapControllerRoute(
  name: "default",
  pattern: "{controller=Main}/{action=Index}/{id?}");

app.Run();
