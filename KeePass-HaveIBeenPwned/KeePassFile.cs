using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;

// Derived from https://xmltocsharp.azurewebsites.net/

namespace KeePass_HaveIBeenPwned
{

    [XmlRoot(ElementName = "KeePassFile")]
    public class KeePassFile
    {
        private static XmlSerializer serializer = new XmlSerializer(typeof(KeePassFile));

        [XmlElement(ElementName = "Meta")]
        public Meta Meta { get; set; }
        [XmlElement(ElementName = "Root")]
        public Root Root { get; set; }

        public static KeePassFile ReadFromXmlFile(string fileName)
        {
            using (Stream reader = new FileStream(fileName, FileMode.Open))
            {
                return (KeePassFile)serializer.Deserialize(reader);
            }
        }
    }

    public class MemoryProtection
    {
        [XmlElement(ElementName = "ProtectTitle")]
        public string ProtectTitle { get; set; }
        [XmlElement(ElementName = "ProtectUserName")]
        public string ProtectUserName { get; set; }
        [XmlElement(ElementName = "ProtectPassword")]
        public string ProtectPassword { get; set; }
        [XmlElement(ElementName = "ProtectURL")]
        public string ProtectURL { get; set; }
        [XmlElement(ElementName = "ProtectNotes")]
        public string ProtectNotes { get; set; }
    }

    [XmlRoot(ElementName = "Meta")]
    public class Meta
    {
        [XmlElement(ElementName = "Generator")]
        public string Generator { get; set; }
        [XmlElement(ElementName = "DatabaseName")]
        public string DatabaseName { get; set; }
        [XmlElement(ElementName = "DatabaseNameChanged")]
        public string DatabaseNameChanged { get; set; }
        [XmlElement(ElementName = "DatabaseDescription")]
        public string DatabaseDescription { get; set; }
        [XmlElement(ElementName = "DatabaseDescriptionChanged")]
        public string DatabaseDescriptionChanged { get; set; }
        [XmlElement(ElementName = "DefaultUserName")]
        public string DefaultUserName { get; set; }
        [XmlElement(ElementName = "DefaultUserNameChanged")]
        public string DefaultUserNameChanged { get; set; }
        [XmlElement(ElementName = "MaintenanceHistoryDays")]
        public string MaintenanceHistoryDays { get; set; }
        [XmlElement(ElementName = "Color")]
        public string Color { get; set; }
        [XmlElement(ElementName = "MasterKeyChanged")]
        public string MasterKeyChanged { get; set; }
        [XmlElement(ElementName = "MasterKeyChangeRec")]
        public string MasterKeyChangeRec { get; set; }
        [XmlElement(ElementName = "MasterKeyChangeForce")]
        public string MasterKeyChangeForce { get; set; }
        [XmlElement(ElementName = "MemoryProtection")]
        public MemoryProtection MemoryProtection { get; set; }
        [XmlElement(ElementName = "RecycleBinEnabled")]
        public string RecycleBinEnabled { get; set; }
        [XmlElement(ElementName = "RecycleBinUUID")]
        public string RecycleBinUUID { get; set; }
        [XmlElement(ElementName = "RecycleBinChanged")]
        public string RecycleBinChanged { get; set; }
        [XmlElement(ElementName = "EntryTemplatesGroup")]
        public string EntryTemplatesGroup { get; set; }
        [XmlElement(ElementName = "EntryTemplatesGroupChanged")]
        public string EntryTemplatesGroupChanged { get; set; }
        [XmlElement(ElementName = "HistoryMaxItems")]
        public string HistoryMaxItems { get; set; }
        [XmlElement(ElementName = "HistoryMaxSize")]
        public string HistoryMaxSize { get; set; }
        [XmlElement(ElementName = "LastSelectedGroup")]
        public string LastSelectedGroup { get; set; }
        [XmlElement(ElementName = "LastTopVisibleGroup")]
        public string LastTopVisibleGroup { get; set; }
        [XmlElement(ElementName = "Binaries")]
        public string Binaries { get; set; }
        [XmlElement(ElementName = "CustomData")]
        public string CustomData { get; set; }
    }

