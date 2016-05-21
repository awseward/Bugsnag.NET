# Bugsnag.NET [![Build status](https://ci.appveyor.com/api/projects/status/1j8qee5j2bxmle08/branch/master?svg=true)](https://ci.appveyor.com/project/datNET/bugsnag-net/branch/master) [![NuGet version](https://badge.fury.io/nu/Bugsnag.NET.svg)](https://badge.fury.io/nu/Bugsnag.NET) [![License](http://img.shields.io/:license-mit-blue.svg?style=flat-square)](http://badges.mit-license.org)

A .NET client for sending exception information to Bugsnag

## Usage
A simple example of how one might use this client.
```csharp
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

  object GetMetadata() { /* See Metadata section below */ }
```

### Metadata
Metadata can be passed in as anonymous types, as Bugsnag expects key/value
pairs for these.

For example, the following will create two extra tabs in Bugnsag's online
interface which will have the labels "firstTab" and "secondTab", respectively:
```cs
object GetMetadata() {
{
  return new {
    firstTab = new {
      someKey = "some value",
    },
    secondTab = new {
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
  return new {
    keyUnderCustomTab = "some value",
    anotherKeyUnderCustomTab = "another value",
  }
}
```
