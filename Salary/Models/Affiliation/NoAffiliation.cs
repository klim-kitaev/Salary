using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Models
{
    public class NoAffiliation : Affiliation
    {
        public double CalculateDeductions(PayCheck paycheck)
        {
            return 0;
        }
    }
}
