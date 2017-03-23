using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Models
{
    public class HourlyClassification:PaymentClassification
    {
        private double hourlyRate;
        private HashSet<TimeCard> timecards;

        public HourlyClassification(double hourlyRate)
        {
            this.hourlyRate = hourlyRate;
            timecards = new HashSet<TimeCard>();
        }

        public double HourlyRate
        {
            get
            {
                return hourlyRate;
            }
        }

        internal void AddTimeCard(TimeCard timeCard)
        {
            timecards.Add(timeCard);
        }

        public TimeCard GetTimeCard(DateTime dateTime)
        {
            return timecards.Where(p => p.Date == dateTime).FirstOrDefault();
        }

        public override double CalculatePay(PayCheck paycheck)
        {
            double totalPay = 0.0;
            foreach (TimeCard timeCard in timecards)
            {
                if (IsInPayPeriod(timeCard.Date, paycheck))
                    totalPay += CalculatePayForTimeCard(timeCard);
            }
            return totalPay;
        }

        private double CalculatePayForTimeCard(TimeCard card)
        {
            double overtimeHours = Math.Max(0.0, card.Hours - 8);
            double normalHours = card.Hours - overtimeHours;
            return hourlyRate * normalHours +
            hourlyRate * 1.5 * overtimeHours;
        }
    }
}
