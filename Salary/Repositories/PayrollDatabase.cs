using Payroll.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll
{
    public class PayrollDatabase
    {
        private static Dictionary<int, Employee> employees = new Dictionary<int, Employee>();
        private static Dictionary<int, Employee> members = new Dictionary<int, Employee>();

        public static void AddEmployee(int id, Employee employee)
        {
            employees[id] = employee;
        }

        internal static void DeleteEmployee(int id)
        {
            employees.Remove(id);
        }

        public static Employee GetEmployee(int id)
        {            
            return employees.ContainsKey(id) ? employees[id]:null;
        }

        public static List<int> GetAllEmployeeIds()
        {
            return employees.Keys.ToList();
        }

        public static void AddUnionMember(int id, Employee employee)
        {
            members[id] = employee;
        }

        internal static void DeleteUnionMember(int id)
        {
            members.Remove(id);
        }


        public static Employee GetUnionMember(int id)
        {
            return members[id];
        }
    }
}
