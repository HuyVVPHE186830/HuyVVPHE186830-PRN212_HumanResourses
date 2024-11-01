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
        public static void DeleteDepartment(Department employee)
        {
            try
            {
                using var db = new FuhrmContext();
                var b1 = db.Departments.SingleOrDefault(b => b.DepartmentId == employee.DepartmentId);
                db.Departments.Remove(b1);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
