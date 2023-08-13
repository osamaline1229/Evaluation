using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class CreateNewInvoice
{
    private List<Item> items;

    public CreateNewInvoice(List<Item> items)
    {
        this.items = items;
    }

    public Invoice GenerateNewInvoice()
    {
        Invoice newInvoice = new Invoice();

        Console.Write("Enter customer full name: ");
        newInvoice.CustomerFullName = Console.ReadLine();

        Console.Write("Enter phone number: ");
        newInvoice.PhoneNumber = Console.ReadLine();

        newInvoice.InvoiceDate = DateTime.Now;

        List<InvoiceItem> invoiceItems = new List<InvoiceItem>();

        while (true)
        {
            InvoiceItem newItem = new InvoiceItem();

            Console.Write("Enter Item ID: ");
            newItem.ItemId = int.Parse(Console.ReadLine());

            Item item = items.Find(i => i.Id == newItem.ItemId);
            if (item == null)
            {
                Console.WriteLine("Item not found.");
                continue;
            }

            newItem.ItemName = item.Name;
            newItem.UnitPrice = item.Price;

            Console.Write("Enter quantity: ");
            newItem.Quantity = int.Parse(Console.ReadLine());

            invoiceItems.Add(newItem);

            Console.Write("Add another item? (y/n): ");
            string addAnother = Console.ReadLine().Trim().ToLower();
            if (addAnother != "y")
                break;
        }

        newInvoice.Items = invoiceItems;

        Console.Write("Enter paid amount: ");
        newInvoice.PaidAmount = decimal.Parse(Console.ReadLine());

        return newInvoice;
    }
}

