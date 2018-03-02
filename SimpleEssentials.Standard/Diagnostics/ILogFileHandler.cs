using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleEssentials.Diagnostics
{
    public interface ILogFileHandler
    {
        void Insert(string msg);
    }
}
