using DataAccessLayer;
using Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class LeaveRequestRepository : ILeaveRequestRepository
    {
        public List<LeaveRequest> GetLeaveRequests() => LeaveRequestDAO.GetLeaveRequests();
        public LeaveRequest GetLeaveRequestById(int id) => LeaveRequestDAO.GetLeaveRequestById(id);
        public void AddLeaveRequest(LeaveRequest leaveRequest) => LeaveRequestDAO.AddLeaveRequest(leaveRequest);
        public void UpdateLeaveRequest(LeaveRequest leaveRequest) => LeaveRequestDAO.UpdateLeaveRequest(leaveRequest);
        public void DeleteLeaveRequest(LeaveRequest leaveRequest) => LeaveRequestDAO.DeleteLeaveRequest(leaveRequest);
    }
}
