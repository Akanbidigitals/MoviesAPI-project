using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Application.Repositories
{
    public interface IUserRepository
    {
        Task<bool> CreateUser(User user);
        Task<User> GetUser(Guid id);
        Task<bool> UpdateUser(User user);
        Task<bool> DeleteUser(Guid id);

        Task<List<User>> GetAllUsers();

        Task<string> Token(Guid Id);
    }
}
