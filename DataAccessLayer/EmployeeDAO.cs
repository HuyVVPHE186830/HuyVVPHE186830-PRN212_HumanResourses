using Microsoft.EntityFrameworkCore;
using Objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
                listEmployees = db.Employees
            .Include(e => e.Department)
            .Include(e => e.Position)
            .ToList();
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
        public static List<Employee> GetEmployeesByDepartmentId(int departmentId)
        {
            using var db = new FuhrmContext();
            return db.Employees
                     .Where(employee => employee.DepartmentId == departmentId)
                     .Include(employee => employee.Position)
                     .Include(employee => employee.Department)
                     .ToList();
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
        public static Employee GetEmployeeByAccountId(int accountId)
        {
            using var db = new FuhrmContext();
            return db.Employees.FirstOrDefault(b => b.AccountId.Equals(accountId));
        }
        public static List<string> GetEmployeeNames()
        {
            try
            {
                using var db = new FuhrmContext();
                return db.Employees.Select(e => e.FullName).ToList();
            }
            catch (Exception e)
            {
                throw new Exception("Error retrieving employee names: " + e.Message);
            }
        }
        public static List<string> GetAvailableEmployeeNames()
        {
            try
            {
                using var db = new FuhrmContext();
                var allEmployees = db.Employees.ToList();
                var employeesWithSalaries = db.Salaries.Select(s => s.EmployeeId).ToList();
                var availableEmployees = allEmployees
                    .Where(e => !employeesWithSalaries.Contains(e.EmployeeId))
                    .Select(e => e.FullName)
                    .ToList();

                return availableEmployees;
            }
            catch (Exception e)
            {
                throw new Exception("Error retrieving available employee names: " + e.Message);
            }
        }
        public static void AddEmployees(List<Employee> employees)
        {
            using (var context = new FuhrmContext())
            {
                context.Employees.AddRange(employees);
                context.SaveChanges();
            }
        }
    }
}
