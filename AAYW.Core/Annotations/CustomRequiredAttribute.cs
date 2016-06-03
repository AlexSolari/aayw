using AAYW.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AAYW.Core.Annotations
{
    public class CustomRequiredAttribute : System.ComponentModel.DataAnnotations.RequiredAttribute
    {
        public CustomRequiredAttribute([CallerMemberName] string PropertyResourceName = null)
            : base()
        {
            var propName = ResourceAccessor.Instance.Get(PropertyResourceName);
            ErrorMessage =
                ResourceAccessor.Instance.Get("Error_Required")
                .FormatWith(propName);
        }
    }
}
