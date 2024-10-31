using Objects;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class LeaveRequestService : ILeaveRequestService
    {
        private readonly ILeaveRequestRepository leaveRequestRepository;
        public LeaveRequestService()
        {
            leaveRequestRepository = new LeaveRequestRepository();
        }
        public void AddLeaveRequest(LeaveRequest leaveRequest)
        {
            leaveRequestRepository.AddLeaveRequest(leaveRequest);
        }
        public void UpdateLeaveRequest(LeaveRequest leaveRequest)
        {
            leaveRequestRepository.UpdateLeaveRequest(leaveRequest);
        }
        public void DeleteLeaveRequest(LeaveRequest leaveRequest)
        {
            leaveRequestRepository.DeleteLeaveRequest(leaveRequest);
        }
        public List<LeaveRequest> GetLeaveRequests()
        {
            return leaveRequestRepository.GetLeaveRequests();
        }
        public LeaveRequest GetLeaveRequestById(int leaveRequestId)
        {
            return leaveRequestRepository.GetLeaveRequestById(leaveRequestId);
        }
    }
}
