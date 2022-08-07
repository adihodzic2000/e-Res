using Common.Dto.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IRoleService
    {
        Task<Message> CreateRoleAsMessageAsync(RoleCreateDto roleCreateDto, CancellationToken cancellationToken);
        Task<Message> DeleteRoleAsMessageAsync(Guid roleId, CancellationToken cancellationToken);
        Task<Message> AddRoleToUserAsMessageAsync(UserRoleDto userRoleDto, CancellationToken cancellationToken);
    }
}
