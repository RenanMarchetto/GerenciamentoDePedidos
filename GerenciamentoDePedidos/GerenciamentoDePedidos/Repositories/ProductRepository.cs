using GerenciamentoDePedidos.Data;
using GerenciamentoDePedidos.Models;
using Dapper;

namespace GerenciamentoDePedidos.Repositories
{
  public class ProductRepository : IProductRepository
  {
    private readonly DapperContext _context;

    public ProductRepository(DapperContext context)
    {
      _context = context;
    }

    public async Task<IEnumerable<Product>> GetAllAsync(string searchTerm = null)
    {
      var query = "SELECT * FROM Products";
      if (!string.IsNullOrEmpty(searchTerm))
        query += " WHERE Name LIKE @Term";
      using var connection = _context.CreateConnection();
      return await connection.QueryAsync<Product>(query, new { Term = $"%{searchTerm}%" });
    }

    public async Task<Product> GetByIdAsync(int id)
    {
      var query = "SELECT * FROM Products WHERE Id = @Id";
      using var connection = _context.CreateConnection();
      return await connection.QuerySingleOrDefaultAsync<Product>(query, new { Id = id });
    }

    public async Task<int> AddAsync(Product product)
    {
      var query = @"INSERT INTO Products (Name, Description, Price, StockQuantity)
                          VALUES (@Name, @Description, @Price, @StockQuantity);
                          SELECT CAST(SCOPE_IDENTITY() as int)";
      using var connection = _context.CreateConnection();
      return await connection.QuerySingleAsync<int>(query, product);
    }

    public async Task<bool> UpdateAsync(Product product)
    {
      var query = @"UPDATE Products SET Name=@Name, Description=@Description, Price=@Price, StockQuantity=@StockQuantity WHERE Id=@Id";
      using var connection = _context.CreateConnection();
      var rows = await connection.ExecuteAsync(query, product);
      return rows > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
      var query = "DELETE FROM Products WHERE Id=@Id";
      using var connection = _context.CreateConnection();
      var rows = await connection.ExecuteAsync(query, new { Id = id });
      return rows > 0;
    }
  }
}