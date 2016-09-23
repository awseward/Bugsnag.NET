using Bugsnag.Common;

namespace Bugsnag.PCL.Request
{
    public class App : IApp
    {
        public string Version { get; set; }

        public string ReleaseStage { get; set; }
    }
}
