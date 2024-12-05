using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Contracts.Requests
{
    public class CreateUserRequest
    {
        public string Email { get; set; }

        public bool Admin { get; set; }
        public bool Trusted_member { get; set; }
    }
   
}
