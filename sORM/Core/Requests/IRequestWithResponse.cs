﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sORM.Core.Requests
{
    public interface IRequestWithResponse : IRequest
    {
        Type GetResponseType();
        void SetResponseType<TType>();
        void SetResponseType(Type type);
    }
}
