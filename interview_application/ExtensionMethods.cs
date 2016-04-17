using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewApp
{
    public static class ExtensionMethods
    {
        public static bool IsBetween(this int source, int min, int max)
        {
            return source >= min && source <= max;
        }
    }
}
