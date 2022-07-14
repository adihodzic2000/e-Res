using Core.Dto;
using Core.Dto.Role;
using Entities;
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
    }
}
