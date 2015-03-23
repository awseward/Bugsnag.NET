Dir.glob('tasks/*.rake').each { |r| import r }

desc "Builds the project"
task :build => ['nuget:clean', 'nuget:restore'] do
  # FIXME: Un-hardcode this
  sh "MSBuild.exe Bugsnag.NET.sln /p:Configuration=Release /target:'Clean;Rebuild'"
end
