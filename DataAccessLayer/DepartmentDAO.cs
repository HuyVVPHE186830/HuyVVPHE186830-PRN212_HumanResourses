using Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class DepartmentDAO
    {
        private static List<Department> listDepartments;
        public static List<Department> GetDepartments()
        {
            listDepartments = new List<Department>();
            try
            {
                using var db = new FuhrmContext();
                listDepartments = db.Departments.ToList();
            }
            catch (Exception e) { }
            return listDepartments;
        }
        public static void AddDepartment(Department employee)
        {
            try
            {
                using var db = new FuhrmContext();
                db.Departments.Add(employee);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public static void UpdateDepartment(Department employee)
        {
            try
            {
                using var db = new FuhrmContext();
                db.Entry<Department>(employee).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public static void DeleteDepartment(Department department)
        {
            try
            {
                using var db = new FuhrmContext();

                var employees = db.Employees.Where(e => e.DepartmentId == department.DepartmentId).ToList();
                foreach (var employee in employees)
                {
                    employee.DepartmentId = 4;
                }
                var notis = db.Notifications.Where(e => e.DepartmentId == department.DepartmentId).ToList();
                foreach (var noti in notis)
                {
                    db.Notifications.Remove(noti);
                }
                db.Departments.Remove(department);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
