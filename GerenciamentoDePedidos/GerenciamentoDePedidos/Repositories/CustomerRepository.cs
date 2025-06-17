using GerenciamentoDePedidos.Data;
using GerenciamentoDePedidos.Models;
using Dapper;

namespace GerenciamentoDePedidos.Repositories
{
  public class CustomerRepository : ICustomerRepository
  {
    private readonly DapperContext _context;

    public CustomerRepository(DapperContext context)
    {
      _context = context;
    }

    public async Task<IEnumerable<Customer>> GetAllAsync(string searchTerm = null)
    {
      var query = "SELECT * FROM Customers";
      if (!string.IsNullOrEmpty(searchTerm))
        query += " WHERE Name LIKE @Term OR Email LIKE @Term";
      using var connection = _context.CreateConnection();
      return await connection.QueryAsync<Customer>(query, new { Term = $"%{searchTerm}%" });
    }

    public async Task<Customer> GetByIdAsync(int id)
    {
      var query = "SELECT * FROM Customers WHERE Id = @Id";
      using var connection = _context.CreateConnection();
      return await connection.QuerySingleOrDefaultAsync<Customer>(query, new { Id = id });
    }

    public async Task<int> AddAsync(Customer customer)
    {
      var query = @"INSERT INTO Customers (Name, Email, Phone, RegistrationDate)
                      VALUES (@Name, @Email, @Phone, @RegistrationDate);
                      SELECT CAST(SCOPE_IDENTITY() as int)";
      using var connection = _context.CreateConnection();
      return await connection.QuerySingleAsync<int>(query, customer);
    }

    public async Task<bool> UpdateAsync(Customer customer)
    {
      var query = @"UPDATE Customers SET Name=@Name, Email=@Email, Phone=@Phone WHERE Id=@Id";
      using var connection = _context.CreateConnection();
      var rows = await connection.ExecuteAsync(query, customer);
      return rows > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
      var query = "DELETE FROM Customers WHERE Id=@Id";
      using var connection = _context.CreateConnection();
      var rows = await connection.ExecuteAsync(query, new { Id = id });
      return rows > 0;
    }
  }
}
