using Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IEmployeeRepository
    {
        void AddEmployee(Employee employee);
        void UpdateEmployee(Employee employee);
        void DeleteEmployee(Employee employee);
        List<Employee> GetEmployees();
        List<Employee> SearchEmployee(string keyword);

        Employee GetEmployeeByEmployeeId(int employeeId);
        Employee GetEmployeeByAccountId(int accountId);

        List<string> GetAvailableEmployeeNames();
        List<string> GetEmployeeNames();
        List<Employee> GetEmployeesByDepartmentId(int employeeId);
        void AddEmployees(List<Employee> employees);

    }
}
