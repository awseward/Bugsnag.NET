using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bugsnag.Common
{
    public static class ExceptionExtensions
    {
        public static string GenerateDeepTypesKey(this Exception ex)
        {
            switch (ex)
            {
                case null:
                    return "";

                case AggregateException aggEx:
                    return $"{aggEx.GetType().Name}[{GenerateDeepTypesKey(aggEx.InnerExceptions)}]";

                default:
                    return $"{ex.GetType().Name}({GenerateDeepTypesKey(ex.InnerException)})";
            }
        }

        public static string GenerateDeepTypesKey(this IEnumerable<Exception> exs) =>
            string.Join(
                separator: ",",
                values: exs.Select(GenerateDeepTypesKey).ToList());
    }
}
