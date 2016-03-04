using AAYW.Core.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AAYW.Core.Models.View.MailTemplates
{
    public class MailTemplateCreateModel
    {
        public virtual Guid Id { get; set; }
        [CustomRequired]
        public virtual string Name { get; set; }
        [CustomRequired]
        [AllowHtml]
        [CustomMaxLength(2000)]
        [DataType(DataType.MultilineText)]
        public virtual string Body { get; set; }

        public MailTemplateCreateModel()
        {
            Id = Guid.NewGuid();
        }
    }
}
