using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Payroll.Models;

namespace Payroll.Transactions
{
    public class ChangeHourlyTransaction : ChangeClassificationTransaction
    {
        private readonly double hourlyRate;

        public ChangeHourlyTransaction(int empId, double hourlyRate) : base(empId)
        {
            this.hourlyRate = hourlyRate;
        }

        protected override PaymentClassification Classification
        {
            get
            {
                return new HourlyClassification(hourlyRate);
            }
        }

        protected override PaymentSchedule Schedule
        {
            get
            {
                return new WeeklySchedule();
            }
        }
    }
}
