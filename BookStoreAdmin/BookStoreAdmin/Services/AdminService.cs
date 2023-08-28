using BookStoreAdmin.Entity;
using BookStoreAdmin.Interface;
using BookStoreAdmin.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookStoreAdmin.Services
{
    public class AdminService : IAdmin
    {

        private readonly AdminContext _db;
        private readonly IConfiguration _config;

        public AdminService(AdminContext db, IConfiguration config)
        {
            _db = db;
            _config = config;
        }
        public AdminEntity RegisterAdmin(AdminModel adminModel)
        {
            try
            {
                AdminEntity adminEntity = new AdminEntity();
                adminEntity.AName = adminModel.AName;
                adminEntity.Email = adminModel.Email;
                adminEntity.PhoneNumber = adminModel.PhoneNumber;
                adminEntity.Password = adminModel.Password;

                _db.Admin.Add(adminEntity);
                _db.SaveChanges();
                if (adminEntity != null)
                {
                    return adminEntity;
                }
                else return null;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string AdminLogin(string email, string password)
        {
            try
            {

                AdminEntity admin = _db.Admin.FirstOrDefault(x => x.Email == email && x.Password == password);
                if (admin != null)
                {
                    string token = GenerateSecurityToken(admin.AdminId, admin.Email);
                    return token;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string GenerateSecurityToken(int adminId, string email)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config["JWT-Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                     new Claim(ClaimTypes.Role, "Admin"),
                    new Claim(ClaimTypes.Email, email),
                     new Claim("adminId", adminId.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(60),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
