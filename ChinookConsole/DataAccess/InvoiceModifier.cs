using ChinookConsole.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ChinookConsole.DataAccess
{
    class InvoiceModifier
    {
        readonly string _connectionString = ConfigurationManager.ConnectionStrings["Chinook"].ConnectionString;

        //bool AddNewInvoice(int customerId, string billingAddress, string BillingState, string Billing)
        //{
        //    using (var connection = new SqlConnection(_connectionString))
        //    {
        //        var cmd = connection.CreateCommand();
        //        cmd.CommandText = @"INSERT INTO Invoice
        //                            (InvoiceId
        //                           ,CustomerId
        //                           ,InvoiceDate
        //                           ,BillingAddress
        //                           ,BillingCity
        //                           ,BillingState
        //                           ,BillingCountry
        //                           ,BillingPostalCode
        //                           ,Total)
        //                           VALUES
        //                           (@invoiceId)";

        //        var invoiceQuery = new InvoiceQuery();

        //        var invoiceId = invoiceQuery.GetLastInvoice() + 1;

        //        var newInvoiceId = new SqlParameter("@invoiceId", SqlDbType.Int);
        //        newInvoiceId.Value = invoiceId;
        //        cmd.Parameters.Add(newInvoiceId);

        //        var addressInfo = ParseAddressInfo(billingAddress);

        //        //var InvoiceData = InvoiceInfo(addressInfo);
                

        //    }
        //}

        string[] ParseAddressInfo(string address)
        {
            var addresspieces = address.Split(new char[] {' ', ',' });

            return addresspieces;
        }

        //InvoiceData InvoiceInfo()
        //{

        //}
    }
}
