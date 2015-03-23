namespace :nuget do
  desc "Removes installed NuGet packages"
  task :clean do
    rm_rf "packages"
  end

  desc "Installs NuGet dependencies"
  task :restore do
    sh "nuget restore ."
  end

  task :pack => :build do
    # FIXME: Un-hardcode this
    sh "nuget pack Bugsnag.NET/Bugsnag.NET.csproj -Prop Configuration=Release"
  end
end

desc "Builds a NuGet package"
task :nuget => 'nuget:pack'

