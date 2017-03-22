using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Models
{
    public class ServiceCharge
    {
        private readonly double amount;
        private readonly DateTime dateTime;

        public ServiceCharge(DateTime dateTime, double amount)
        {
            this.dateTime = dateTime;
            this.amount = amount;
        }

        public double Amount
        {
            get
            {
                return amount;
            }
        }

        public DateTime DateTime
        {
            get
            {
                return dateTime;
            }
        }
    }
}
