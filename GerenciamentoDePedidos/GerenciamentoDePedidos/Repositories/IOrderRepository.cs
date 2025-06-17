using GerenciamentoDePedidos.Models;

namespace GerenciamentoDePedidos.Repositories
{
  public interface IOrderRepository
  {
    Task<IEnumerable<Order>> GetAllAsync(int? customerId = null, string status = null);
    Task<Order> GetByIdAsync(int id);
    Task<int> AddAsync(Order order);
    Task<bool> UpdateStatusAsync(int orderId, string status);
  }
}