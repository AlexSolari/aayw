﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sORM.Core.Requests
{
    public interface IParametrizedRequest : IRequest
    {
        void AddParameter(string fieldname, object value);
    }
}
