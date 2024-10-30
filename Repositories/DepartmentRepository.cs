using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Objects;
using DataAccessLayer;
namespace Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        public List<Department> GetDepartments() => DepartmentDAO.GetDepartments();
    }
}
