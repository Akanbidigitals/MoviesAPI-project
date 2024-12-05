using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Movies.Api.DataAccess.DataContext;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Application.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationContext _ctx;
        private readonly Jwt _setup;
        public UserRepository(ApplicationContext ctx, IOptions<Jwt> setup)
        {
            _ctx = ctx;
            _setup = setup.Value;
        }
        public async Task<bool> CreateUser(User user)
        {
           var checkUser = await _ctx.Users.AnyAsync(x=>x.Email == user.Email);
            if(checkUser)
            {
                throw new ArgumentNullException("User already exist");
            }
             await _ctx.Users.AddAsync(user);
            var createuser = await _ctx.SaveChangesAsync();
            if(createuser > 0)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteUser(Guid id)
        {
            var user = await _ctx.Users.FirstOrDefaultAsync(x => x.UserId == id);
            if(user is null)
            {
                throw new ArgumentNullException("User does not exist");

            }
             _ctx.Users.Remove(user);
            var result = await _ctx.SaveChangesAsync();
            if (result > 0)
            {
                return true;
            }
            return false;
        }

        public async Task<List<User>> GetAllUsers() => await _ctx.Users.ToListAsync();
        

        public async Task<User> GetUser(Guid id)
        {
            var user = await _ctx.Users.FirstOrDefaultAsync(x => x.UserId == id);
            if(user is null)
            {
                throw new ArgumentNullException("User does not exist");

            }
            return user;
        }

       

        public async Task<bool> UpdateUser(User user)
        {
            var checkuser = await _ctx.Users.FirstOrDefaultAsync(x=>x.UserId == user.UserId);
            if(checkuser is null)
            {
                throw new ArgumentNullException("User does not exist");
            }
            checkuser.UserId = checkuser.UserId;
            checkuser.Email = user.Email ?? checkuser.Email;
            checkuser.Admin = user.Admin;
            checkuser.Trusted_member = user.Trusted_member;

            _ctx.Users.Update(user);
            var result = await _ctx.SaveChangesAsync();
            if(result > 0) { return true; } return false;
        }


        public async Task<string> Token(Guid Id)
        {
            var user = await _ctx.Users.FirstOrDefaultAsync(x=>x.UserId==Id);
            if(user is null)
            {
                throw new ArgumentNullException("User does not exist");

            }


            var claims = new List<Claim>
            {
                new (ClaimTypes.Email ,  user.Email),
                new Claim("userId",user.UserId.ToString()),
                new Claim("IsAdmin",user.Admin.ToString().ToLower()),
                new Claim("IsTrustedMember",user.Trusted_member.ToString().ToLower())


            };


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_setup.Key));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _setup.Issuer,
                signingCredentials: credentials,
                claims: claims,
                expires: DateTime.Now.AddMinutes(120)
                );
            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            return jwtToken;
            

        }
    }
}
