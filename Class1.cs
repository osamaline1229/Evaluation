using System;
using System.Collections.Generic;

class Customer
{
    public int CustomerId { get; set; }
    public string FullName { get; set; }
    public string PhoneNumber { get; set; }
}

class Product
{
    public int ProductId { get; set; }
    public string Name { get; set; }
    public double UnitPrice { get; set; }
}

class InvoiceItem
{
    public int ItemId { get; set; }
    public string ItemName { get; set; }
    public double UnitPrice { get; set; }
    public int Quantity { get; set; }
    public double QuantityAmount { get { return UnitPrice * Quantity; } }
}