﻿using AAYW.Core.Models.Bussines.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAYW.Core.Data.Providers
{
    public class MailTemplateProvider : BaseProvider<MailTemplate>
    {
        public MailTemplateProvider() : base() { }
        public MailTemplateProvider(bool suppressLogging) : base(suppressLogging) { }
    }
}
