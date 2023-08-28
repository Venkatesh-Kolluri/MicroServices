using BookStore.User.Entity;
using BookStore.User.Model;

namespace BookStore.User.Interface
{
    public interface IUserServices
    {
        UserEntity Register(UserModel user);
        string Login(string email, string password);
        string ForgetPassword(string email);
        string ResetPassword(string email,string newPassword, string confirmPassword);
        public UserEntity GetUserDetails(int user);

    }
}
