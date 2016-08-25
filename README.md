# Bugsnag.NET [![Build status](https://ci.appveyor.com/api/projects/status/1j8qee5j2bxmle08/branch/master?svg=true)](https://ci.appveyor.com/project/datNET/bugsnag-net/branch/master) [![NuGet version](https://badge.fury.io/nu/Bugsnag.NET.svg)](https://badge.fury.io/nu/Bugsnag.NET) [![License](http://img.shields.io/:license-mit-blue.svg?style=flat-square)](http://badges.mit-license.org)

A .NET client for sending exception information to Bugsnag

## Installation
```
Install-Package Bugsnag.NET
```

### For PCLs
```
Install-Package Bugsnag.PCL
```

## Usage
Some simple examples of how to use this client.

### Configuration
#### Use the default `IBugsnagger` instance
This library provides a default `IBugsnagger` instance you can set some properties on to set up your integration.

Your setup might look something like this.
```cs
using System;
// ...
using Bugsnag.NET;
using Bugsnag.NET.Extensions;
using BsReq = Bugsnag.NET.Request;

// ...

    static void _InitBugsnag()
    {
        var snagger = Bugsnagger.Default;

        snagger.ApiKey = "YOUR_API_KEY_HERE";
        snagger.App = new BsReq.App
        {
            Version = "1.2.3",
            ReleaseStage = "test",
        };
    }

// ...
```

#### Bring your own `IBugsnagger`
Alternatively, you can choose to create your own `IBugsnagger` instance (or multiple), and hold references to it/them somewhere.

That might look something like this.
```cs
using System;
// ...
using Bugsnag.NET;
using Bugsnag.NET.Extensions;
using BsReq = Bugsnag.NET.Request;

// ...

    static IBugsnagger _Snagger { get; } = new Bugsnagger
    {
        ApiKey = "YOUR_API_KEY_HERE",
        App = new BsReq.App
        {
            Version = "1.2.3",
            ReleaseStage = "test",
        },
    };

// ...
```

### Capture Errors
Depending on which approach you choose to take in your application, a simple setup to start sending application errors to Bugsnag might look like one of the following.

#### Default `IBugsnagger` instance
```cs
using System;
// ...
using Bugsnag.NET;
using Bugsnag.NET.Extensions;
using BsReq = Bugsnag.NET.Request;

class Program
{
    static void Main(string[] args)
    {
        _InitBugsnag();

        try
        {
            // Your app code here
        }
        catch (Exception ex)
        {
            _NotifyBugsnag(ex);

            throw;
        }
    }

    static void _InitBugsnag()
    {
        var snagger = Bugsnagger.Default;

        snagger.ApiKey = "YOUR_API_KEY_HERE";
        snagger.App = new BsReq.App
        {
            Version = "1.2.3",
            ReleaseStage = "test",
        };
    }

    static void _NotifyBugsnag(Exception ex)
    {
        var snagger = Bugsnagger.Default;
        var snagEvent = snagger.CreateEvent(
            BsReq.Severity.Error,
            ex,
            _GetUser(),
            _GetMetadata());

        snagger.Notify(snagEvent);
    }

    static BsReq.IUser _GetUser() { /* ... */ }

    static object _GetMetadata() { /* See Metadata section */ }
}
```

#### Custom `IBugsnagger` instance
```cs
using System;
// ...
using Bugsnag.NET;
using Bugsnag.NET.Extensions;
using BsReq = Bugsnag.NET.Request;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            // Your app code here
        }
        catch (Exception ex)
        {
            var snagEvent = _Snagger.CreateEvent(BsReq.Severity.Error, ex, null, null);
            _Snagger.Notify(snagEvent);

            throw;
        }
    }

    static IBugsnagger _Snagger { get; } = new Bugsnagger
    {
        ApiKey = "YOUR_API_KEY_HERE",
        App = new BsReq.App
        {
            Version = "1.2.3",
            ReleaseStage = "test"
        },
    };

    static void _NotifyBugsnag(Exception ex)
    {
        var snagEvent = _Snagger.CreateEvent(
            BsReq.Severity.Error,
            ex,
            _GetUser(),
            _GetMetadata());

        _Snagger.Notify(snagEvent);
    }

    static BsReq.IUser _GetUser() { /* ... */ }

    static object _GetMetadata() { /* See Metadata section */ }
}
```

### Metadata
Metadata can be passed in as anonymous types, as Bugsnag expects key/value
pairs for these.

For example, the following will create two extra tabs in Bugnsag's online
interface which will have the labels "firstTab" and "secondTab", respectively:
```cs
object GetMetadata() {
{
    return new
    {
        firstTab = new
        {
            someKey = "some value",
        },
        secondTab = new
        {
            anotherKey = "another value",
        },
    };
}
```

For keys/values at the root level of this metadata object, Bugsnag creates a tab
for you with the label "Custom", and displays them there. That case would look
like this:
```cs
object GetMetadata() {
    return new
    {
        keyUnderCustomTab = "some value",
        anotherKeyUnderCustomTab = "another value",
    }
}
```

---

### Statically Configured Approach
This was the initial implementation of this tool's interface.

It consists solely of static properties, which makes it slightly less flexible. Because of this, its use is discouraged and is being considered for deprecation.
```csharp
//
using BsNET = Bugsnag.NET;
using BsReq = Bugsnag.NET.Request;

...

  // Set configuration attributes
  public void InitBugsnag()
  {
      BsNET.Bugsnag.ApiKey = "YOUR_API_KEY";
      BsNET.Bugsnag.App = new App
      {
          Version = "1.0.24",
          ReleaseStage = "production"
      };
  }

  // Report an unhandled exception
  public void OnUnhandled(Exception ex)
  {
      Report(BsNET.Bugsnag.Error, ex);
  }

  // Report a handled exception with `Warning` severity
  public void OnHandled_Warn(Exception ex)
  {
      Report(BsNet.Bugsnag.Warning, ex);
  }

  // Report a handled exception with `Info` severity
  public void OnHandled_Info(Exception ex)
  {
      Report(BsNET.Bugsnag.Info, ex);
  }

  void Report(BsNET.Bugsnag client, Exception ex)
  {
      var evt = client.GetEvent(ex, GetCurrentUser(), GetMetaData());
      client.Notify(evt);
  }

  BsReq.IUser GetCurrentUser() { ... }

  object GetMetadata() { /* See Metadata section */ }
```
