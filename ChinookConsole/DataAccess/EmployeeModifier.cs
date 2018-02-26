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
    class EmployeeModifier
    {
        readonly string _connectionString = ConfigurationManager.ConnectionStrings["Chinook"].ConnectionString;

        public bool UpdateEmployee(int employeeId, string employeeName)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var cmd = connection.CreateCommand();
                cmd.CommandText = @"UPDATE[dbo].[Employee]
                                   SET LastName = @lastName
                                      ,FirstName = @firstName  
                                 WHERE EmployeeId = @employeeID";

                var newName = SplitMyName(employeeName);

                connection.Open();

                var employeeIdToEdit = new SqlParameter("@employeeID", SqlDbType.Int);
                employeeIdToEdit.Value = employeeId;
                cmd.Parameters.Add(employeeIdToEdit);

                var newFirstName = new SqlParameter("@firstName", SqlDbType.NVarChar);
                newFirstName.Value = newName[0];
                cmd.Parameters.Add(newFirstName);

                var newLastName = new SqlParameter("@lastName", SqlDbType.NVarChar);
                newLastName.Value = newName[1];
                cmd.Parameters.Add(newLastName);

                var result = cmd.ExecuteNonQuery();

                return result == 1;
            }
        }

        string[] SplitMyName(string name)
        {
            var newEmployeeName = name.Split(' ');

            return newEmployeeName;
        }
    }
}
