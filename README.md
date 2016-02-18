# Bugsnag.NET [![Build status](https://ci.appveyor.com/api/projects/status/86j23g6eltupv6ox?svg=true)](https://ci.appveyor.com/project/mglodack/bugsnag-net) [![NuGet Status](http://nugetstatus.com/Bugsnag.NET.png)](http://nugetstatus.com/packages/Bugsnag.NET)

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

  object GetMetadata() { ... }
```
