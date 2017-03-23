using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Models
{
    public abstract class PaymentSchedule
    {
        public abstract bool IsPayDate(DateTime payDate);

        public abstract DateTime GetPayPeriodStartDate(DateTime payDate);
    }
}
