using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dto.Role
{
    public class UserRoleDto
    {
        public Guid UserId { get; set; }
        public List<Guid> RoleIds { get; set; }
    }
}
