using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Helpers.Interfaces
{
    public interface ICaptchaHelper
    {
        string Validate(string response);
    }
}
