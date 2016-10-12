using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAS.ServiceCall
{
    public interface IServiceCalls
    {
        bool LoginWP(string username, string password);
    }
}
