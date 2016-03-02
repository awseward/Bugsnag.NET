namespace Bugsnag.PCL.Request
{
    public class Device : IDevice
    {
        public string Hostname { get; set; }

        public string OsVersion { get; set; }
    }
}
