using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestJWT.Interfaces
{
    public interface IJWTAuthentication
    {
        string Authenticate(string UserName, String Password);
    }
}
