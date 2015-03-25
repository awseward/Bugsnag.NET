namespace :clean do
  def _get_ignore_patterns
    File.open(".git-clean-ignore", "r") do |f|
      return f.each_line.map(&:chomp)
    end
  end

  def _build_ignore_flags(patterns)
    patterns.map{ |p| "-e '#{p}'" }.join(' ')
  end

  def git_clean(dry_run = false)
    clean = "git clean"
    base_flags = "xdf"
    ignore_flags = _build_ignore_flags(_get_ignore_patterns)
    dry_flag = "n"

    flags = "#{base_flags}#{dry_run ? dry_flag : ''} #{ignore_flags}"

    sh "#{clean} -#{flags}"
  end

  task :clean do
    git_clean false
  end

  desc "Dry run of `clean:clean`"
  task :dry do
    git_clean true
  end
end

desc "Cleans untracked/git-ignored files, skipping patterns in `.git-clean-ignore`"
task :clean => 'clean:clean'

task :cl => :clean
task :cld => 'clean:dry'
