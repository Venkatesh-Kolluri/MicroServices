using BookStore.User.Entity;
using BookStore.User.Interface;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.IdentityModel.Tokens;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BookStore.User.Model;

namespace BookStore.User.Service
{
    public class UserServices : IUserServices
    {
        private readonly UserContext _db;
        private readonly IConfiguration _config;

        public UserServices(UserContext db, IConfiguration config)
        {
            _db = db;
            _config = config;
        }
        public UserEntity Register(UserModel user)
        {
            try
            {
                UserEntity userEntity = new UserEntity();
                userEntity.Name = user.Name;
                userEntity.PhoneNumber = user.PhoneNumber;
                userEntity.Email = user.Email;
                userEntity.Password = user.Password;
            
                _db.Users.Add(userEntity);
                _db.SaveChanges();
                if (userEntity != null)
                {
                    return userEntity;
                }
                else return null;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public string Login(string email, string password)
        {
            try
            {

                UserEntity user = _db.Users.FirstOrDefault(x => x.Email == email && x.Password == password);
                if (user != null)
                {
                    string token = GenerateSecurityToken(user.UserId, user.Email);
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

        public string ForgetPassword(string email)
        {
            try
            {
                UserEntity userEntity = new UserEntity();
                userEntity = _db.Users.FirstOrDefault(u => u.Email == email);
                if (userEntity != null)
                {
                    var Token = GenerateSecurityToken(userEntity.UserId, userEntity.Email);
                    MSMQModel msmqModel = new MSMQModel();
                    msmqModel.sendData2Queue(Token);
                    return Token;
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

        public string ResetPassword(string email,string newPassword, string confirmPassword)
        {
            try
            {
                if (newPassword.Equals(confirmPassword))
                {
                    UserEntity userEntity = new UserEntity();
                    userEntity = _db.Users.FirstOrDefault(x => x.Email == email);
                    userEntity.Password = newPassword;
                    _db.SaveChanges();
                    return "Password changed successfully";

                }
                else
                {
                    return "Please enter confirmPassword Correctly";
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public UserEntity GetUserDetails(int userId)
        {
            try
            {

                var result = _db.Users.FirstOrDefault(x=> x.UserId== userId);
                
                if (result != null)
                {
                    result.Password = null;
                    
                    return result;
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

        public string GenerateSecurityToken(int userId, string email)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config["JWT-Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Email, email),
                    new Claim(ClaimTypes.Sid, userId.ToString()),


                     new Claim("userId", userId.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(60),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

    }
}
