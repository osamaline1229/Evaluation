using iText.Kernel.Pdf;
using iText.Layout.Element;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class InvoiceItem
{
    public int ItemId { get; set; }
    public string ItemName { get; set; }
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
    public decimal TotalPrice { get { return UnitPrice * Quantity; } }



}

