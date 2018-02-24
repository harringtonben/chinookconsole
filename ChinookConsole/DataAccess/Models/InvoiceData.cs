using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChinookConsole.DataAccess.Models
{
    class InvoiceData
    {
        public double Total { get; set; }
        public string SalesAgent { get; set; }
        public string BillingCountry { get; set; }
        public string CustomerName { get; set; }
    }
}
