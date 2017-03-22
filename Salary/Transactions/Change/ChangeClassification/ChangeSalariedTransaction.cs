using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Payroll.Models;

namespace Payroll.Transactions
{
    public class ChangeSalariedTransaction : ChangeClassificationTransaction
    {
        private readonly double salary;

        public ChangeSalariedTransaction(int empId, double salary) : base(empId)
        {
            this.salary = salary;
        }

        protected override PaymentClassification Classification
        {
            get
            {
                return new SalariedClassification(salary);
            }
        }

        protected override PaymentSchedule Schedule
        {
            get
            {
                return new MonthlySchedule();
            }
        }
    }
}
