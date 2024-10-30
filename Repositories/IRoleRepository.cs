using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Objects;

namespace Repositories
{
    public interface IRoleRepository
    {
        List<Role> GetRoles();
    }
}
