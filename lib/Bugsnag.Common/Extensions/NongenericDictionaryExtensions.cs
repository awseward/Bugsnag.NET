using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bugsnag.Common.Extensions
{
    public static class NongenericDictionaryExtensions
    {
        public static IDictionary<string, object> ToGeneric(this IDictionary nonGeneric)
        {
            IDictionary<string, object> generic = new Dictionary<string, object>();
            foreach (DictionaryEntry entry in nonGeneric)
            {
                if (entry.Key is string)
                {
                    generic.Add((string)entry.Key, entry.Value);
                }
            }
            return generic;
        }
    }
}
