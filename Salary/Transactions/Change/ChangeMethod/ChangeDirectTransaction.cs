using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Payroll.Models;

namespace Payroll.Transactions
{
    public class ChangeDirectTransaction:ChangeMethodTransaction
    {
        private readonly string bank;
        private readonly string account;

        protected override PaymentMethod Method
        {
            get
            {
                return new DirectMethod(bank, account);
            }
        }

        public ChangeDirectTransaction(int empId, string bank, string account) : base(empId)
        {
            this.bank = bank;
            this.account = account;
        }
    }
}
