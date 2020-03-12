# KeePass-HaveIBeenPwned

A Windows .Net console app for checking the passwords in KeePass against those in [Have I Been Pwned](https://haveibeenpwned.com/)

## Table of contents
1. [Overview](#Overview)
2. [Versions](#Versions)
    1. [KeePass Version](#.KeePassVersion)
    2. [HaveIBeenPwned Version](#.HaveIBeenPwnedVersion)
    3. [.Net Version](#.NetVersion)
3. [Performance](#Performance)
4. [Credits](#Credits)



## Overview <a name="Overview"></a>

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
I have 667 entries in my KeePass file (yes, I know, I've been using it for a few years) although this number should not significantly affect the run time - the main issue is reading about 550M lines from the HIBP file. The app ran in just over 4 minutes.


## Credits <a name="Credits"></a>

Command Line Parser Library for CLR and NetStandard
https://github.com/commandlineparser/commandline


