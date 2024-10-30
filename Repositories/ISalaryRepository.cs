using Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    internal  interface ISalaryRepository 
    {
        void AđdSalary(Salary salary);
        void UpdateSalary(Salary salary);
        Salary GetSalaryByEmployeeId(int employeeId);
    }
}
