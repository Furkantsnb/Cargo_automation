using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results.Concrete
{
    public class ErrorDataResult : Result
    {
        public ErrorDataResult() : base(false)
        {
        }

        public ErrorDataResult( string message) : base(false, message)
        {
        }
       
    }
}
