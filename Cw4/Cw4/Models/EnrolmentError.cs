using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cw4.ModelsBaza;

namespace Cw4.Models
{
    public class EnrolmentError : Enrollment
    {

        public EnrolmentError(string errorText)
        {
            ErrorText = errorText;
        }
        public string ErrorText { get;  }

    }
}
