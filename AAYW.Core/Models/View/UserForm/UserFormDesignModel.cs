﻿using AAYW.Core.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAYW.Core.Models.View.UserForm
{
    public class UserFormDesignModel
    {
        public string Id { get; set; }
        public List<UserFormField> FormFields { get; set; }
        [CustomRequired]
        public string Header { get; set; }
        [CustomRequired]
        public string Url { get; set; }
        [CustomRequired]
        public string MailTemplateName { get; set; }
        [CustomRequired]
        [DataType(DataType.EmailAddress)]
        public string SubmitAdress { get; set; }

        public UserFormDesignModel()
        {
            FormFields = new List<UserFormField>();
        }

        public UserFormDesignModel(Guid id)
            : this()
        {
            Id = id.ToString();
        }
    }
}
