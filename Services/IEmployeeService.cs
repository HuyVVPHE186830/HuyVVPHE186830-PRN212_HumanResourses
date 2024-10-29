using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Objects;

namespace Services
{
    public interface IEmployeeService
    {
        void AddEmployee(Employee employee);
        void UpdateEmployee(Employee employee);
        void DeleteEmployee(Employee employee);
        List<Employee> GetEmployees();
        Employee GetEmployeeByEmployeeId(int EmployeeId);
    }
}
