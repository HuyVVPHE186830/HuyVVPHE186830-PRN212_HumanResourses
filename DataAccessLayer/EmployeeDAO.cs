using Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class EmployeeDAO
    {
        private static List<Employee> listEmployees;
        public static List<Employee> GetEmployees()
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
        public static void DeleteEmployee(Employee employee)
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
        public static List<Employee> SearchEmployee(String keyword)
        {
            try
            {
                using var db = new FuhrmContext();
                return db.Employees
                          .Where(c => c.Department.DepartmentName.Contains(keyword)
                                   || c.FullName.Contains(keyword)
                                   || c.Position.PositionName.Contains(keyword))
                          .ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static Employee GetEmployeeByEmployeeId(int employeeId)
        {
            using var db = new FuhrmContext();
            return db.Employees.FirstOrDefault(b => b.EmployeeId.Equals(employeeId));
        }

    }
}
