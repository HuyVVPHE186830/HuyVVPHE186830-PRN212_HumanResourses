using Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface ILeaveRequestService
    {
        List<LeaveRequest> GetLeaveRequests();
        LeaveRequest GetLeaveRequestById(int id);
        void AddLeaveRequest(LeaveRequest leaveRequest);
        void UpdateLeaveRequest(LeaveRequest leaveRequest);
        void DeleteLeaveRequest(LeaveRequest leaveRequest);
    }
}
