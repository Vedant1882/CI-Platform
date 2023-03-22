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
        
        public bool UserExist(User user);
        
       
       
    }
}
