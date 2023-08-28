using BookStore.User.Entity;
using BookStore.User.Interface;
using BookStore.User.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;
using System.Security.Claims;

namespace BookStore.User.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _user;
        private readonly ResponseEntity response;
        private readonly UserContext _db;

        public UserController(IUserServices user,UserContext db)
        {
            _user = user;
            response = new ResponseEntity();
            _db = db;

        }

        [HttpPost]
        [Route("register")]
        public ResponseEntity RegisterUser(UserModel newUser)
        {
            try
            {
                UserEntity user = _user.Register(newUser);

                if (user != null)
                {
                    response.Data = user;
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "Something went wrong";
                }
                return response;
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        [Route("login")]
        public ResponseEntity Login(string email, string password)
        {
            try
            {

                string result = _user.Login(email, password);

                if (result != null)
                {
                    response.Data = result;
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "Something went wrong";
                }
                return response;
            }
            catch (Exception)
            {

                throw;
            }

        }



        [HttpPost]
        [Route("forgetpassword")]
        public ResponseEntity ForgetPassword(string email)
        {
            try
            {

                var user = _user.ForgetPassword(email);

                if (user != null)
                {
                    response.Data = user;
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "Something went wrong";
                }
                return response;
            }
            catch (Exception)
            {

                throw;
            }
            

        }

        [Authorize]
        [HttpPut]
        [Route("resetpassword")]
        public ResponseEntity ResetPassword(string newPassword,string confirmPassword)
        {
            try
            {
                string email = User.FindFirstValue( ClaimTypes.Email);
                var user = _user.ResetPassword(email,newPassword,confirmPassword);

                if (user != null)
                {
                    response.Data = user;
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "Something went wrong";
                }
                return response;
            }
            catch (Exception)
            {

                throw;
            }


        }

        [Authorize]
        [HttpGet]
        [Route("getuserdetails")]
        public ResponseEntity GetUserDetails()
        {
            try
            {
                int result = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "userId").Value);
                var user = _user.GetUserDetails(result);
               

                if (user != null)
                {
                    response.Data = user;
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "Something went wrong";
                }
                return response;
            }
            catch (Exception)
            {

                throw;
            }


        }


    }
}
