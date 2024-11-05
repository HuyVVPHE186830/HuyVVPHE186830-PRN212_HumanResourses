using Objects;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository iDepartmentRepository;
        public DepartmentService()
        {
            iDepartmentRepository = new DepartmentRepository();
        }
        public List<Department> GetDepartments()
        {
            return iDepartmentRepository.GetDepartments();
        }
        public void AddDepartment(Department employee)
        {
            iDepartmentRepository.AddDepartment(employee);
        }
        public void UpdateDepartment(Department employee)
        {
            iDepartmentRepository.UpdateDepartment(employee);
        }
        public void DeleteDepartment(Department employee)
        {
            iDepartmentRepository.DeleteDepartment(employee);
        }
        public List<Employee> GetEmployeesByDepartmentId(int departmentId)
        {
            return iDepartmentRepository.GetEmployeesByDepartmentId(departmentId);
        }
    }
}
