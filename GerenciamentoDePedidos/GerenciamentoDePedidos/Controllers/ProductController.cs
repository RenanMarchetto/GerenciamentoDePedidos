using GerenciamentoDePedidos.Models;
using GerenciamentoDePedidos.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GerenciamentoDePedidos.Controllers
{
  /// <summary>
  /// Handles product management actions.
  /// </summary>
  public class ProductController : Controller
  {
    private readonly IProductRepository _repository;

    public ProductController(IProductRepository repository)
    {
      _repository = repository;
    }

    /// <summary>
    /// Lists and searches products.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> Index(string search)
    {
      var products = await _repository.GetAllAsync(search);
      ViewBag.Search = search;
      return View(products);
    }

    /// <summary>
    /// Shows product details.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
      var product = await _repository.GetByIdAsync(id);
      if (product == null) return NotFound();
      return View(product);
    }

    /// <summary>
    /// Shows the create product form.
    /// </summary>
    [HttpGet]
    public IActionResult Create()
    {
      return View();
    }

    /// <summary>
    /// Handles product creation.
    /// </summary>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Product product)
    {
      if (ModelState.IsValid)
      {
        await _repository.AddAsync(product);
        return RedirectToAction(nameof(Index));
      }
      return View(product);
    }

    /// <summary>
    /// Shows the edit product form.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
      var product = await _repository.GetByIdAsync(id);
      if (product == null) return NotFound();
      return View(product);
    }

    /// <summary>
    /// Handles product editing.
    /// </summary>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Product product)
    {
      if (id != product.Id) return BadRequest();
      if (ModelState.IsValid)
      {
        await _repository.UpdateAsync(product);
        return RedirectToAction(nameof(Index));
      }
      return View(product);
    }

    /// <summary>
    /// Shows the delete confirmation page.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
      var product = await _repository.GetByIdAsync(id);
      if (product == null) return NotFound();
      return View(product);
    }

    /// <summary>
    /// Handles product deletion.
    /// </summary>
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
      await _repository.DeleteAsync(id);
      return RedirectToAction(nameof(Index));
    }
  }
}