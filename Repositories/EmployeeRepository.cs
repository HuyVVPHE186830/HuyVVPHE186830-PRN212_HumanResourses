using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using Objects;

namespace Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        public void AddEmployee(Employee employee) => EmployeeDAO.AddEmployee(employee);
        public void UpdateEmployee(Employee employee) => EmployeeDAO.UpdateEmployee(employee);
        public void DeleteEmployee(Employee employee) => EmployeeDAO.DeleteEmployee(employee);
        public List<Employee> GetEmployees() => EmployeeDAO.GetEmployees();
        public List<Employee> SearchEmployee(string keyword) => EmployeeDAO.SearchEmployee(keyword);
        public Employee GetEmployeeByEmployeeId(int employeeId) => EmployeeDAO.GetEmployeeByEmployeeId(employeeId);
        public Employee GetEmployeeByAccountId(int accountId) => EmployeeDAO.GetEmployeeByAccountId(accountId);
<<<<<<< HEAD

        public List<string> GetAvailableEmployeeNames()=>EmployeeDAO.GetAvailableEmployeeNames();
       

        public List<string> GetEmployeeNames()=>EmployeeDAO.GetEmployeeNames();
        
=======
        public void AddEmployees(List<Employee> employees) => EmployeeDAO.AddEmployees(employees);
>>>>>>> main
    }
}
