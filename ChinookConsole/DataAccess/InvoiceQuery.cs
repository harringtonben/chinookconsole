using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChinookConsole.DataAccess
{
    class InvoiceQuery
    {
        readonly string _connectionString = ConfigurationManager.ConnectionStrings["Chinook"].ConnectionString;

        public List<Employee> GetInvoicesBySalesRep()
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

                var employees = new List<Employee>();

                while (reader.Read())
                {
                    var employee = new Employee
                    {
                        Name = reader["Name"].ToString(),
                        InvoiceId = int.Parse(reader["InvoiceId"].ToString())
                    };

                    employees.Add(employee);
                }
                return employees;
            }
        }
    }

    internal class Employee
    {
        public string Name { get; set; }
        public int InvoiceId { get; set; }
    }
}
