﻿using AAYW.Core.Data.Providers;
using AAYW.Core.Models.Bussines.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAYW.Core.Data.Managers
{
    public class MailTemplateManager : BaseManager<MailTemplateProvider, MailTemplate>, IManager<MailTemplate>
    {
        public override void CreateOrUpdate(MailTemplate model)
        {
            if (CanCreate(model))
            {
                base.CreateOrUpdate(model);
            }
            else
            {
                throw new ArgumentException("Template with this name already exist");
            }
        }

        public bool CanCreate(MailTemplate model)
        {
            var secondModel = GetByField("Name", model.Name);

            return (secondModel == null || (secondModel.Id == model.Id));
        }
    }
}