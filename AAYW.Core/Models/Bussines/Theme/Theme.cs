using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAYW.Core.Models.Bussines.Theme
{
    public class Theme
    {
        [UIHint("Color")]
        public string Primary { get; set; }
        [UIHint("Color")]
        public string PrimaryText { get; set; }

        [UIHint("Color")]
        public string DarkPrimary { get; set; }
        [UIHint("Color")]
        public string DarkPrimaryText { get; set; }

        [UIHint("Color")]
        public string Divider { get; set; }

        [UIHint("Color")]
        public string SecondaryText { get; set; }

        [UIHint("Color")]
        public string Danger { get; set; }
        [UIHint("Color")]
        public string DangerPrimary { get; set; }
        [UIHint("Color")]
        public string DangerAccent { get; set; }

        [UIHint("Color")]
        public string Accent { get; set; }
    }
}
