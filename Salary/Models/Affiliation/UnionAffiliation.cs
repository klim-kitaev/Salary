﻿using System;
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
            double service_charges = GetServiceCharge(paycheck.PayPeriodEndDate)==null ? 0 : GetServiceCharge(paycheck.PayPeriodEndDate).Amount;
            double totalDues = 0;
            int fridays = NumberOfFridaysInPayPeriod(paycheck.PayPeriodStartDate, paycheck.PayPeriodEndDate);
            totalDues = dues * fridays;
            return service_charges + totalDues;
        }

        private int NumberOfFridaysInPayPeriod(DateTime payPeriodStart, DateTime payPeriodEnd)
        {
            int fridays = 0;
            for (DateTime day = payPeriodStart;
            day <= payPeriodEnd;day = day.AddDays(1))
            {
                if (day.DayOfWeek == DayOfWeek.Friday)
                    fridays++;
            }
            return fridays;
        }
    }
}
