# KeePass-HaveIBeenPwned

A Windows .Net Core console app for checking passwords held in a KeePass file against those in [Have I Been Pwned](https://haveibeenpwned.com/)

## Table of contents
1. [Overview](#Overview)
2. [Usage](#Usage)
3. [Versions](#Versions)
    1. [KeePass Version](#.KeePassVersion)
    2. [HaveIBeenPwned Version](#.HaveIBeenPwnedVersion)
    3. [.Net Version](#.NetVersion)
4. [Performance](#Performance)
5. [Credits](#Credits)

## Overview <a name="Overview"></a>

Compares the passwords in an exported [KeePass](https://keepass.info/) file with those recorded by the ["Have I Been Pwned"](https://haveibeenpwned.com/) project. These are passwords that have been associated with [large data breaches](https://haveibeenpwned.com/PwnedWebsites).
<br>This app does not attempt to identify the breach, it just identifies matching passwords that are compromised and will likely be used in attempted bad logins.

## Usage <a name="Usage"></a>

* Download the file "Have I been Pwned" password file from https://haveibeenpwned.com/Passwords
<br> Note: You *MUST* use the SHA-1	(ordered by hash) file.
<br> i.e. **SHA-1**, *NOT* NTLM
<br>and 
<br>**(ordered by hash)**, *NOT* (ordered by prevalence)
<br>The torrent link for this file is currently [here](https://downloads.pwnedpasswords.com/passwords/pwned-passwords-sha1-ordered-by-hash-v5.7z.torrent), but this may change.

* Open KeePass, load your password file and export it:
<br> `File > Export > KeePass XML (2.x)`
<br> Ensure you export this file somewhere safe as it holds all your cleartext passwords! Ensure you delete it after completion.

* Run the KeePass-HaveIBeenPwned app with two parameters:
<br> `-h HaveIBeenPwnedPasswordList`
<br> `-k Exported KeePass file`
<br> e.g. to run the .Net Core dll from the VS2017 debug or release folder:
<br> `dotnet KeePass-HaveIBeenPwned.dll -h hibpFile -k keepassFile`

* Delete your KeePass file i.e. [*shift*][delete] in file explorer, not just [delete], as that would typically put the file in the recycle bin.

* If you want a standalone exe, rather than running dotnet against the dll, see
<br> [MS docs](https://docs.microsoft.com/en-us/dotnet/core/deploying/) and/or [StackOverflow](https://stackoverflow.com/questions/44074121/build-net-core-console-application-to-output-an-exe).

## Versions <a name="Versions"></a>

### KeePass version <a name="KeePassVersion"></a>
V2.35, from [KeePass](https://keepass.info/)

### HaveIBeenPwned version <a name="HaveIBeenPwnedVersion"></a>
Version 5, (ordered by hash), 14 Jul 2019m, 9.84GB.  
[Torrent](https://downloads.pwnedpasswords.com/passwords/pwned-passwords-sha1-ordered-by-hash-v5.7z.torrent)
<br>It is important that the "ordered by hash" file is used as the program assumes ordering and will not work correctly without it.
Essentially, the pre-ordering of the file saves us having to [sort](https://www.amazon.co.uk/Art-Computer-Programming-Sorting-Searching/dp/0201896850) a ~22GB text file with a little over half a billion lines.

### .Net version <a name=",NetVersion"></a>
.Net core 2.1 developed with VS2017

## Performance <a name="Performance"></a>
I have 667 entries in my KeePass file (yes, I know, I've been using it for a few years) although this number should not significantly affect the run time - the main issue is reading ~550M lines from the HIBP file.
<br>The app ran in just over 4 minutes on my i7 laptop with SSDs.


## Credits <a name="Credits"></a>

Command Line Parser Library for CLR and NetStandard
https://github.com/commandlineparser/commandline


