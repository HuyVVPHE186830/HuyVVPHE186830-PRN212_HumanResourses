using Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface ISalaryService
    {
        void AddSalary(Salary salary);
        void UpdateSalary(Salary salary);

        Salary GetSalaryByEmployeeId(int employeeId);
    }
}
