using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cw4.Models
{
    public class EnrolmentError : Enrolment
    {

        public EnrolmentError(string errorText)
        {
            ErrorText = errorText;
        }
        public string ErrorText { get;  }

    }
}
