using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Helpers
{
    public static class BooleanExtensions
    {
        public static string GetStatus(this bool status)
        {
            if (status)
            {
                return "Active";
            }
            else
            {
                return "Inactive";
            }
        }
    }
}
