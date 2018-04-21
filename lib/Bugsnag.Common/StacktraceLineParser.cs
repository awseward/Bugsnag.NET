using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Bugsnag.Common
{
    public class StacktraceLineParser
    {
        Regex _fileRegex = new Regex("\\) [^ ]+ (.+):");
        public string ParseFile(string line)
        {
            var match = _fileRegex.Match(line);

            if (match.Groups.Count < 2) { return ""; }

            return match.Groups[1].Value;
        }

        Regex _methodNameRegex = new Regex("^ *[^ ]+ (.+\\))");
        public string ParseMethodName(string line)
        {
            var match = _methodNameRegex.Match(line);

            if (match.Groups.Count < 2) { return line; }

            return match.Groups[1].Value;
        }

        Regex _lineNumberRegex = new Regex(":.+ ([0-9]+)$");
        public int? ParseLineNumber(string line)
        {
            var match = _lineNumberRegex.Match(line);

            if (match.Groups.Count < 2) { return null; }

            if (int.TryParse(match.Groups[1].Value, out var lineNumber))
            {
                return lineNumber;
            }
            else
            {
                return null;
            }
        }
    }
}
