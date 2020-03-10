using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace KeePass_HaveIBeenPwned
{
    public class Checker
    {

        private KeePassFile keePassFile;

        private List<Credential> credentials;


        public Checker(Options options)
        {
            // Check required files
            if (!File.Exists(options.HIBPFile))
            {
                throw new FileNotFoundException(string.Format("Can't find [Have I been Pwned hash file]: {0}", options.HIBPFile));
            }
            if (!File.Exists(options.KeePassFile))
            {
                throw new FileNotFoundException(string.Format("Can't find [KeePass file]: {0}", options.KeePassFile));
            }

            keePassFile = KeePassFile.ReadFromXmlFile(options.KeePassFile);

            credentials = new List<Credential>();
            StringBuilder groupNameString = new StringBuilder();
            GetCredentials(keePassFile.Root.Groups, groupNameString, credentials);
            Console.WriteLine("Found {0} passwords in {1}", credentials.Count, options.KeePassFile);

            // Sort credentials by the SHA1 hash 
            Console.WriteLine("Sorting passwords by SHA-1 hash");

            // Now we need to think how we're going to step through both files comparing

        }


        private void GetCredentials(List<Group> groups, StringBuilder groupNameString, List<Credential> credentials)
        {
            SHA1Managed sha1 = new SHA1Managed();

            foreach (Group group in groups)
            {
                // New string builder
                StringBuilder subGroupNameString = new StringBuilder(groupNameString.ToString());
                if (groupNameString.Length > 0) subGroupNameString.Append(" - ");
                subGroupNameString.Append(group.Name);

                foreach (Entry entry in group.Entries)
                {
                    Credential credential = new Credential();
                    foreach (StringPair stringPair in entry.StringPairs)
                    {
                        if (stringPair.Key == "Password")
                        {
                            // Get the password
                            credential.Password = stringPair.Value.Text;
                            // Get the password hash (uppercase)
                            byte[] hashBytes = sha1.ComputeHash(Encoding.UTF8.GetBytes(credential.Password));
                            credential.PasswordSHA1 = string.Concat(hashBytes.Select(b => b.ToString("X2")));
                            // Get the group path
                            credential.Path = subGroupNameString.ToString();
                        }
                        if (stringPair.Key == "UserName")
                        {
                            credential.UserName = stringPair.Value.Text;
                        }
                    }
                    // If we found a password (even w/o a username), save it
                    if (!string.IsNullOrWhiteSpace(credential.Password))
                    {
                        credentials.Add(credential);
                    }
                }

                if (group.Groups != null)
                {
                    GetCredentials(group.Groups, subGroupNameString, credentials);
                }
            }
        }

    }


}
