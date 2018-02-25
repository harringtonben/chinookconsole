using ChinookConsole.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChinookConsole.DataAccess
{
    class InvoiceQuery
    {
        readonly string _connectionString = ConfigurationManager.ConnectionStrings["Chinook"].ConnectionString;

        public List<SalesRep> GetInvoicesBySalesRep()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var cmd = connection.CreateCommand();
                cmd.CommandText = @"select e.firstname + ' ' + e.lastname as Name, i.InvoiceId from Employee e
                                    join customer c on c.SupportRepId = e.EmployeeId
                                    join invoice i on i.CustomerId = c.CustomerId
                                    where e.title = 'Sales Support Agent'";

                connection.Open();
                var reader = cmd.ExecuteReader();

                var employees = new List<SalesRep>();

                while (reader.Read())
                {
                    var employee = new SalesRep
                    {
                        Name = reader["Name"].ToString(),
                        InvoiceId = int.Parse(reader["InvoiceId"].ToString())
                    };

                    employees.Add(employee);
                }
                return employees;
            }
        }

        public List<InvoiceData> GetInvoiceDetails()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var cmd = connection.CreateCommand();
                cmd.CommandText = @"select i.Total, e.firstname + ' ' + e.lastname as [Sales Agent], i.BillingCountry, c.FirstName + ' ' +                        c.LastName as [Customer Name] 
                                    from Employee e
                                    join customer c on c.SupportRepId = e.EmployeeId
                                    join invoice i on i.CustomerId = c.CustomerId
                                    where e.title = 'Sales Support Agent'";

                connection.Open();
                var reader = cmd.ExecuteReader();

                var invoiceData = new List<InvoiceData>();

                while (reader.Read())
                {
                    var invoice = new InvoiceData
                    {
                        Total = double.Parse(reader["Total"].ToString()),
                        SalesAgent = reader["Sales Agent"].ToString(),
                        BillingCountry = reader["BillingCountry"].ToString(),
                        CustomerName = reader["Customer Name"].ToString()
                    };

                    invoiceData.Add(invoice);
                }

                return invoiceData;
            }
        }

        public int GetInvoiceLineItems(int invoiceIdInput)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var cmd = connection.CreateCommand();
                cmd.CommandText = @"select count(*) as [Total Number of Line Items] from InvoiceLine
                                    where InvoiceId = @invoiceId";

                var invoiceId = new SqlParameter("@invoiceId", SqlDbType.Int);
                invoiceId.Value = invoiceIdInput;
                cmd.Parameters.Add(invoiceId);

                connection.Open();

                var invoiceLineCount = (int)cmd.ExecuteScalar();

                return invoiceLineCount;
            }
        }
    }
}
