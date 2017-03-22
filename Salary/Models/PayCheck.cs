using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Models
{
    public class PayCheck
    {
        public PayCheck(DateTime startDate, DateTime payDate)
        {
            PayPeriodEndDate = payDate;
            PayPeriodStartDate = startDate;
        }

        public double Deductions { get; set; }
        public double GrossPay { get; set; }
        public double NetPay { get; set; }
        public DateTime PayPeriodEndDate { get; set; }
        public DateTime PayPeriodStartDate { get; set; }

        public string GetField(string v)
        {
            return "Hold";
        }
    }
}
