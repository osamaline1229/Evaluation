using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using System.Reflection.Metadata;
using iTextLayout = iText.Layout.Document;
using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

class Program
{
    static void Main(string[] args)
    {
        Console.Title = "Invoicing System";

        InvoicingSystem invoicingSystem = new InvoicingSystem();
        Menu mainMenu = new Menu("Application Main Menu");
        mainMenu.AddMenuItem("Shop Settings");
        mainMenu.AddMenuItem("Manage Shop Items");
        mainMenu.AddMenuItem("Create New Invoice");
        mainMenu.AddMenuItem("Report: Statistics");
        mainMenu.AddMenuItem("Report: All Invoices");
        mainMenu.AddMenuItem("Search (1) Invoice");
        mainMenu.AddMenuItem("Program Statistics");
        mainMenu.AddMenuItem("Exit");

        while (true)
        {
            Console.Clear();
            Console.WriteLine("╔═════════════════════════════════╗");
            Console.WriteLine("        Invoicing System Menu       ");
            Console.WriteLine("╚═════════════════════════════════╝");
            mainMenu.Show();
            int choice = GetIntInput("\nEnter your choice: ");

            switch (choice)
            {
                case 1:
                    invoicingSystem.ManageShopSettings();
                    break;
                case 2:
                    invoicingSystem.ManageShopItems();
                    break;
                case 3:
                    invoicingSystem.CreateNewInvoice();
                    break;
                case 4:
                    invoicingSystem.ReportStatistics();
                    break;
                case 5:
                    invoicingSystem.ReportAllInvoices();
                    break;
                case 6:
                    invoicingSystem.SearchInvoice();
                    break;
                case 7:
                    invoicingSystem.ProgramStatistics();
                    break;
                case 8:
                    if (ExitPrompt())
                    {
                        Console.Clear();
                        return;
                    }
                    break;
                default:
                    Console.WriteLine("\nInvalid choice. Please select a valid option.");
                    break;
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
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
   

    static bool ExitPrompt()
    {
        Console.Write("Are you sure you want to exit? (y/n): ");
        return Console.ReadLine().Trim().ToLower() == "y";
    }

    


}









enum MainMenuOption
{
    ShopSettings = 1,
    ManageShopItems,
    CreateNewInvoice,
    ReportStatistics,
    ReportAllInvoices,
    SearchInvoice,
    ProgramStatistics,
    Exit
}
