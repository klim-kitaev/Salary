using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Payroll;
using Payroll.Models;
using Payroll.Transactions;

namespace Payroll.Test
{
    [TestClass]
    public class PayrollTest
    {
        [TestMethod]
        public void TestAddSalariedEmployee()
        {
            int empId = 1;
            AddSalariedEmployee t = new AddSalariedEmployee(empId, "Bob", "Home", 1000.00);
            t.Execute();
            Employee e = PayrollDatabase.GetEmployee(empId);
            Assert.AreEqual("Bob", e.Name);

            PaymentClassification pc = e.Classification;
            Assert.IsTrue(pc is SalariedClassification);
            SalariedClassification sc = pc as SalariedClassification;
            Assert.AreEqual(1000.00, sc.Salary);

            PaymentSchedule ps = e.Schedule;
            Assert.IsTrue(ps is MonthlySchedule);
            PaymentMethod pm = e.Method;
            Assert.IsTrue(pm is HoldMethod);
        }

        [TestMethod]
        public void TestAddHourlyEmployee()
        {
            int empId = 1;
            AddHourlyEmployee t = new AddHourlyEmployee(empId, "Bob", "Home", 5.00);
            t.Execute();
            Employee e = PayrollDatabase.GetEmployee(empId);
            Assert.AreEqual("Bob", e.Name);

            PaymentClassification pc = e.Classification;
            Assert.IsTrue(pc is HourlyClassification);
            HourlyClassification sc = pc as HourlyClassification;
            Assert.AreEqual(5.00, sc.HourlyRate);

            PaymentSchedule ps = e.Schedule;
            Assert.IsTrue(ps is WeeklySchedule);
            PaymentMethod pm = e.Method;
            Assert.IsTrue(pm is HoldMethod);
        }

        [TestMethod]
        public void TestAddCommissionEmployee()
        {
            int empId = 1;
            AddCommissionedEmployee t = new AddCommissionedEmployee(empId, "Bob", "Home", 1000.00, 500.00);
            t.Execute();
            Employee e = PayrollDatabase.GetEmployee(empId);
            Assert.AreEqual("Bob", e.Name);

            PaymentClassification pc = e.Classification;
            Assert.IsTrue(pc is CommissionedClassification);
            CommissionedClassification sc = pc as CommissionedClassification;
            Assert.AreEqual(1000, sc.Salary);
            Assert.AreEqual(500, sc.CommissionRate);

            PaymentSchedule ps = e.Schedule;
            Assert.IsTrue(ps is BiweeklySchedule);
            PaymentMethod pm = e.Method;
            Assert.IsTrue(pm is HoldMethod);
        }

        [TestMethod]
        public void DeleteEmployee()
        {
            int empId = 4;
            AddCommissionedEmployee t = new AddCommissionedEmployee(empId, "Bill", "Home", 2500, 3.2);
            t.Execute();

            Employee e = PayrollDatabase.GetEmployee(empId);
            Assert.IsNotNull(e);

            DeleteEmployeeTransaction dt = new DeleteEmployeeTransaction(empId);
            dt.Execute();
            e = PayrollDatabase.GetEmployee(empId);
            Assert.IsNull(e);
        }

        [TestMethod]
        public void TestTimeCardTransaction()
        {
            int empId = 1;
            AddHourlyEmployee t = new AddHourlyEmployee(empId, "Bob", "Home", 15.25);
            t.Execute();

            TimeCardTransaction tct = new TimeCardTransaction(new DateTime(2005, 7, 31), 8.0, empId);
            tct.Execute();

            Employee e = PayrollDatabase.GetEmployee(empId);
            Assert.IsNotNull(e);

            PaymentClassification pc = e.Classification;
            Assert.IsTrue(pc is HourlyClassification);
            HourlyClassification hc = pc as HourlyClassification;

            TimeCard tc = hc.GetTimeCard(new DateTime(2005, 7, 31));
            Assert.IsNotNull(tc);
            Assert.AreEqual(8.0, tc.Hours);
        }

        [TestMethod]
        public void TestSalesReceiptTransaction()
        {
            int empId = 4;
            AddCommissionedEmployee t = new AddCommissionedEmployee(empId, "Bill", "Home", 2500, 3.2);
            t.Execute();

            SalesReceiptTransaction srt = new SalesReceiptTransaction(new DateTime(2005, 7, 31), 20, empId);
            srt.Execute();

            Employee e = PayrollDatabase.GetEmployee(empId);
            Assert.IsNotNull(e);

            PaymentClassification pc = e.Classification;
            Assert.IsTrue(pc is CommissionedClassification);
            CommissionedClassification hc = pc as CommissionedClassification;

            SalesReceipt sr = hc.GetSalesReceipt(new DateTime(2005, 7, 31));
            Assert.IsNotNull(sr);
            Assert.AreEqual(20, sr.Amount);
        }

