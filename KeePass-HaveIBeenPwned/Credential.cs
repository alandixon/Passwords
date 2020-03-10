using System.Diagnostics;

namespace KeePass_HaveIBeenPwned
{
    /// <summary> Represents one credential set of user/pass, along with the groupname path hierarchy that holds it </summary>
    [DebuggerDisplay("{Path} - {UserName} -{PasswordSHA1}")]
    public class Credential
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string PasswordSHA1 { get; set; }

        public string Path { get; set; }
    }
}
