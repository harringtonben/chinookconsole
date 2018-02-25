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

        bool AddNewInvoice(string billingAddress)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var cmd = connection.CreateCommand();
                cmd.CommandText = @"INSERT INTO Invoice
                                    (InvoiceId
                                   ,CustomerId
                                   ,InvoiceDate
                                   ,BillingAddress
                                   ,BillingCity
                                   ,BillingState
                                   ,BillingCountry
                                   ,BillingPostalCode
                                   ,Total)
                                   VALUES
                                   (@invoiceId,
                                    @customerId,
                                    2/28/2018,
                                    @billingAddress,
                                    @billingCity,
                                    @billingState,
                                    'USA',
                                    @billingZip,
                                    0)";

                connection.Open();

                var invoiceQuery = new InvoiceQuery();

                var invoiceId = invoiceQuery.GetLastInvoice() + 1;
                var newCustomerId = invoiceQuery.GetLastCustomerId() + 1;

                var addressInfo = ParseAddressInfo(billingAddress);

                var InvoiceData = InvoiceInfo(addressInfo);

                var newInvoiceId = new SqlParameter("@invoiceId", SqlDbType.Int);
                newInvoiceId.Value = invoiceId;
                cmd.Parameters.Add(newInvoiceId);

                var customerId = new SqlParameter("@customerId", SqlDbType.Int);
                customerId.Value = newCustomerId;
                cmd.Parameters.Add(customerId);

                var billingAddy = new SqlParameter("@billingAddress", SqlDbType.NVarChar);
                billingAddy.Value = InvoiceData.BillingAddress;
                cmd.Parameters.Add(billingAddy);

                var billingCity = new SqlParameter("@billingCity", SqlDbType.NVarChar);
                billingCity.Value = InvoiceData.BillingCity;
                cmd.Parameters.Add(billingCity);

                var billingState = new SqlParameter("@billingState", SqlDbType.NVarChar);
                billingState.Value = InvoiceData.BillingState;
                cmd.Parameters.Add(billingState);

                var billingZip = new SqlParameter("@billingZip", SqlDbType.NVarChar);
                billingZip.Value = InvoiceData.BillingPostalCode;
                cmd.Parameters.Add(billingZip);

                var result = cmd.ExecuteNonQuery();

                return result == 1;

            }
        }

        string[] ParseAddressInfo(string address)
        {
            var addresspieces = address.Split(new char[] {' ', ',' });

            return addresspieces;
        }

        InvoiceData InvoiceInfo(string[] addressString)
        {
            var addressInfo = new InvoiceData();
            var counter = 1;
            foreach (var item in addressString)
            {
                if (counter == 1)
                {
                    addressInfo.BillingPostalCode = item;
                    counter++;
                }
                if (counter == 2)
                {
                    addressInfo.BillingState = item;
                    counter++;
                }
                if (counter == 3)
                {
                    addressInfo.BillingCity = item;
                    counter++;
                }
                else
                {
                    addressInfo.BillingAddress += item + " ";
                }
            }

            return addressInfo;
        }
    }
}
