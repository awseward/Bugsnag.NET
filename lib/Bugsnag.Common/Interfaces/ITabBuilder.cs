using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bugsnag.Common.Interfaces
{
    public interface IFluentTabBuilder
    {
        IFluentTabBuilder Add(string value);
        IFluentTabBuilder Add(string key, string value);
        IFluentTabBuilder Add(string key, IEnumerable<string> values);
        IFluentTabBuilder Add(string key, object value);

        IFluentTabBuilder Tab(string tabName);
        IFluentTabBuilder Tab(string tabName, Action<IFluentTabBuilder> action);
    }
}
