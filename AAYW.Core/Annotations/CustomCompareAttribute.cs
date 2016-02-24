using AAYW.Resources;
using AAYW.Core.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace AAYW.Core.Annotations
{
    public class CustomCompareAttribute : CompareAttribute
    {
        public CustomCompareAttribute(string otherProperty, [CallerMemberName] string PropertyResourceName = null)
            : base(otherProperty)
        {
            var firstPropName = ResourceAccessor.Instance.Get(otherProperty);
            var secondPropName = ResourceAccessor.Instance.Get(PropertyResourceName);

            ErrorMessage =
                ResourceAccessor.Instance.Get("Error_Compare")
                .FormatWith(firstPropName, secondPropName);
        }
    }
}