    [XmlRoot(ElementName = "Times")]
    public class Times
    {
        [XmlElement(ElementName = "CreationTime")]
        public string CreationTime { get; set; }
        [XmlElement(ElementName = "LastModificationTime")]
        public string LastModificationTime { get; set; }
        [XmlElement(ElementName = "LastAccessTime")]
        public string LastAccessTime { get; set; }
        [XmlElement(ElementName = "ExpiryTime")]
        public string ExpiryTime { get; set; }
        [XmlElement(ElementName = "Expires")]
        public string Expires { get; set; }
        [XmlElement(ElementName = "UsageCount")]
        public string UsageCount { get; set; }
        [XmlElement(ElementName = "LocationChanged")]
        public string LocationChanged { get; set; }
    }

    [DebuggerDisplay("{Key} : {Value.Text}")]
    [XmlRoot(ElementName = "String")]
    public class StringPair
    {
        [XmlElement(ElementName = "Key")]
        public string Key { get; set; }
        [XmlElement(ElementName = "Value")]
        public Value Value { get; set; }
    }

    [XmlRoot(ElementName = "Value")]
    public class Value
    {
        [XmlAttribute(AttributeName = "ProtectInMemory")]
        public string ProtectInMemory { get; set; }
        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "Association")]
    public class Association
    {
        [XmlElement(ElementName = "Window")]
        public string Window { get; set; }
        [XmlElement(ElementName = "KeystrokeSequence")]
        public string KeystrokeSequence { get; set; }
    }

    [XmlRoot(ElementName = "AutoType")]
    public class AutoType
    {
        [XmlElement(ElementName = "Enabled")]
        public string Enabled { get; set; }
        [XmlElement(ElementName = "DataTransferObfuscation")]
        public string DataTransferObfuscation { get; set; }
        [XmlElement(ElementName = "Association")]
        public Association Association { get; set; }
    }

    [DebuggerDisplay("Entry: {Strings.Count} strings")]
    [XmlRoot(ElementName = "Entry")]
    public class Entry
    {
        [XmlElement(ElementName = "UUID")]
        public string UUID { get; set; }
        [XmlElement(ElementName = "IconID")]
        public string IconID { get; set; }
        [XmlElement(ElementName = "ForegroundColor")]
        public string ForegroundColor { get; set; }
        [XmlElement(ElementName = "BackgroundColor")]
        public string BackgroundColor { get; set; }
        [XmlElement(ElementName = "OverrideURL")]
        public string OverrideURL { get; set; }
        [XmlElement(ElementName = "Tags")]
        public string Tags { get; set; }
        [XmlElement(ElementName = "Times")]
        public Times Times { get; set; }
        [XmlElement(ElementName = "String")]
        public List<StringPair> StringPairs { get; set; }
        [XmlElement(ElementName = "AutoType")]
        public AutoType AutoType { get; set; }
        [XmlElement(ElementName = "History")]
        public History History { get; set; }
    }

    [DebuggerDisplay("{Name}")]
    [XmlRoot(ElementName = "Group")]
    public class Group
    {
        [XmlElement(ElementName = "UUID")]
        public string UUID { get; set; }
        [XmlElement(ElementName = "Name")]
        public string Name { get; set; }
        [XmlElement(ElementName = "Notes")]
        public string Notes { get; set; }
        [XmlElement(ElementName = "IconID")]
        public string IconID { get; set; }
        [XmlElement(ElementName = "Times")]
        public Times Times { get; set; }
        [XmlElement(ElementName = "IsExpanded")]
        public string IsExpanded { get; set; }
        [XmlElement(ElementName = "DefaultAutoTypeSequence")]
        public string DefaultAutoTypeSequence { get; set; }
        [XmlElement(ElementName = "EnableAutoType")]
        public string EnableAutoType { get; set; }
        [XmlElement(ElementName = "EnableSearching")]
        public string EnableSearching { get; set; }
        [XmlElement(ElementName = "LastTopVisibleEntry")]
        public string LastTopVisibleEntry { get; set; }
        [XmlElement(ElementName = "Entry")]
        public List<Entry> Entries { get; set; }
        [XmlElement(ElementName = "Group")]
        public List<Group> Groups { get; set; }
    }

    [XmlRoot(ElementName = "History")]
    public class History
    {
        [XmlElement(ElementName = "Entry")]
        public List<Entry> Entries { get; set; }
    }

    [XmlRoot(ElementName = "Root")]
    public class Root
    {
        [XmlElement(ElementName = "Group")]
        public List<Group> Groups { get; set; }
        [XmlElement(ElementName = "DeletedObjects")]
        public string DeletedObjects { get; set; }
    }
}