        [TestMethod]
        public void AddServiceCharge()
        {
            int empId = 2;
            AddHourlyEmployee t = new AddHourlyEmployee(empId, "Bill", "Home", 15.25);
            t.Execute();
            Employee e = PayrollDatabase.GetEmployee(empId);
            Assert.IsNotNull(e);

            int memberId = 86; // Maxwell Smart
            UnionAffiliation af = new UnionAffiliation(memberId,80);
            e.Affiliation = af;
           
            PayrollDatabase.AddUnionMember(memberId, e);

            ServiceChargeTransaction sct = new ServiceChargeTransaction(memberId, new DateTime(2005, 8, 8), 12.95);
            sct.Execute();
            ServiceCharge sc = af.GetServiceCharge(new DateTime(2005, 8, 8));
            Assert.IsNotNull(sc);
            Assert.AreEqual(12.95, sc.Amount);
        }

        [TestMethod]
        public void TestChangeNameTransaction()
        {
            int empId = 2;
            AddHourlyEmployee t = new AddHourlyEmployee(empId, "Bill", "Home", 15.25);
            t.Execute();
            ChangeNameTransaction cnt = new ChangeNameTransaction(empId, "Bob");
            cnt.Execute();
            Employee e = PayrollDatabase.GetEmployee(empId);
            Assert.IsNotNull(e);
            Assert.AreEqual("Bob", e.Name);
        }

        [TestMethod]
        public void TestChangeAddressTransaction()
        {
            int empId = 2;
            AddHourlyEmployee t = new AddHourlyEmployee(empId, "Bill", "Home", 15.25);
            t.Execute();
            ChangeAddressTransaction cnt = new ChangeAddressTransaction(empId, "Work");
            cnt.Execute();
            Employee e = PayrollDatabase.GetEmployee(empId);
            Assert.IsNotNull(e);
            Assert.AreEqual("Work", e.Address);
        }

        [TestMethod]
        public void TestChangeHourlyTransaction()
        {
            int empId = 4;
            AddCommissionedEmployee t = new AddCommissionedEmployee(empId, "Bill", "Home", 2500, 3.2);
            t.Execute();

            ChangeHourlyTransaction cht = new ChangeHourlyTransaction(empId, 27.52);
            cht.Execute();
            Employee e = PayrollDatabase.GetEmployee(empId);
            Assert.IsNotNull(e);
            PaymentClassification pc = e.Classification;
            Assert.IsNotNull(pc);
            Assert.IsTrue(pc is HourlyClassification);
            HourlyClassification hc = pc as HourlyClassification;
            Assert.AreEqual(27.52, hc.HourlyRate);
            PaymentSchedule ps = e.Schedule;
            Assert.IsTrue(ps is WeeklySchedule);
        }

        [TestMethod]
        public void TestChangeSalariedTransaction()
        {
            int empId = 4;
            AddCommissionedEmployee t = new AddCommissionedEmployee(empId, "Bill", "Home", 2500, 3.2);
            t.Execute();

            ChangeSalariedTransaction cht = new ChangeSalariedTransaction(empId, 5000);
            cht.Execute();
            Employee e = PayrollDatabase.GetEmployee(empId);
            Assert.IsNotNull(e);
            PaymentClassification pc = e.Classification;
            Assert.IsNotNull(pc);
            Assert.IsTrue(pc is SalariedClassification);
            SalariedClassification hc = pc as SalariedClassification;
            Assert.AreEqual(5000, hc.Salary);
            PaymentSchedule ps = e.Schedule;
            Assert.IsTrue(ps is MonthlySchedule);
        }

        [TestMethod]
        public void TestChangeCommissionedTransaction()
        {
            int empId = 2;
            AddHourlyEmployee t = new AddHourlyEmployee(empId, "Bill", "Home", 15.25);
            t.Execute();

            ChangeCommissionedTransaction cht = new ChangeCommissionedTransaction(empId, 2500, 3.2);
            cht.Execute();
            Employee e = PayrollDatabase.GetEmployee(empId);
            Assert.IsNotNull(e);

            PaymentClassification pc = e.Classification;
            Assert.IsNotNull(pc);
            Assert.IsTrue(pc is CommissionedClassification);
            CommissionedClassification hc = pc as CommissionedClassification;
            Assert.AreEqual(2500, hc.Salary);
            Assert.AreEqual(3.2, hc.CommissionRate);
            PaymentSchedule ps = e.Schedule;
            Assert.IsTrue(ps is BiweeklySchedule);
        }

