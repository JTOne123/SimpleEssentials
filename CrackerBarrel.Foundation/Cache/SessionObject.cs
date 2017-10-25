using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrackerBarrel.Foundation.Cache
{
    public class SessionObject<T>
    {
        public T Data;
        public DateTime? Expiration;
    }
}
