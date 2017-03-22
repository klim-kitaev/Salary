using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Models
{
    public class MailMethod:PaymentMethod
    {
        private readonly string address;

        public MailMethod(string address)
        {
            this.address = address;
        }

        public string Address
        {
            get
            {
                return address;
            }
        }
    }
}
