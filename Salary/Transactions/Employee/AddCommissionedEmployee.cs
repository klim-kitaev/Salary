using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Payroll.Models;

namespace Payroll.Transactions
{
    public class AddCommissionedEmployee : AddEmployeeTransaction
    {
        private readonly double salary;
        private readonly double commissionRate;

        public AddCommissionedEmployee(int empid, string name, string address, double salary, double commissionRate) : base(empid, name, address)
        {
            this.salary = salary;
            this.commissionRate = commissionRate;
        }

        protected override PaymentClassification MakeClassification()
        {
            return new CommissionedClassification(salary, commissionRate);
        }

        protected override PaymentSchedule MakeSchedule()
        {
            return new BiweeklySchedule();
        }
    }
}
