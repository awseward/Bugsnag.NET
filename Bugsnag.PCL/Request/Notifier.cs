using System;
using System.Reflection;
using Bugsnag.Common;

namespace Bugsnag.PCL.Request
{
    public class Notifier : INotifier
    {
        public string Name { get; } = "Bugsnag.PCL";

        public string Version { get; } = new Func<string>(() =>
        {
            var assembly = typeof(Notifier).GetTypeInfo().Assembly;
            var assemblyName = new AssemblyName(assembly.FullName);
            return assemblyName.Version.ToString();
        }).Invoke();

        public string Url { get; } = "https://github.com/awseward/Bugsnag.NET";
    }
}
