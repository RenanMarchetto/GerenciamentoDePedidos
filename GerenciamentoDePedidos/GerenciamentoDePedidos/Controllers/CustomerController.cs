using GerenciamentoDePedidos.Models;
using GerenciamentoDePedidos.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GerenciamentoDePedidos.Controllers
{
  /// <summary>
  /// Handles customer management actions.
  /// </summary>
  public class CustomerController : Controller
  {
    private readonly ICustomerRepository _repository;

    public CustomerController(ICustomerRepository repository)
    {
      _repository = repository;
    }

    /// <summary>
    /// Lists and searches customers.
    /// </summary>
    public async Task<IActionResult> Index(string search)
    {
      var customers = await _repository.GetAllAsync(search);
      ViewBag.Search = search;
      return View(customers);
    }

    /// <summary>
    /// Shows customer details.
    /// </summary>
    public async Task<IActionResult> Details(int id)
    {
      var customer = await _repository.GetByIdAsync(id);
      if (customer == null) return NotFound();
      return View(customer);
    }

    /// <summary>
    /// Shows the create customer form.
    /// </summary>
    [HttpGet]
    public IActionResult Create()
    {
      return View();
    }

    /// <summary>
    /// Handles customer creation.
    /// </summary>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Customer customer)
    {
      if (ModelState.IsValid)
      {
        customer.RegistrationDate = DateTime.Now;
        await _repository.AddAsync(customer);
        return RedirectToAction(nameof(Index));
      }
      return View(customer);
    }

    /// <summary>
    /// Shows the edit customer form.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
      var customer = await _repository.GetByIdAsync(id);
      if (customer == null) return NotFound();
      return View(customer);
    }

    /// <summary>
    /// Handles customer editing.
    /// </summary>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Customer customer)
    {
      if (id != customer.Id) return BadRequest();
      if (ModelState.IsValid)
      {
        await _repository.UpdateAsync(customer);
        return RedirectToAction(nameof(Index));
      }
      return View(customer);
    }

    /// <summary>
    /// Shows the delete confirmation page.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
      var customer = await _repository.GetByIdAsync(id);
      if (customer == null) return NotFound();
      return View(customer);
    }

    /// <summary>
    /// Handles customer deletion.
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