        [TestMethod]
        public void TestChangeDirectTransaction()
        {
            int empId = 2;
            AddHourlyEmployee t = new AddHourlyEmployee(empId, "Bill", "Home", 15.25);
            t.Execute();

            ChangeDirectTransaction mt = new ChangeDirectTransaction(empId, "b_test", "a_test");
            mt.Execute();
            Employee e = PayrollDatabase.GetEmployee(empId);
            Assert.IsNotNull(e);

            PaymentMethod pm = e.Method;
            Assert.IsNotNull(pm);
            Assert.IsTrue(pm is DirectMethod);
            DirectMethod dm = pm as DirectMethod;
            Assert.AreEqual("b_test", dm.Bank);
            Assert.AreEqual("a_test", dm.Account);
        }

        [TestMethod]
        public void TestChangeMailTransaction()
        {
            int empId = 2;
            AddHourlyEmployee t = new AddHourlyEmployee(empId, "Bill", "Home", 15.25);
            t.Execute();

            ChangeMailTransaction mt = new ChangeMailTransaction(empId, "email");
            mt.Execute();
            Employee e = PayrollDatabase.GetEmployee(empId);
            Assert.IsNotNull(e);

            PaymentMethod pm = e.Method;
            Assert.IsNotNull(pm);
            Assert.IsTrue(pm is MailMethod);
            MailMethod dm = pm as MailMethod;
            Assert.AreEqual("email", dm.Address);
        }

        [TestMethod]
        public void TestChangeHoldransaction()
        {
            int empId = 2;
            AddHourlyEmployee t = new AddHourlyEmployee(empId, "Bill", "Home", 15.25);
            t.Execute();

            ChangeHoldransaction mt = new ChangeHoldransaction(empId);
            mt.Execute();
            Employee e = PayrollDatabase.GetEmployee(empId);
            Assert.IsNotNull(e);

            PaymentMethod pm = e.Method;
            Assert.IsNotNull(pm);
            Assert.IsTrue(pm is HoldMethod);
        }
        [TestMethod]
        public void ChangeUnionMember()
        {
            int empId = 8;
            AddHourlyEmployee t =
            new AddHourlyEmployee(empId, "Билл", "Домашний", 15.25);
            t.Execute();
            int memberId = 7743;
            ChangeMemberTransaction cmt =
            new ChangeMemberTransaction(empId, memberId, 99.42);
            cmt.Execute();
            Employee e = PayrollDatabase.GetEmployee(empId);
            Assert.IsNotNull(e);
            Affiliation affiliation = e.Affiliation;
            Assert.IsNotNull(affiliation);
            Assert.IsTrue(affiliation is UnionAffiliation);
            UnionAffiliation uf = affiliation as UnionAffiliation;
            Assert.AreEqual(99.42, uf.Dues);
            Employee member = PayrollDatabase.GetUnionMember(memberId);
            Assert.IsNotNull(member);
            Assert.AreEqual(e, member);
        }

        [TestMethod]
        public void PaySingleSalariedEmployee()
        {
            int empId = 1;
            AddSalariedEmployee t = new AddSalariedEmployee(empId, "Bill", "Home", 1000);
            t.Execute();

            DateTime payDate = new DateTime(2001, 11, 30);
            PaydayTransaction pt = new PaydayTransaction(payDate);
            pt.Execute();
            PayCheck pc = pt.GetPaycheck(empId);
            Assert.IsNotNull(pc);
            Assert.AreEqual(payDate, pc.PayPeriodEndDate);
            Assert.AreEqual(1000.00, pc.GrossPay, .001);
            Assert.AreEqual("Hold", pc.GetField("Disposition"));
            Assert.AreEqual(0.0, pc.Deductions, .001);
            Assert.AreEqual(1000.00, pc.NetPay, .001);
        }

        [TestMethod]
        public void PaySingleSalariedEmployeeOnWrongDate()
        {
            int empId = 1;
            AddSalariedEmployee t = new AddSalariedEmployee(empId, "Bill", "Home", 1000);
            t.Execute();

            DateTime payDate = new DateTime(2001, 11, 29);
            PaydayTransaction pt = new PaydayTransaction(payDate);
            pt.Execute();
            PayCheck pc = pt.GetPaycheck(empId);
            Assert.IsNull(pc);
        }
        [TestMethod]
        public void PayingSingleHourlyEmployeeNoTimeCards()
        {
            int empId = 2;
            AddHourlyEmployee t =
            new AddHourlyEmployee(empId, "Билл", "Домашний", 15.25);
            t.Execute();

            DateTime payDate = new DateTime(2001, 11, 9);
            PaydayTransaction pt = new PaydayTransaction(payDate);
            pt.Execute();
            ValidateHourlyPaycheck(pt, empId, payDate, 0.0);
        }

