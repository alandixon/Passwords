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

        private DateTime startTime;
        private DateTime endTime;


        public Checker(Options options)
        {
            startTime = DateTime.Now;

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
            List<Credential> SortedCredentials = credentials.OrderBy(c => c.PasswordSHA1).ToList();

            // Now we need to think how we're going to step through both files comparing

            // Keep a record of which credential we are checking
            int credentialIndex = 0;

            // Keep a count of matches
            int matchesFound = 0;

            HIBPLines hibpLines = new HIBPLines(options.HIBPFile);
            int hibpCount = 0;

            // We jump back here whenever hibp hash < kp hash
            NewHIBPline:
            foreach (string hibpLine in hibpLines)
            {
                try
                {
                    hibpCount++;
                    string[] hibpLineFragments = hibpLine.Split(':');
                    string hibpHash = hibpLineFragments[0];

                    try
                    {
                        Credential currentCredential;
                        while (TryGetCredentialAtIndex(SortedCredentials, credentialIndex, out currentCredential))
                        {
                            int compare = String.Compare(hibpHash, currentCredential.PasswordSHA1);
                            switch (compare)
                            {
                                case 0:
                                    // currentCredential matches line in HIBP list
                                    Console.WriteLine("Match found: {0}    Hash:{1}", currentCredential.Path, currentCredential.PasswordSHA1);
                                    matchesFound++;
                                    // Then get the next KeePass credential in case that also matches this hibp hash
                                    break;
                                case 1:
                                    // currentCredential not matched in HIBP list, so get next KeePass credential
                                    break;
                                case -1:
                                    // keep iterating through sorted HIBP for line matching currentCredential
                                    goto NewHIBPline;
                            }
                            credentialIndex++;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error comparing hashes: KeePass[{0}] HIBP[{1}], {2}", hibpCount, options.HIBPFile, ex.Message);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error reading HIBP file: Line {0} of {1} {2}", hibpCount, options.HIBPFile, ex.Message);
                }
            }
            Console.WriteLine("Compared {0} HIBP file lines with {1} KeePass credentials and found {2} matches", hibpCount, SortedCredentials.Count, matchesFound);
            endTime = DateTime.Now;
            TimeSpan elapsedTime = endTime - startTime;
            Console.WriteLine("Run time: {0} - [d.]hh:mm:ss.fffffff", elapsedTime);
        }


        /// <summary> Get the credential at the specified index from the sorted credental list</summary>
        /// <param name="SortedCredentials"></param>
        /// <param name="credentialIndex"></param>
        /// <param name="credential"></param>
        /// <returns>True if successful</returns>
        private bool TryGetCredentialAtIndex(List<Credential> SortedCredentials, int credentialIndex, out Credential credential)
        {
            if (credentialIndex < SortedCredentials.Count)
            {
                credential = SortedCredentials[credentialIndex];
                return true;
            }
            credential = null;
            return false;
        }

        /// <summary>Recursive fetch of all credentials from the KeePass groups </summary>
        /// <param name="groups"></param>
        /// <param name="groupNameString"></param>
        /// <param name="credentials">Found credentials</param>
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
                        if (stringPair.Key == "Password" && stringPair.Value.Text != null)
                        {
                            // Get the password
                            credential.Password = stringPair.Value.Text;
                            // Get the password hash (uppercase)
                            byte[] hashBytes = sha1.ComputeHash(Encoding.UTF8.GetBytes(credential.Password));
                            credential.PasswordSHA1 = string.Concat(hashBytes.Select(b => b.ToString("X2")));
                            // Get the group path
                            credential.Path = subGroupNameString.ToString();
                        }
                        if (stringPair.Key == "Title")
                        {
                            credential.Title = stringPair.Value?.Text ?? String.Empty;
                            credential.Path = credential.Path + " - " + credential.Title;
                        }
                        if (stringPair.Key == "UserName")
                        {
                            credential.UserName = stringPair.Value?.Text ?? String.Empty;
                            credential.Path = credential.Path + " - " + credential.UserName;
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


    /// <summary> HIBPLines provides an enumerable set of the lines from the file </summary>
    public class HIBPLines
    {
        private StreamReader fileReader;
        private string fileName;

        public HIBPLines(string fileName)
        {
            this.fileName = fileName;
            fileReader = new StreamReader(fileName);
        }

        public IEnumerator<string> GetEnumerator()
        {
            string line;
            while ((line = fileReader.ReadLine()) != null)
            {
                yield return line;
            }
            fileReader.Close();
            yield break;
        }
    }

}
