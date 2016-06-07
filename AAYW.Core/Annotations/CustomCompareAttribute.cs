using AAYW.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using AAYW.Core.Api;

namespace AAYW.Core.Annotations
{
    public class CustomCompareAttribute : CompareAttribute
    {
        public CustomCompareAttribute(string otherProperty, [CallerMemberName] string PropertyResourceName = null)
            : base(otherProperty)
        {
            var firstPropName = SiteApi.Texts.Get(otherProperty);
            var secondPropName = SiteApi.Texts.Get(PropertyResourceName);

            ErrorMessage =
                SiteApi.Texts.Get("Error_Compare")
                .FormatWith(firstPropName, secondPropName);
        }
    }
}
