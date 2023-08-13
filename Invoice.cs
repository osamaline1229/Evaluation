using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Invoice
{
    public string CustomerFullName { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime InvoiceDate { get; set; }
    public List<InvoiceItem> Items { get; set; }
    public decimal TotalAmount
    {
        get
        {
            decimal total = 0;
            foreach (var item in Items)
            {
                total += item.TotalPrice;
            }
            return total;
        }
    }
    public decimal PaidAmount { get; set; }
    public decimal Balance { get { return TotalAmount - PaidAmount; } }
}
