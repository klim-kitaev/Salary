using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Models
{
    public class BiweeklySchedule : PaymentSchedule
    {
        public override bool IsPayDate(DateTime payDate)
        {

            int days_count = DateTime.DaysInMonth(payDate.Year, payDate.Month);
            HashSet<DateTime> need_frides = new HashSet<DateTime>();
            bool use = true;

            for (int i = 1; i <= days_count; i++)
            {
                var day = new DateTime(payDate.Year, payDate.Month, i);
                if (day.DayOfWeek == DayOfWeek.Friday)
                {
                    if (use)
                    {
                        need_frides.Add(day);
                    }
                    use = !use;
                }
            }
            return need_frides.Contains(payDate);
        }
    }
}