        [TestMethod]
        public void PaySingleHourlyEmployeeOneTimeCard()
        {
            int empId = 2;
            AddHourlyEmployee t =
            new AddHourlyEmployee(empId, "Билл", "Домашний", 15.25);
            t.Execute();

            DateTime payDate = new DateTime(2001, 11, 9); // пятница
            TimeCardTransaction tc =
            new TimeCardTransaction(payDate, 2.0, empId);
            tc.Execute();
            PaydayTransaction pt = new PaydayTransaction(payDate);
            pt.Execute();
            ValidateHourlyPaycheck(pt, empId, payDate, 30.5);
        }

        [TestMethod]
        public void PaySingleHourlyEmployeeOvertimeOneTimeCard()
        {
            int empId = 2;
            AddHourlyEmployee t =
            new AddHourlyEmployee(empId, "Билл", "Домашний", 15.25);
            t.Execute();

            DateTime payDate = new DateTime(2001, 11, 9); // пятница
            TimeCardTransaction tc =
            new TimeCardTransaction(payDate, 9.0, empId);
            tc.Execute();
            PaydayTransaction pt = new PaydayTransaction(payDate);
            pt.Execute();
            ValidateHourlyPaycheck(pt, empId, payDate,
            (8 + 1.5) * 15.25);
        }

        [TestMethod]
        public void PaySingleHourlyEmployeeOnWrongDate()
        {
            int empId = 2;
            AddHourlyEmployee t =
            new AddHourlyEmployee(empId, "Билл", "Домашний", 15.25);
            t.Execute();

            DateTime payDate = new DateTime(2001, 11, 8); // четверг
            TimeCardTransaction tc =
            new TimeCardTransaction(payDate, 9.0, empId);
            tc.Execute();
            PaydayTransaction pt = new PaydayTransaction(payDate);
            pt.Execute();
            PayCheck pc = pt.GetPaycheck(empId);
            Assert.IsNull(pc);
        }

        [TestMethod]
        public void PaySingleHourlyEmployeeTwoTimeCards()
        {
            int empId = 2;
            AddHourlyEmployee t =
            new AddHourlyEmployee(empId, "Билл", "Домашний", 15.25);
            t.Execute();

            DateTime payDate = new DateTime(2001, 11, 9); // пятница
            TimeCardTransaction tc =
            new TimeCardTransaction(payDate, 2.0, empId);
            tc.Execute();
            TimeCardTransaction tc2 =
            new TimeCardTransaction(payDate.AddDays(-1), 5.0, empId);
            tc2.Execute();
            PaydayTransaction pt = new PaydayTransaction(payDate);
            pt.Execute();
            ValidateHourlyPaycheck(pt, empId, payDate, 7 * 15.25);
        }

        [TestMethod]
        public void TestPaySingleHourlyEmployeeWithTimeCardsSpanningTwoPayPeriods()
        {
            int empId = 2;
            AddHourlyEmployee t =
            new AddHourlyEmployee(empId, "Билл", "Домашний", 15.25);
            t.Execute();

            DateTime payDate = new DateTime(2001, 11, 9); // пятница
            DateTime dateInPreviousPayPeriod = new DateTime(2001, 11, 2);
            TimeCardTransaction tc =
            new TimeCardTransaction(payDate, 2.0, empId);
            tc.Execute();
            TimeCardTransaction tc2 = new TimeCardTransaction(
            dateInPreviousPayPeriod, 5.0, empId);
            tc2.Execute();
            PaydayTransaction pt = new PaydayTransaction(payDate);
            pt.Execute();
            ValidateHourlyPaycheck(pt, empId, payDate, 2 * 15.25);
        }


        private void ValidateHourlyPaycheck(PaydayTransaction pt,int empid, DateTime payDate, double pay)
        {
            PayCheck pc = pt.GetPaycheck(empid);
            Assert.IsNotNull(pc);
            Assert.AreEqual(payDate, pc.PayPeriodEndDate);
            Assert.AreEqual(pay, pc.GrossPay, .001);
            Assert.AreEqual("Hold", pc.GetField("Disposition"));
            Assert.AreEqual(0.0, pc.Deductions, .001);
            Assert.AreEqual(pay, pc.NetPay, .001);
        }

