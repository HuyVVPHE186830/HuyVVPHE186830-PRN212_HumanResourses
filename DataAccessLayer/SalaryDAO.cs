using Microsoft.EntityFrameworkCore;
using Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class SalaryDAO
    {
        public static void AddSalary(Salary salary)
        {
            try
            {
                using var db = new FuhrmContext();
                db.Salaries.Add(salary);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception("Error adding salary: " + e.Message);
            }
        }
        public static Salary GetSalaryByEmployeeId(int employeeId)
        {
            try
            {
                using var db = new FuhrmContext();
                return db.Salaries.FirstOrDefault(s => s.EmployeeId == employeeId);
            }
            catch (Exception e)
            {
                throw new Exception("Error retrieving salary: " + e.Message);
            }
        }

        public static void UpdateSalary(Salary salary)
        {
            using (var context = new FuhrmContext())
            {
                var existingSalary = context.Salaries.Find(salary.SalaryId);
                if (existingSalary != null)
                {
                    existingSalary.BaseSalary = salary.BaseSalary;
                    existingSalary.Allowance = salary.Allowance;
                    existingSalary.Bonus = salary.Bonus;
                    existingSalary.Penalty = salary.Penalty;
                    context.SaveChanges();
                }
            }
        }
        public static List<Salary> GetSalaries()
        {
            try
            {
                using var db = new FuhrmContext();
                return db.Salaries
                    .Include(s => s.Employee)
                    .ToList();
            }
            catch (Exception ex)
            {
                return new List<Salary>();
            }
        }
    }
}
