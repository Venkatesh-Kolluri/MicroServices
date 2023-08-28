using BookStoreAdmin.Entity;
using BookStoreAdmin.Interface;
using BookStoreAdmin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookStoreAdmin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdmin _admin;
        private readonly ResponseEntity response;
        private readonly AdminContext _db;

        public AdminController(IAdmin admin, AdminContext db)
        {
            _admin = admin;
            response = new ResponseEntity();
            _db = db;

        }

        [HttpPost]
        [Route("register")]
        public ResponseEntity RegisterUser(AdminModel newUser)
        {
            try
            {
                AdminEntity admin = _admin.RegisterAdmin(newUser);

                if (admin != null)
                {
                    response.Data = admin;
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

                string result = _admin.AdminLogin(email, password);

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
    }
}
