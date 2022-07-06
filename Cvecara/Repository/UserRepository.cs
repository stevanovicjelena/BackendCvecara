using AutoMapper;
using Cvecara.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cvecara.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly CvecaraContext context;
        private readonly IMapper mapper;

        public UserRepository(CvecaraContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public List<User> GetAllUsers()
        {
            return context.tblUser.ToList();
        }
        public User GetUserById(int UserId)
        {
            return context.tblUser.FirstOrDefault(a => a.userID == UserId);
        }
        public User CreateUser(User User)
        {
            var createdEntity = context.Add(User);
            return mapper.Map<User>(createdEntity.Entity);
        }

        public void DeleteUser(int UserId)
        {
            var User = GetUserById(UserId);
            context.Remove(User);
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public void UpdateUser(User User)
        {
            throw new NotImplementedException();
        }
    }
}
