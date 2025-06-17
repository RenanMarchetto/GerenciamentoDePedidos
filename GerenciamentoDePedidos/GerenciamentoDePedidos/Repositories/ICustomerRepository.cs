using GerenciamentoDePedidos.Models;

namespace GerenciamentoDePedidos.Repositories
{
  public interface ICustomerRepository
  {
    Task<IEnumerable<Customer>> GetAllAsync(string searchTerm = null);
    Task<Customer> GetByIdAsync(int id);
    Task<int> AddAsync(Customer customer);
    Task<bool> UpdateAsync(Customer customer);
    Task<bool> DeleteAsync(int id);
  }
}
