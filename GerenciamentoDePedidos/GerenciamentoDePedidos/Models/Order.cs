using System;
using System.Collections.Generic;

namespace GerenciamentoDePedidos.Models
{
  public class Order
  {
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.Now;
    public decimal TotalAmount { get; set; }
    public string Status { get; set; } = "Novo";
    public Customer? Customer { get; set; }
    public List<OrderItem>? Items { get; set; } = new();
  }
}