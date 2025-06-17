using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using GerenciamentoDePedidos.Controllers;
using GerenciamentoDePedidos.Repositories;
using Moq;


namespace Tests
{
  public class CustomerControllerTests
  {
    [Fact]
    public async Task Index_ReturnsViewResult_WithCustomers()
    {
      // Arrange
      var mockCustomerRepo = new Mock<ICustomerRepository>();
      mockCustomerRepo.Setup(repo => repo.GetAllAsync(null!))
        .ReturnsAsync(new List<GerenciamentoDePedidos.Models.Customer>());
      var controller = new CustomerController(mockCustomerRepo.Object);

      // Act
      var result = await controller.Index(null!);

      // Assert
      var viewResult = Assert.IsType<ViewResult>(result);
      Assert.IsAssignableFrom<IEnumerable<GerenciamentoDePedidos.Models.Customer>>(viewResult.Model);
    }
  }
}