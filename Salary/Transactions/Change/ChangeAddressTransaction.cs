using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Payroll.Models;

namespace Payroll.Transactions
{
    public class ChangeAddressTransaction : ChangeEmployeeTransaction
    {
        private readonly string newAddress;

        public ChangeAddressTransaction(int empId, string newAddress) : base(empId)
        {
            this.newAddress = newAddress;
        }

        protected override void Change(Employee e)
        {
            e.Address = newAddress;
        }
    }
}
