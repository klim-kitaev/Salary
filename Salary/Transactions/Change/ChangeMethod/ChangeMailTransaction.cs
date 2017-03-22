using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Payroll.Models;

namespace Payroll.Transactions
{
    public class ChangeMailTransaction : ChangeMethodTransaction
    {
        private readonly string address;

        public ChangeMailTransaction(int empId, string address) : base(empId)
        {
            this.address = address;
        }

        protected override PaymentMethod Method
        {
            get
            {
                return new MailMethod(address);
            }
        }
    }
}
