using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class UserService : ServiceBase, IService<User, UserModel>
    {
        public UserService(Db db) : base(db)
        {
        }

        public IQueryable<UserModel> Query()
        {
            return _db.Users.Include(u => u.Role).OrderByDescending(u => u.IsActive).Select(u => new UserModel() { Record = u });
        }

        public ServiceBase Update(User entity)
        {
            throw new NotImplementedException();
        }

        ServiceBase IService<User, UserModel>.Create(User record)
        {
            throw new NotImplementedException();
        }

        ServiceBase IService<User, UserModel>.Delete(int id)
        {
            throw new NotImplementedException();
        }

        ServiceBase IService<User, UserModel>.Update(User record)
        {
            throw new NotImplementedException();
        }
    }
}
