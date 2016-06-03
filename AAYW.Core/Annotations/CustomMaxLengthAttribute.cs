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
    public class CustomMaxLengthAttribute : StringLengthAttribute
    {
        public bool PlainTextOnly { get; set; }

        public CustomMaxLengthAttribute(int MaxLength, int MinLength = 0, [CallerMemberName] string PropertyResourceName = null)
            : base(MaxLength)
        {
            MinimumLength = MinLength;
            var propName = ResourceAccessor.Instance.Get(PropertyResourceName);
            ErrorMessage =
                ResourceAccessor.Instance.Get("Error_MaxLength")
                .FormatWith(propName, MaxLength, MinimumLength);
        }

        public override bool IsValid(object value)
        {
            return (PlainTextOnly) ? ((string)value).StripTags().Length.InRange(MinimumLength, MaximumLength) : base.IsValid(value);
        }
    }
}
