using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace finalStore.AdditionalMethods
{
    public static class EmptinessChecker
    {
        public static bool CheckEmptiness(params string[] fields)
        {
            foreach (var field in fields)
            {
                if (string.IsNullOrWhiteSpace(field)) return true;
            }
            return false;
        }
    }
}
