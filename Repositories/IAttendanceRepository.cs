using Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IAttendanceRepository
    {
        void AddAttendance(Attendance attendance);
        List<Attendance> GetAttendancesByEmployeeId(int employeeId);

        Boolean AttendanceExists(int employeeId, DateOnly date);
    }
}
