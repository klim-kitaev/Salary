using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Models
{
    public class WeeklySchedule : PaymentSchedule
    {
        public override DateTime GetPayPeriodStartDate(DateTime payDate)
        {
            //Вырнем первый субботу до текущей даты
            for (int i = 1; i <= 7; i++)
            {
                if (payDate.DayOfWeek == DayOfWeek.Saturday)
                    return payDate;
                payDate = payDate.AddDays(-1);
            }
            return payDate;
        }

        public override bool IsPayDate(DateTime payDate)
        {
            return payDate.DayOfWeek == DayOfWeek.Friday;
        }
    }
}
