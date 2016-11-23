using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectA.Core.Attributes
{
    public class RequeiredPropertyException : Exception
    {
        public RequeiredPropertyException() : base("Property is required")
        {

        }
    }
}
