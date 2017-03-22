using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Payroll.Models;

namespace Payroll.Transactions
{
    public class ChangeNameTransaction : ChangeEmployeeTransaction
    {
        private readonly string newName;

        public ChangeNameTransaction(int empId,string newName) : base(empId)
        {
            this.newName = newName;
        }

        protected override void Change(Employee e)
        {
           e.Name=newName;
        }
    }
}
