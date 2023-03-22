using CI_Entity.Models;
using CI_Platform.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Repository.Repository
{
    public class UserRepository:IUserRepository
    {
        public readonly CIDbContext _CIDbContext;

        public UserRepository(CIDbContext CIDbContext)
        {
            _CIDbContext = CIDbContext;


        }
        
        public bool UserExist(User user)
        {
            var obj=_CIDbContext.Users.Where(u => u.Email == user.Email).FirstOrDefault();
            if (obj != null)
            {
                return false;
            }
            else
            {
                _CIDbContext.Users.Add(user);
                _CIDbContext.SaveChanges();
                return true;
            }
        }
        



    }
}
