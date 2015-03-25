namespace :build do
  def build(configuration)
    sln = Dir.glob('**/*.sln').first
    target = 'Clean;Rebuild'

    sh "MSBuild.exe #{sln} /p:Configuration=#{configuration} /target:'#{target}'"
  end

  task :release => 'nuget:restore' do
    build 'Release'
  end

  desc 'Run MSBuild for debug configuration'
  task :debug => 'nuget:restore' do
    build 'Debug'
  end
end

desc 'Run MSBuild for release configuration'
task :build => 'build:release'
