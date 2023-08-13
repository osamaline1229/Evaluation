using iText.Kernel.Pdf;
using iText.Layout.Element;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class InvoicingSystem
{
    private ShopSettings shopSettings;
    private List<Item> items;
    private List<Invoice> invoices;
    private CreateNewInvoice createNewInvoice;

    public InvoicingSystem()
    {
        LoadData();
        createNewInvoice = new CreateNewInvoice(items);
    }

    private static int GetIntInput(string prompt)
    {
        Console.Write(prompt);
        int input;
        while (!int.TryParse(Console.ReadLine(), out input))
        {
            Console.Write("Invalid input. Please enter a valid integer: ");
        }
        return input;
    }

    private void LoadData()
    {
        if (File.Exists("shopSettings.json"))
        {
            string settingsJson = File.ReadAllText("shopSettings.json");
            shopSettings = JsonConvert.DeserializeObject<ShopSettings>(settingsJson);
        }
        else
        {
            shopSettings = new ShopSettings();
        }

        if (File.Exists("items.json"))
        {
            string itemsJson = File.ReadAllText("items.json");
            items = JsonConvert.DeserializeObject<List<Item>>(itemsJson);
        }
        else
        {
            items = new List<Item>();
        }

        if (File.Exists("invoices.json"))
        {
            string invoicesJson = File.ReadAllText("invoices.json");
            invoices = JsonConvert.DeserializeObject<List<Invoice>>(invoicesJson);
        }
        else
        {
            invoices = new List<Invoice>();
        }
    }

    private void SaveData()
    {
        string settingsJson = JsonConvert.SerializeObject(shopSettings, Formatting.Indented);
        File.WriteAllText("shopSettings.json", settingsJson);

        string itemsJson = JsonConvert.SerializeObject(items, Formatting.Indented);
        File.WriteAllText("items.json", itemsJson);

        string invoicesJson = JsonConvert.SerializeObject(invoices, Formatting.Indented);
        File.WriteAllText("invoices.json", invoicesJson);
    }
    public void CreateNewInvoice()
    {
        Invoice newInvoice = createNewInvoice.GenerateNewInvoice();
        invoices.Add(newInvoice);
        SaveData();
        Console.WriteLine("Invoice created successfully.");
    }
    



    public void ManageShopSettings()
    {
        Menu shopSettingsMenu = new Menu("Shop Settings Menu");
        shopSettingsMenu.AddMenuItem("Load Data (Items and invoices)");
        shopSettingsMenu.AddMenuItem("Set Shop Name");
        shopSettingsMenu.AddMenuItem("Set Invoice Header");
        shopSettingsMenu.AddMenuItem("Go Back");

        while (true)
        {
            shopSettingsMenu.Show();
            int choice = GetIntInput("Enter your choice: ");

            switch (choice)
            {
                case 1:
                    LoadData();
                    Console.WriteLine("Shop settings and data loaded.");
                    break;
                case 2:
                    shopSettings.ShopName = GetStringInput("Enter the new shop name: ");
                    SaveData();
                    Console.WriteLine("Shop name has been updated.");
                    break;
                case 3:
                    Console.Write("Enter telephone number: ");
                    string tel = Console.ReadLine();
                    Console.Write("Enter fax number: ");
                    string fax = Console.ReadLine();
                    Console.Write("Enter email: ");
                    string email = Console.ReadLine();
                    Console.Write("Enter website: ");
                    string website = Console.ReadLine();
                    shopSettings.InvoiceHeader = $"Tel: {tel} | Fax: {fax} | Email: {email} | Website: {website}";
                    SaveData();
                    Console.WriteLine("Invoice header has been updated.");
                    break;
                case 4:
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please select a valid option.");
                    break;
            }
        }
    }

    public void ManageShopItems()
    {
        Menu manageItemsMenu = new Menu("Manage Shop Items Menu");
        manageItemsMenu.AddMenuItem("Add Item");
        manageItemsMenu.AddMenuItem("Delete Item");
        manageItemsMenu.AddMenuItem("Change Item Price");
        manageItemsMenu.AddMenuItem("Report All Items");
        manageItemsMenu.AddMenuItem("Go Back");

        while (true)
        {
            manageItemsMenu.Show();
            int choice = GetIntInput("Enter your choice: ");

            switch (choice)
            {
                case 1:
                    AddItem();
                    break;
                case 2:
                    DeleteItem();
                    break;
                case 3:
                    ChangeItemPrice();
                    break;
                case 4:
                    ReportAllItems();
                    break;
                case 5:
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please select a valid option.");
                    break;
            }
        }
    }



    public void ReportStatistics()
    {
        int numberOfItems = items.Count;
        int numberOfInvoices = invoices.Count;
        decimal totalSales = 0;

        foreach (var invoice in invoices)
        {
            totalSales += invoice.TotalAmount;
        }

        Console.WriteLine($"Statistics:");
        Console.WriteLine($"Number of Items: {numberOfItems}");
        Console.WriteLine($"Number of Invoices: {numberOfInvoices}");
        Console.WriteLine($"Total Sales: {totalSales}");
    }

    public void ReportAllInvoices()
    {
        Console.WriteLine("All Invoices:");
        foreach (var invoice in invoices)
        {
            Console.WriteLine($"Invoice No: {invoices.IndexOf(invoice) + 1}");
            Console.WriteLine($"Invoice Date: {invoice.InvoiceDate}");
            Console.WriteLine($"Customer Name: {invoice.CustomerFullName}");
            Console.WriteLine($"Number of Items: {invoice.Items.Count}");
            Console.WriteLine($"Total: {invoice.TotalAmount}");
            Console.WriteLine($"Balance: {invoice.Balance}");
            Console.WriteLine();
        }
    }

    public void SearchInvoice()
    {
        int invoiceNumber = GetIntInput("Enter Invoice Number to search: ");
        if (invoiceNumber > 0 && invoiceNumber <= invoices.Count)
        {
            Invoice invoice = invoices[invoiceNumber - 1];
            Console.WriteLine($"Invoice No: {invoiceNumber}");
            Console.WriteLine($"Invoice Date: {invoice.InvoiceDate}");
            Console.WriteLine($"Customer Name: {invoice.CustomerFullName}");
            Console.WriteLine($"Phone Number: {invoice.PhoneNumber}");
            Console.WriteLine($"Number of Items: {invoice.Items.Count}");
            Console.WriteLine($"Total: {invoice.TotalAmount}");
            Console.WriteLine($"Paid Amount: {invoice.PaidAmount}");
            Console.WriteLine($"Balance: {invoice.Balance}");
            Console.WriteLine("Items:");

            foreach (var item in invoice.Items)
            {
                Console.WriteLine($"Item ID: {item.ItemId}");
                Console.WriteLine($"Item Name: {item.ItemName}");
                Console.WriteLine($"Unit Price: {item.UnitPrice}");
                Console.WriteLine($"Quantity: {item.Quantity}");
                Console.WriteLine($"Total Price: {item.TotalPrice}");
                Console.WriteLine();
            }
        }
        else
        {
            Console.WriteLine("Invoice not found.");
        }
    }

    public void ProgramStatistics()
    {
        Console.WriteLine("Program Statistics:");
        foreach (var menuItem in Enum.GetValues(typeof(MainMenuOption)))
        {
            MainMenuOption option = (MainMenuOption)menuItem;
            int count = GetOptionCount(option);
            Console.WriteLine($"{(int)option}. {option}: {count} times");
        }
    }

    private int GetOptionCount(MainMenuOption option)
    {
        int count = 0;
        foreach (var menuItem in Enum.GetValues(typeof(MainMenuOption)))
        {
            if ((MainMenuOption)menuItem == option)
            {
                count++;
            }
        }
        return count;
    }

    private void AddItem()
    {
        Item newItem = new Item();
        newItem.Id = GetIntInput("Enter Item ID: ");
        newItem.Name = GetStringInput("Enter Item Name: ");
        newItem.Price = GetDecimalInput("Enter Item Price: ");
        items.Add(newItem);
        SaveData();
        Console.WriteLine("Item added successfully!");
    }

    private void DeleteItem()
    {
        int itemId = GetIntInput("Enter Item ID to delete: ");
        Item itemToRemove = items.Find(i => i.Id == itemId);
        if (itemToRemove != null)
        {
            items.Remove(itemToRemove);
            SaveData();
            Console.WriteLine("Item deleted successfully!");
        }
        else
        {
            Console.WriteLine("Item not found.");
        }
    }

    private void ChangeItemPrice()
    {
        int itemId = GetIntInput("Enter Item ID to change price: ");
        Item itemToChange = items.Find(i => i.Id == itemId);
        if (itemToChange != null)
        {
            itemToChange.Price = GetDecimalInput("Enter new Item Price: ");
            SaveData();
            Console.WriteLine("Item price changed successfully!");
        }
        else
        {
            Console.WriteLine("Item not found.");
        }
    }

    private void ReportAllItems()
    {
        Console.WriteLine("All Items:");
        foreach (var item in items)
        {
            Console.WriteLine($"Item ID: {item.Id}");
            Console.WriteLine($"Item Name: {item.Name}");
            Console.WriteLine($"Item Price: {item.Price}");
            Console.WriteLine();
        }
    }

    private decimal GetDecimalInput(string prompt)
    {
        Console.Write(prompt);
        decimal input;
        while (!decimal.TryParse(Console.ReadLine(), out input))
        {
            Console.Write("Invalid input. Please enter a valid decimal: ");
        }
        return input;
    }

    private string GetStringInput(string prompt)
    {
        Console.Write(prompt);
        return Console.ReadLine();
    }


}
