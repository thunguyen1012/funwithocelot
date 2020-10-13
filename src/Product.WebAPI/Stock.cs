using System;

namespace Inventory.WebAPI
{
    public class Stock
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}