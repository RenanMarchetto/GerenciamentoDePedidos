using GerenciamentoDePedidos.Models;

namespace GerenciamentoDePedidos.Repositories
{
  public interface IProductRepository
  {
    Task<IEnumerable<Product>> GetAllAsync(string searchTerm = null);
    Task<Product> GetByIdAsync(int id);
    Task<int> AddAsync(Product product);
    Task<bool> UpdateAsync(Product product);
    Task<bool> DeleteAsync(int id);
  }
}