using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Payroll.Models;

namespace Payroll.Transactions
{
    public class ChangeHoldransaction : ChangeMethodTransaction
    {
        public ChangeHoldransaction(int empId) : base(empId)
        {
        }

        protected override PaymentMethod Method
        {
            get
            {
                return new HoldMethod();
            }
        }
    }
}
