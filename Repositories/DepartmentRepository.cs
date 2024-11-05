using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Objects;
using DataAccessLayer;
namespace Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        public List<Department> GetDepartments() { return DepartmentDAO.GetDepartments(); }
        public void AddDepartment(Department employee) => DepartmentDAO.AddDepartment(employee);
        public void UpdateDepartment(Department employee) => DepartmentDAO.UpdateDepartment(employee);
        public void DeleteDepartment(Department employee) => DepartmentDAO.DeleteDepartment(employee);
        public List<Employee> GetEmployeesByDepartmentId(int departmentId)
        {
            using var db = new FuhrmContext();
            return db.Employees.Where(e => e.DepartmentId == departmentId).ToList();
        }
    }
}
