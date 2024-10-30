using Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class DepartmentDAO
    {
        private static List<Department> listDepartments;
        public static List<Department> GetDepartments()
        {
            listDepartments = new List<Department>();
            try
            {
                using var db = new FuhrmContext();
                listDepartments = db.Departments.ToList();
            }
            catch (Exception e) { }
            return listDepartments;
        }
    }
}
