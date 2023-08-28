using BookStoreAdmin.Entity;
using BookStoreAdmin.Models;

namespace BookStoreAdmin.Interface
{
    public interface IAdmin
    {
        AdminEntity RegisterAdmin(AdminModel adminModel);
        string AdminLogin(string email, string password);
    }
}
