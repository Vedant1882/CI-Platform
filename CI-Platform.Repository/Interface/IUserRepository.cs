using CI_Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Repository.Interface
{
    public interface IUserRepository
    {
        
        public bool UserExist(string FirstName, string LastName, string Email, long PhoneNumber, string ConfirmPassword);
        
       
       
    }
}
