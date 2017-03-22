using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Models
{
    public class CommissionedClassification : PaymentClassification
    {
        private double salary;
        private double commissionRate;
        private HashSet<SalesReceipt> reciptes;

        public CommissionedClassification(double salary, double commissionRate)
        {
            this.salary = salary;
            this.commissionRate = commissionRate;
            reciptes = new HashSet<SalesReceipt>();
        }

        public double Salary
        {
            get
            {
                return salary;
            }
        }

        public double CommissionRate
        {
            get
            {
                return commissionRate;
            }
        }

        public SalesReceipt GetSalesReceipt(DateTime dateTime)
        {
            return reciptes.Where(p => p.Date == dateTime).FirstOrDefault();
        }

        public void AddSalesReceipt(SalesReceipt salesReceipt)
        {
            reciptes.Add(salesReceipt);
        }

        public override double CalculatePay(PayCheck paycheck)
        {
            double totalPay = IsNeedSalaryPay(paycheck.PayDate) ? salary / 2 : 0;
            foreach (var receipt in reciptes)
            {
                if (IsInPayPeriod(receipt, paycheck.PayDate))
                {
                    totalPay += (receipt.Amount * commissionRate);
                }
            }
            return totalPay;
        }

        private bool IsInPayPeriod(SalesReceipt receipt,DateTime payPeriod)
        {
            DateTime payPeriodEndDate = payPeriod;
            DateTime payPeriodStartDate = payPeriod.AddDays(-13);
            return receipt.Date <= payPeriodEndDate &&
            receipt.Date >= payPeriodStartDate;
        }

        private bool IsNeedSalaryPay(DateTime payDate)
        {
            //The salary paid only 1-st and 3-rd week
            int days_count = DateTime.DaysInMonth(payDate.Year, payDate.Month);
            HashSet<DateTime> need_frides = new HashSet<DateTime>();
            int week_count = 1;

            for (int i = 1; i <= days_count; i++)
            {
                var day = new DateTime(payDate.Year, payDate.Month, i);
                if (day.DayOfWeek == DayOfWeek.Friday)
                {
                    if (week_count==1 || week_count==3)
                    {
                        need_frides.Add(day);
                    }
                    week_count++;
                }
            }
            return need_frides.Contains(payDate);
        }

    }
}
