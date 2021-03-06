﻿using ChinookConsole.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
Create a console application that allows the user to run and view the results of the following queries:

1. Provide a query that shows the invoices associated with each sales agent. The resultant table should include the Sales Agent's full name.

2. Provide a query that shows the Invoice Total, Customer name, Country and Sale Agent name for all invoices.

3. Looking at the InvoiceLine table, provide a query that COUNTs the number of line items for an Invoice with a parameterized Id from user input

hint, this will use ExecuteScalar

4. INSERT a new invoice with parameters for customerid and billing address

5. UPDATE an Employee's name with a parameter for Employee Id and new name

Each query should be represented by a main menu item. Each parameter should be prompted for by name from the user.

Remember, it is helpful to create models for these sets of data so that you have a way to represent them in your code. Models do not have to be exactly representative of tables, but can also represent the results of queries.
*/

namespace ChinookConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var run = true;
            while (run)
            {
                ConsoleKeyInfo userInput = MainMenu();

                switch (userInput.KeyChar)
                {
                    case '0':
                        run = false;
                        break;
                    case '1':
                        Console.Clear();
                        var invoiceQuery = new InvoiceQuery();

                        var invoices = invoiceQuery.GetInvoicesBySalesRep();
                        var invoiceDetails = invoiceQuery.GetInvoiceDetails();

                        Console.WriteLine("Here are all of the invoice IDs associated with their sales reps");

                        foreach (var invoice in invoices)
                        {
                            Console.WriteLine($"Sales Rep: {invoice.Name}, Invoice ID: {invoice.InvoiceId}");
                        }
                        Console.WriteLine("press enter to continue.");
                        Console.ReadLine();
                        break;
                    case '2':
                        Console.Clear();
                        Console.WriteLine("Here are all of the invoices with their totals, and some other shit");

                        invoiceQuery = new InvoiceQuery();
                        invoices = invoiceQuery.GetInvoicesBySalesRep();
                        invoiceDetails = invoiceQuery.GetInvoiceDetails();

                        foreach (var detail in invoiceDetails)
                        {
                            Console.WriteLine($"Total: {detail.Total}, Sales Rep: {detail.SalesAgent}, Billing Country: {detail.BillingCountry}, Customer Name: {detail.CustomerName}");
                        }
                        Console.WriteLine("press enter to continue.");
                        Console.ReadLine();
                        break;
                    case '3':
                        Console.Clear();
                        Console.WriteLine("Please enter in an invoice ID to see the total number of line items for that invoice.");
                        var invoiceInput = Console.ReadLine();
                        invoiceQuery = new InvoiceQuery();
                        var lineItems = invoiceQuery.GetInvoiceLineItems(int.Parse(invoiceInput));
                        Console.WriteLine($"There are {lineItems} line items for invoice ID {invoiceInput}");
                        Console.WriteLine("press enter to continue.");
                        Console.ReadLine();
                        break;
                    case '4':
                        Console.Clear();
                        Console.WriteLine("Please enter your address to create a new invoice.");
                        var userAddress = Console.ReadLine();
                        Console.WriteLine("What is the customerID for this invoice?");
                        var customerID = Console.ReadLine();
                        var invoiceModifier = new InvoiceModifier();
                        var createInvoice = invoiceModifier.AddNewInvoice(userAddress, int.Parse(customerID));
                        if (createInvoice)
                        {
                            Console.WriteLine("Congratulations, you created a new invoice!");
                        }
                        Console.WriteLine("press enter to continue.");
                        Console.ReadLine();
                        break;
                    case '5':
                        Console.Clear();
                        Console.WriteLine("Please enter the Employee ID whose name you would like to change.");
                        var employeeId = Console.ReadLine();
                        Console.WriteLine("What would you like to change their name to?");
                        var newName = Console.ReadLine();
                        var employeeModifier = new EmployeeModifier();
                        var changeName = employeeModifier.UpdateEmployee(int.Parse(employeeId), newName);
                        if (changeName)
                        {
                            Console.WriteLine($"Congratulations, you updated the name of employee ID {employeeId} to {newName}!");
                        }
                        Console.WriteLine("press enter to continue.");
                        Console.ReadLine();
                        break;
                }
            }

            ConsoleKeyInfo MainMenu()
            {
                View mainMenu = new View()
                        .AddMenuOption("Show invoices associated with each sales agent.")
                        .AddMenuOption("See invoice data for all invoices.")
                        .AddMenuOption("See number of invoice line items for each invoice.")
                        .AddMenuOption("Add anew invoice.")
                        .AddMenuOption("Update an employee name.")
                        .AddMenuText("Press 0 to exit.");

                Console.Write(mainMenu.GetFullMenu());
                ConsoleKeyInfo userOption = Console.ReadKey();
                return userOption;
            }
        }
    }
}
