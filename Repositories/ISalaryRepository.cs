using Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public  interface ISalaryRepository 
    {
        void AddSalary(Salary salary);
        Salary GetEmployeeByEmployeeId(int employeeId);
        void UpdateEmployee(Salary salary);
    }
}
