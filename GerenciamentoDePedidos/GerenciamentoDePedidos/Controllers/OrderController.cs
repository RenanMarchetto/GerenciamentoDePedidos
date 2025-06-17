using GerenciamentoDePedidos.Models;
using GerenciamentoDePedidos.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GerenciamentoDePedidos.Controllers
{
  /// <summary>
  /// Handles order management actions.
  /// </summary>
  public class OrderController : Controller
  {
    private readonly IOrderRepository _orderRepo;
    private readonly ICustomerRepository _customerRepo;
    private readonly IProductRepository _productRepo;

    public OrderController(IOrderRepository orderRepo, ICustomerRepository customerRepo, IProductRepository productRepo)
    {
      _orderRepo = orderRepo;
      _customerRepo = customerRepo;
      _productRepo = productRepo;
    }

    /// <summary>
    /// Lists orders with optional filtering.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> Index(int? customerId, string status)
    {
      ViewBag.Customers = await _customerRepo.GetAllAsync();
      ViewBag.Status = status;
      ViewBag.CustomerId = customerId;
      var orders = await _orderRepo.GetAllAsync(customerId, status);
      return View(orders);
    }

    /// <summary>
    /// Shows the create order form.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> Create()
    {
      ViewBag.Customers = await _customerRepo.GetAllAsync();
      ViewBag.Products = await _productRepo.GetAllAsync();
      return View();
    }

    /// <summary>
    /// Handles order creation.
    /// </summary>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Order order, string items)
    {
      if (!string.IsNullOrEmpty(items))
      {
        order.Items = Newtonsoft.Json.JsonConvert.DeserializeObject<List<OrderItem>>(items);
      }

      order.TotalAmount = order.Items?.Sum(i => i.UnitPrice * i.Quantity) ?? 0;

      if (order.Items == null || !order.Items.Any())
      {
        ModelState.AddModelError("", "Add at least one product.");
      }

      // Log model errors for debugging
      foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
      {
        // Consider using a logger in production
        Console.WriteLine("Model Error: " + error.ErrorMessage);
      }

      if (ModelState.IsValid)
      {
        try
        {
          order.OrderDate = DateTime.Now;
          order.Status = "Novo";
          await _orderRepo.AddAsync(order);
          return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
          ModelState.AddModelError("", ex.Message);
        }
      }

      ViewBag.Customers = await _customerRepo.GetAllAsync();
      ViewBag.Products = await _productRepo.GetAllAsync();
      return View(order);
    }

    /// <summary>
    /// Shows order details.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
      var order = await _orderRepo.GetByIdAsync(id);
      if (order == null) return NotFound();
      return View(order);
    }

    /// <summary>
    /// Updates the status of an order.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> UpdateStatus(int id, string status)
    {
      await _orderRepo.UpdateStatusAsync(id, status);
      return RedirectToAction(nameof(Details), new { id });
    }
  }
}