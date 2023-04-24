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
        
        public bool UserExist(string FirstName, string LastName, string Email, long PhoneNumber, string ConfirmPassword)
        {
            var obj=_CIDbContext.Users.Where(u => u.Email == Email).FirstOrDefault();
            var userData = new User
            {
                FirstName = FirstName,
                LastName = LastName,
                PhoneNumber = PhoneNumber,
                Email = Email,
                Password = ConfirmPassword,
            };
            if (obj != null)
            {
                return false;
            }
            else
            {
                _CIDbContext.Users.Add(userData);
                _CIDbContext.SaveChanges();
                return true;
            }
        }
        public List<Banner> AllBanners()
        {
            return _CIDbContext.Banners.Where(b => b.DeletedAt == null).ToList();
        }



    }
}
