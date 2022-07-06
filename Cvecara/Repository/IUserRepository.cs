using Cvecara.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cvecara.Repository
{
    public interface IUserRepository
    {
        User GetUserById(int UserId);
        List<User> GetAllUsers();
        User CreateUser(User User);
        void UpdateUser(User User);
        void DeleteUser(int UserId);
        bool SaveChanges();
    }
}