        private void ValidateCommisionPaycheck(PaydayTransaction pt, int empid, DateTime payDate, double pay)
        {
            PayCheck pc = pt.GetPaycheck(empid);
            Assert.IsNotNull(pc);
            Assert.AreEqual(payDate, pc.PayPeriodEndDate);
            Assert.AreEqual(pay, pc.GrossPay, .001);
            Assert.AreEqual("Hold", pc.GetField("Disposition"));
            Assert.AreEqual(0.0, pc.Deductions, .001);
            Assert.AreEqual(pay, pc.NetPay, .001);
        }

        [TestMethod]
        public void PayingCommisionEmployeeNoSalesRecipt()
        {
            int empId = 4;
            AddCommissionedEmployee t = new AddCommissionedEmployee(empId, "Bill", "Home", 2500, 3.2);
            t.Execute();

            DateTime payDate = new DateTime(2001, 11, 2);
            PaydayTransaction pt = new PaydayTransaction(payDate);
            pt.Execute();
            ValidateCommisionPaycheck(pt, empId, payDate, 2500/2);
        }

        [TestMethod]
        public void PayingCommisionEmployeeOneSalesRecipt()
        {
            int empId = 4;
            AddCommissionedEmployee t = new AddCommissionedEmployee(empId, "Bill", "Home", 2500, 3.2);
            t.Execute();

            DateTime payDate = new DateTime(2001, 11, 2);
            SalesReceiptTransaction st = new SalesReceiptTransaction(payDate, 2, empId);
            st.Execute();

            PaydayTransaction pt = new PaydayTransaction(payDate);
            pt.Execute();
            ValidateCommisionPaycheck(pt, empId, payDate, (2500 / 2)+(3.2*2));
        }

        [TestMethod]
        public void PayingCommisionEmployeeOnWrongDate()
        {
            int empId = 4;
            AddCommissionedEmployee t = new AddCommissionedEmployee(empId, "Bill", "Home", 2500, 3.2);
            t.Execute();

            DateTime payDate = new DateTime(2001, 11, 8);
            SalesReceiptTransaction st = new SalesReceiptTransaction(payDate, 2, empId);
            st.Execute();

            PaydayTransaction pt = new PaydayTransaction(payDate);
            pt.Execute();

            PayCheck pc = pt.GetPaycheck(empId);
            Assert.IsNull(pc);
        }
        [TestMethod]
        public void PayingCommisionEmployeeTwoSalesRecipt()
        {
            int empId = 4;
            AddCommissionedEmployee t = new AddCommissionedEmployee(empId, "Bill", "Home", 2500, 3.2);
            t.Execute();

            DateTime payDate = new DateTime(2001, 11, 2);
            SalesReceiptTransaction st = new SalesReceiptTransaction(payDate, 2, empId);
            st.Execute();
            SalesReceiptTransaction st2 = new SalesReceiptTransaction(payDate.AddDays(-1), 3, empId);
            st2.Execute();

            PaydayTransaction pt = new PaydayTransaction(payDate);
            pt.Execute();
            ValidateCommisionPaycheck(pt, empId, payDate, (2500 / 2) + (3.2 * 2)+(3.2*3));
        }

        [TestMethod]
        public void PayingCommisionEmployeeOnFiveFridaysMonth()
        {
            int empId = 4;
            AddCommissionedEmployee t = new AddCommissionedEmployee(empId, "Bill", "Home", 2500, 3.2);
            t.Execute();

            DateTime payDate = new DateTime(2017, 3, 31);
            SalesReceiptTransaction st = new SalesReceiptTransaction(payDate, 2, empId);
            st.Execute();

            PaydayTransaction pt = new PaydayTransaction(payDate);
            pt.Execute();
            ValidateCommisionPaycheck(pt, empId, payDate, 3.2 * 2);
        }
        [TestMethod]
        public void PayingCommisionEmployeeTwoSalesReciptInTwoPeriods()
        {
            int empId = 4;
            AddCommissionedEmployee t = new AddCommissionedEmployee(empId, "Bill", "Home", 2500, 3.2);
            t.Execute();

            DateTime payDate = new DateTime(2001, 11, 2);
            SalesReceiptTransaction st = new SalesReceiptTransaction(payDate, 2, empId);
            st.Execute();
            SalesReceiptTransaction st2 = new SalesReceiptTransaction(payDate.AddDays(1), 3, empId);
            st2.Execute();

            PaydayTransaction pt = new PaydayTransaction(payDate);
            pt.Execute();
            ValidateCommisionPaycheck(pt, empId, payDate, (2500 / 2) + (3.2 * 2));
        }
    }
}
