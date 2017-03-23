using System;

namespace Payroll.Models
{
    public class Employee
    {
        private string address;
        private readonly int empid;
        private string name;
        private PaymentClassification classification;
        private Affiliation affiliation;
        private PaymentMethod method;
        private PaymentSchedule schedule;

        public Employee(int empid, string name, string address)
        {
            this.empid = empid;
            this.name = name;
            this.address = address;
            this.affiliation = new NoAffiliation();
        }

        public string Address
        {
            get
            {
                return address;
            }
            set
            {
                address = value;
            }
        }

        internal DateTime GetPayPeriodStartDate(DateTime date)
        {
            return schedule.GetPayPeriodStartDate(date);
        }

        internal bool IsPayDate(DateTime payDate)
        {
            return schedule.IsPayDate(payDate);
        }

        public int Empid
        {
            get
            {
                return empid;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }        

        public PaymentClassification Classification
        {
            get
            {
                return classification;
            }

            set
            {
                classification = value;
            }
        }

        public Affiliation Affiliation
        {
            get
            {
                return affiliation;
            }

            set
            {
                affiliation = value;
            }
        }

        public PaymentMethod Method
        {
            get
            {
                return method;
            }

            set
            {
                method = value;
            }
        }

        public PaymentSchedule Schedule
        {
            get
            {
                return schedule;
            }

            set
            {
                schedule = value;
            }
        }

        public void PayDay(PayCheck paycheck)
        {
            double grossPay = classification.CalculatePay(paycheck);
            double deductions = affiliation.CalculateDeductions(paycheck);
            double netPay = grossPay - deductions;
            paycheck.GrossPay = grossPay;
            paycheck.Deductions = deductions;
            paycheck.NetPay = netPay;
            method.Pay(paycheck);
        }
    }
}