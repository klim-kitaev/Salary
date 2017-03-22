using Payroll.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Transactions
{
    public class SalesReceiptTransaction : Transaction
    {
        private DateTime date;
        private int empId;
        private double amount;

        public SalesReceiptTransaction(DateTime date, double amount, int empId)
        {
            this.date = date;
            this.amount = amount;
            this.empId = empId;
        }

        public void Execute()
        {
            var e = PayrollDatabase.GetEmployee(empId);
            if (e != null)
            {
                var sc = e.Classification as CommissionedClassification;
                if (sc != null)
                    sc.AddSalesReceipt(new SalesReceipt(date,amount));
                else
                    throw new InvalidOperationException("Попытка добавить справки о продажах для работника не на комиссионной оплате");
            }
            else
            {
                throw new InvalidOperationException("Работник не найден.");
            }
        }
    }
}
