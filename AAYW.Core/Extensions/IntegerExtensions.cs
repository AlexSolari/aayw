using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace AAYW.Core.Extensions
{
    public static class IntegerExtensions
    {
        public static bool InRange(this int value, int min, int max)
        {
            return value >= min && value <= max;
        }
    }
}
