using GerenciamentoDePedidos.Data;
using GerenciamentoDePedidos.Models;
using Dapper;

namespace GerenciamentoDePedidos.Repositories
{
  public class OrderRepository : IOrderRepository
  {
    private readonly DapperContext _context;

    public OrderRepository(DapperContext context)
    {
      _context = context;
    }

    public async Task<IEnumerable<Order>> GetAllAsync(int? customerId = null, string status = null)
    {
      var query = @"SELECT o.*, c.Id, c.Name, c.Email, c.Phone, c.RegistrationDate
                          FROM Orders o
                          INNER JOIN Customers c ON o.CustomerId = c.Id
                          WHERE (@CustomerId IS NULL OR o.CustomerId = @CustomerId)
                            AND (@Status IS NULL OR o.Status = @Status)
                          ORDER BY o.OrderDate DESC";
      using var connection = _context.CreateConnection();
      var orderDict = new Dictionary<int, Order>();
      var orders = await connection.QueryAsync<Order, Customer, Order>(
          query,
          (order, customer) =>
          {
            order.Customer = customer;
            return order;
          },
          new { CustomerId = customerId, Status = status }
      );
      return orders;
    }

    public async Task<Order> GetByIdAsync(int id)
    {
      var query = @"SELECT o.*, c.Id, c.Name, c.Email, c.Phone, c.RegistrationDate
                          FROM Orders o
                          INNER JOIN Customers c ON o.CustomerId = c.Id
                          WHERE o.Id = @Id;
                          SELECT oi.*, p.Id, p.Name, p.Description, p.Price, p.StockQuantity
                          FROM OrderItems oi
                          INNER JOIN Products p ON oi.ProductId = p.Id
                          WHERE oi.OrderId = @Id;";
      using var connection = _context.CreateConnection();
      using var multi = await connection.QueryMultipleAsync(query, new { Id = id });
      var order = multi.Read<Order, Customer, Order>((o, c) => { o.Customer = c; return o; }).FirstOrDefault();
      if (order != null)
        order.Items = multi.Read<OrderItem, Product, OrderItem>((oi, p) => { oi.Product = p; return oi; }).ToList();
      return order;
    }

    public async Task<int> AddAsync(Order order)
    {
      var queryOrder = @"INSERT INTO Orders (CustomerId, OrderDate, TotalAmount, Status)
                               VALUES (@CustomerId, @OrderDate, @TotalAmount, @Status);
                               SELECT CAST(SCOPE_IDENTITY() as int);";
      var queryItem = @"INSERT INTO OrderItems (OrderId, ProductId, Quantity, UnitPrice)
                              VALUES (@OrderId, @ProductId, @Quantity, @UnitPrice);";
      using var connection = _context.CreateConnection();
      connection.Open();
      using var transaction = connection.BeginTransaction();
      try
      {
        var orderId = await connection.QuerySingleAsync<int>(queryOrder, order, transaction);
        foreach (var item in order.Items)
        {
          item.OrderId = orderId;
          await connection.ExecuteAsync(queryItem, item, transaction);

          // Optional: Decrement stock
          var updateStock = "UPDATE Products SET StockQuantity = StockQuantity - @Quantity WHERE Id = @ProductId AND StockQuantity >= @Quantity";
          var affected = await connection.ExecuteAsync(updateStock, new { item.ProductId, item.Quantity }, transaction);
          if (affected == 0)
            throw new Exception("Insufficient stock for product " + item.ProductId);
        }
        transaction.Commit();
        return orderId;
      }
      catch
      {
        transaction.Rollback();
        throw;
      }
    }

    public async Task<bool> UpdateStatusAsync(int orderId, string status)
    {
      var query = "UPDATE Orders SET Status = @Status WHERE Id = @OrderId";
      using var connection = _context.CreateConnection();
      var rows = await connection.ExecuteAsync(query, new { OrderId = orderId, Status = status });
      return rows > 0;
    }
  }
}