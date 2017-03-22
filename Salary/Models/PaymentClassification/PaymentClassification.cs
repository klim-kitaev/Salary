using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Models
{
    public abstract class PaymentClassification
    {
        public abstract double CalculatePay(PayCheck paycheck);

        public bool IsInPayPeriod(DateTime theDate, PayCheck paycheck)
        {
            DateTime payPeriodEndDate = paycheck.PayPeriodEndDate;
            DateTime payPeriodStartDate = paycheck.PayPeriodStartDate;
            return (theDate >= payPeriodStartDate)
            && (theDate <= payPeriodEndDate);
        }
    }
}
