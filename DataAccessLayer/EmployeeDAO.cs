using Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    internal class EmployeeDAO
    {
        private static List<Employee> listEmployees;
        public static List<Employee> GetBookings()
        {
            listEmployees = new List<Employee>();
            try
            {
                using var db = new FuhrmContext();
                listEmployees = db.Employees.ToList();
            }
            catch (Exception e) { }
            return listEmployees;
        }
        public static void AddEmployee(Employee employee)
        {
            try
            {
                using var db = new FuhrmContext();
                db.Employees.Add(employee);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public static void UpdateEmployee(Employee employee)
        {
            try
            {
                using var db = new FuhrmContext();
                db.Entry<Employee>(employee).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public static void DeteleEmployee(Employee employee)
        {
            try
            {
                using var db = new FuhrmContext();
                var b1 = db.Employees.SingleOrDefault(b => b.EmployeeId == employee.EmployeeId);
                db.Employees.Remove(b1);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static Employee GetEmployeeByBookingId(int employeeId)
        {
            using var db = new FuhrmContext();
            return db.Employees.FirstOrDefault(b => b.EmployeeId.Equals(employeeId));
        }

    }
}
