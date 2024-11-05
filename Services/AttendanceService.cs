using Objects;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositories;
using Objects;
namespace Services
{
    public class AttendanceService : IAttendanceService
    {
        private readonly IAttendanceRepository iAttendanceRepository;
        public AttendanceService(IAttendanceRepository attendanceRepository)
        {
            iAttendanceRepository = attendanceRepository ?? throw new ArgumentNullException(nameof(attendanceRepository));
        }

        public void AddAttendance(Attendance attendance)
        {
            iAttendanceRepository.AddAttendance(attendance);
        }

        public bool AttendanceExists(int employeeId, DateOnly date)
        {
            return iAttendanceRepository.AttendanceExists(employeeId, date);
        }

        public List<Attendance> GetAttendancesByEmployeeId(int employeeId)
        {
           
            return iAttendanceRepository.GetAttendancesByEmployeeId(employeeId);
        }

    }
}
