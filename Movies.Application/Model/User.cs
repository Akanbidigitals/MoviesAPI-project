using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Application.Model
{
    public  class User
    {
        [Key]
        public Guid UserId { get; set; } 
        public string Email { get; set; }

        public bool Admin { get; set; }
        public bool Trusted_member { get; set; }


    }
   


}
