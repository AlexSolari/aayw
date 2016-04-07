using AAYW.Core.Annotations;
using AAYW.Core.Data.Providers;
using AAYW.Core.Models.Bussines.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAYW.Core.Data.Managers
{
    [ManagerFor(typeof(UserForm))]
    public class UserFormManager : BaseManager<UserFormProvider, UserForm>, IManager<UserForm>
    {
        public bool IsAvalibleForCreation(UserForm model)
        {
            var fromDb = GetByField("Url", model.Url);

            if (fromDb == null)
            {
                return true;
            }

            return false;
        }
    }
}
