using CommandLine;
using System;
using System.Collections.Generic;
using System.Text;

namespace KeePass_HaveIBeenPwned
{
    public class Options
    {
        [Option('h', "HIBPFile", Required = true, HelpText = "Have I Been Pwned hash file, ordered by hash")]
        public string HIBPFile { get; set; }

        [Option('k', "KeePassFile", Required = true, HelpText = "Exported KeePassXml file")]
        public string KeePassFile { get; set; }

        //[Option('o', "OutFile", Required = false, HelpText = "Send output here rather than stdout.")]
        //public string OutFile { get; set; }
    }
}
