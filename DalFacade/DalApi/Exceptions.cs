using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi
{
    public class ExceptionExists : Exception
    {
        public override string Message => "Error: Item already exists.";
    }
    public class ExceptionNotExists : Exception
    {
        public override string Message => "Error: Item does not exist.";
    }
}
