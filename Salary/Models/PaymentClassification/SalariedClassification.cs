using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Models
{
    public class SalariedClassification : PaymentClassification
    {
        private double salary;

        public SalariedClassification(double salary)
        {
            this.salary = salary;
        }

        public double Salary
        {
            get
            {
                return salary;
            }
        }

        public override double CalculatePay(PayCheck paycheck)
        {
            return salary;
        }
    }
}
