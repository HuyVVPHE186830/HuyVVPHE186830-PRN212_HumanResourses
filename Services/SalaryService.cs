using Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositories;

namespace Services
{
   
    public class SalaryService : ISalaryService
    {
        private readonly ISalaryRepository _employeeService;
        public SalaryService()
        {
            _employeeService = new SalaryRepository();
        }
     

        public void AddSalary(Salary salary)
        {
            _employeeService.AddSalary(salary);
        }

        public Salary GetSalaryByEmployeeId(int employeeId)
        {
            return _employeeService.GetEmployeeByEmployeeId(employeeId);
        }

        public void UpdateSalary(Salary salary)
        {
            _employeeService.UpdateEmployee(salary);
        }
    }
}
