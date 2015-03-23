namespace :clean do
  def git_clean_ignore_patterns
    File.open(".git-clean-ignore", "r") do |f|
      return f.each_line.map(&:chomp)
    end
  end

  def build_ignore_flags(patterns)
    patterns.map{ |p| "-e '#{p}'" }.join(' ')
  end

  git_clean = "git clean"
  base_flags = "xdf"
  ignore_flags = build_ignore_flags git_clean_ignore_patterns
  dry_flag = "n"

  task :clean do
    sh "#{git_clean} -#{base_flags} #{ignore_flags}"
  end

  desc "Dry run of `clean:clean`"
  task :dry do
    sh "#{git_clean} -#{base_flags}#{dry_flag} #{ignore_flags}"
  end
end

desc "Cleans untracked/git-ignored files, except for patterns in `.git-clean-ignore`"
task :clean => 'clean:clean'
