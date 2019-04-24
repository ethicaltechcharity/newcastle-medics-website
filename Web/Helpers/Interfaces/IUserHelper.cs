using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Helpers.Interfaces
{
    public interface IUserHelper
    {
        UserViewModel BuildUserViewModel(UserViewModel model);

        UserViewModel BuildUserViewModel(string id);

        void EditUser(UserViewModel model);
    }
}
