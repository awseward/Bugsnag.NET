namespace :nuget do
  def _nuget_pack
    # FIXME: Improve this
    dir = File.dirname Dir.glob('**/*.nuspec').first
    csproj = Dir.glob("#{dir}/*.csproj").first
    sh "nuget pack #{csproj} -Prop Configuration=Release"
  end

  desc "Removes installed NuGet packages"
  task :clean do
    rm_rf "packages"
  end

  desc "Installs NuGet dependencies"
  task :restore => :clean do
    sh "nuget restore ."
  end

  task :pack => :build do
    _nuget_pack
  end
end

desc "Builds a NuGet package"
task :nuget => 'nuget:pack'
