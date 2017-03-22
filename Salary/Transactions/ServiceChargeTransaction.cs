using Payroll.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Transactions
{
    public class ServiceChargeTransaction:Transaction
    {
        private readonly int memberId;
        private readonly DateTime time;
        private readonly double charge;
        public ServiceChargeTransaction(int id, DateTime time, double charge)
        {
            this.memberId = id;
            this.time = time;
            this.charge = charge;
        }

        public void Execute()
        {
            var e = PayrollDatabase.GetUnionMember(memberId);
            if (e != null)
            {
                UnionAffiliation ua = null;
                if (e.Affiliation is UnionAffiliation)
                    ua = e.Affiliation as UnionAffiliation;
                if (ua != null)
                    ua.AddServiceCharge(new ServiceCharge(time, charge));
                else
                    throw new InvalidOperationException("Попытка добавить плату за услуги для члена профсоюза с незарегистрированным членством");
            }
            else
            {
                throw new InvalidOperationException("Член профсоюза не найден.");
            }
        }
    }
}
