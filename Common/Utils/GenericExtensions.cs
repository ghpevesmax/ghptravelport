using Common.Lookups;
using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Common.Utils
{
    public static class GenericExtensions
    {
        public static IList<string> OrderSplit(this IList<string> str, string separator) 
        {
            var strQuery = str.AsQueryable();

            return strQuery.OrderBy(_ => _)
                .Select(_ => _
                    .Split(separator, StringSplitOptions.None)
                    .LastOrDefault()
                ).ToList();
        }
    }
}
