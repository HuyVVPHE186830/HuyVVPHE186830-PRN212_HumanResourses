using Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;

namespace Repositories
{
   public class SalaryRepository : ISalaryRepository
    {
        public void AddSalary(Salary salary) => SalaryDAO.AddSalary(salary);

        public Salary GetEmployeeByEmployeeId(int employeeId) => SalaryDAO.GetSalaryByEmployeeId(employeeId);


        public void UpdateEmployee(Salary salary) => SalaryDAO.UpdateSalary(salary);


    }
}
