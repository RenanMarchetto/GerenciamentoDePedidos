using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using GerenciamentoDePedidos.Controllers;
using GerenciamentoDePedidos.Repositories;
using Moq;


namespace Tests
{
  public class OrderControllerTests
  {
    [Fact]
    public async Task Index_ReturnsViewResult_WithOrders()
    {
      // Arrange
      var mockOrderRepo = new Mock<IOrderRepository>();
      var mockCustomerRepo = new Mock<ICustomerRepository>();
      var mockProductRepo = new Mock<IProductRepository>();
      mockOrderRepo.Setup(repo => repo.GetAllAsync(null, null))
        .ReturnsAsync(new List<GerenciamentoDePedidos.Models.Order>());
      var controller = new OrderController(mockOrderRepo.Object, mockCustomerRepo.Object, mockProductRepo.Object);

      // Act
      var result = await controller.Index(null, null);

      // Assert
      var viewResult = Assert.IsType<ViewResult>(result);
      Assert.IsAssignableFrom<IEnumerable<GerenciamentoDePedidos.Models.Order>>(viewResult.Model);
    }
  }
}