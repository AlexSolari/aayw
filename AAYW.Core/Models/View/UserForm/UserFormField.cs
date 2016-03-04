using AAYW.Core.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAYW.Core.Models.View.UserForm
{
    public class UserFormField
    {
        public enum InputType
        {
            Hidden,
            Text,
            Number,
            Password,
            DateTime
        }
        [CustomRequired]
        public string FieldName { get; set; }
        [CustomRequired]
        public InputType Type { get; set; }
        [CustomRequired]
        public bool Validate { get; set; }
        public bool Required { get; set; }
        public int Max { get; set; }
        public int Min { get; set; }
        public string Pattern { get; set; }
    }
}
