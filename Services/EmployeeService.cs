using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositories;
using Objects;

namespace Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository iEmployeeRepository;
        public EmployeeService()
        {
            iEmployeeRepository = new EmployeeRepository();
        }
        public void AddEmployee(Employee employee)
        {
            iEmployeeRepository.AddEmployee(employee);
        }
        public void UpdateEmployee(Employee employee)
        {
            iEmployeeRepository.UpdateEmployee(employee);
        }
        public void DeleteEmployee(Employee employee)
        {
            iEmployeeRepository.DeleteEmployee(employee);
        }
        public List<Employee> GetEmployees()
        {
            return iEmployeeRepository.GetEmployees();
        }
        public List<Employee> SearchEmployee(string keyword)
        {
            return iEmployeeRepository.SearchEmployee(keyword);
        }
        public Employee GetEmployeeByEmployeeId(int employeeId)
        {
            return iEmployeeRepository.GetEmployeeByEmployeeId(employeeId);
        }
        public Employee GetEmployeeByAccountId(int accountId)
        {
            return iEmployeeRepository.GetEmployeeByEmployeeId(accountId);
        }
        public void AddEmployees(List<Employee> employees)
        {
            iEmployeeRepository.AddEmployees(employees);
        }
    }
}
