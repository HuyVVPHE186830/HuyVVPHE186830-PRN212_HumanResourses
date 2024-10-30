using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Objects;


namespace DataAccessLayer
{
    public class RoleDAO
    {
        private static List<Role> listRoles;
        public static List<Role> GetRoles()
        {
            listRoles = new List<Role>();
            try
            {
                using var db = new FuhrmContext();
                listRoles = db.Roles.ToList();
            }
            catch (Exception e) { }
            return listRoles;
        }
    }
}
