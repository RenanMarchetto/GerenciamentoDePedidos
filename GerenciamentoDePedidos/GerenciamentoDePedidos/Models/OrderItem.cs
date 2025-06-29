﻿using System.Runtime.CompilerServices;

namespace GerenciamentoDePedidos.Models
{
  public class OrderItem
  {
    public int Id { get; set; }
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public Product Product { get; set; }
    public decimal Total => Quantity * UnitPrice;
  }
}