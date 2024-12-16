using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Contracts.Responses
{
    public class UsersResponse
    {
        public required IEnumerable<UserResponse> ListOfUsers { get; init; } = Enumerable.Empty<UserResponse>();
    }
}
