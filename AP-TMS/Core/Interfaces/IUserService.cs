using Core.Dto.User;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IUserService
    {
        Task<Message> CreateUserAsMessageAsync(UserCreateDto user, CancellationToken cancellationToken);
    }
}
