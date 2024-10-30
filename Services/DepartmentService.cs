using Objects;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository iDepartmentRepository;
        public DepartmentService()
        {
            iDepartmentRepository = new DepartmentRepository();
        }
        public List<Department> GetDepartments()
        {
            return iDepartmentRepository.GetDepartments();
        }
    }
}
