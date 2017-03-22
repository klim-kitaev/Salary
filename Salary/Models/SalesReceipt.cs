using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Models
{
    public class SalesReceipt
    {
        private readonly DateTime date;
        private readonly double amount;

        public DateTime Date
        {
            get
            {
                return date;
            }
        }

        public double Amount
        {
            get
            {
                return amount;
            }
        }

        public SalesReceipt(DateTime date, double amount)
        {
            this.amount = amount;
            this.date = date;
        }
    }
}
