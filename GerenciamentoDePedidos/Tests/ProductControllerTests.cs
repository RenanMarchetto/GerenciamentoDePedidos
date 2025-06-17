using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using GerenciamentoDePedidos.Controllers;
using GerenciamentoDePedidos.Repositories;
using Moq;


namespace Tests
{
  public class ProductControllerTests
  {
    [Fact]
    public async Task Index_ReturnsViewResult_WithProducts()
    {
      // Arrange
      var mockProductRepo = new Mock<IProductRepository>();
      mockProductRepo.Setup(repo => repo.GetAllAsync(null!))
        .ReturnsAsync(new List<GerenciamentoDePedidos.Models.Product>());
      var controller = new ProductController(mockProductRepo.Object);

      // Act
      var result = await controller.Index(null!);

      // Assert
      var viewResult = Assert.IsType<ViewResult>(result);
      Assert.IsAssignableFrom<IEnumerable<GerenciamentoDePedidos.Models.Product>>(viewResult.Model);
    }
  }
}