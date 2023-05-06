using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VKInternshipTask.Application.Common.Exceptions
{
    public class ConflictActionException : Exception
    {
        public ConflictActionException(string message)
            : base(message) { }
    }
}
