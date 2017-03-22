using Payroll.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Transactions
{
    public class PaydayTransaction : Transaction
    {
        private DateTime payDate;
        private Dictionary<int, PayCheck> paychecks = new Dictionary<int, PayCheck>();

        public PaydayTransaction(DateTime payDate)
        {
            this.payDate = payDate;
        }

        public void Execute()
        {
            var empIds = PayrollDatabase.GetAllEmployeeIds();
            foreach (var empId in empIds)
            {
                var employee = PayrollDatabase.GetEmployee(empId);
                if (employee.IsPayDate(payDate))
                {
                    DateTime startDate =employee.GetPayPeriodStartDate(payDate);
                    PayCheck pc = new PayCheck(startDate,payDate);
                    paychecks[empId] = pc;
                    employee.PayDay(pc);
                }
            }
        }

        public PayCheck GetPaycheck(int empId)
        {
            return paychecks.ContainsKey(empId) ? paychecks[empId] : null;
        }
    }
}
