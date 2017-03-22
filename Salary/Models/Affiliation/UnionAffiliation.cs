using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Models
{
    public class UnionAffiliation : Affiliation
    {
        private HashSet<ServiceCharge> serviceCharges;
        private readonly int memberId;
        private readonly double dues;

        public UnionAffiliation(int memberId, double dues)
        {
            this.memberId = memberId;
            this.dues = dues;
            serviceCharges = new HashSet<ServiceCharge>();
        }

        public object Dues
        {
            get
            {
                return dues;
            }
        }

        public int MemberId
        {
            get
            {
                return memberId;
            }
        }

        public void AddServiceCharge(ServiceCharge serviceCharge)
        {
            serviceCharges.Add(serviceCharge);
        }

        public ServiceCharge GetServiceCharge(DateTime dateTime)
        {
            return serviceCharges.Where(p => p.DateTime == dateTime).FirstOrDefault();
        }

        public double CalculateDeductions(PayCheck paycheck)
        {
            return GetServiceCharge(paycheck.PayDate) == null ? 0 : GetServiceCharge(paycheck.PayDate).Amount;
        }
    }
}
