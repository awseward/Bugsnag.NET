using System;
using System.Reflection;

namespace Bugsnag.PCL.Request
{
    public class Notifier : INotifier
    {
        readonly string _name = "Bugsnag.PCL";

        public string Name { get { return _name; } }

        string _version = new Func<string>(() =>
        {
            var assembly = typeof(Notifier).GetTypeInfo().Assembly;
            var assemblyName = new AssemblyName(assembly.FullName);
            return assemblyName.Version.ToString();
        }).Invoke();

        public string Version { get { return _version; } }

        readonly string _url = "https://github.com/awseward/Bugsnag.NET";

        public string Url { get { return _url; } }
    }
}
