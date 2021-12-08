using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerafTelis.db
{
    [Serializable]
    public class BDException : Exception
    {
        public BDException(string message) : base(message) { }
    }
}
