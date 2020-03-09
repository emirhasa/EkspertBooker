using System;
using System.Collections.Generic;
using System.Text;

namespace EkspertBookerMobileApp.Validation
{
    public static class ValidationRules
    {
        public static bool IsNullOrEmptyRule<T>(T value)
        {
            var str = value as string;
            if (str == null)
                return false;

            return !string.IsNullOrWhiteSpace(str);
        }

    }
}
