using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Models
{
    public class BiweeklySchedule : PaymentSchedule
    {
        public override DateTime GetPayPeriodStartDate(DateTime payDate)
        {
            //найдем ближаюшую пятницу для оплаты
            var nearest_friday = GetNeedFridays(payDate).Where(p => p >= payDate).DefaultIfEmpty().Min();
            //Если таких дней не найдено, то вернем день после последней пятницы
            if (nearest_friday == default(DateTime))
            {
                return GetNeedFridays(payDate).Max().AddDays(1);
            }
            //а так выводим 
            return nearest_friday.AddDays(-13);
        }

        public override bool IsPayDate(DateTime payDate)
        {
            return GetNeedFridays(payDate).Contains(payDate);
        }

        private HashSet<DateTime> GetNeedFridays(DateTime payDate)
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
            return need_frides;
        }
    }
}
