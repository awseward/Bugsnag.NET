﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bugsnag.Common
{
    [JsonObject(MemberSerialization.OptIn)]
    public interface IDevice
    {
        [JsonProperty("osVersion")]
        string OsVersion { get; }

        [JsonProperty("hostname")]
        string Hostname { get; }
    }
}
