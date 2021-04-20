using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using Newtonsoft.Json;


namespace Blep
{
    public static class BackupManager
    {
        public static string BackupFolderPath => Path.Combine(BlepOut.BOIpath, "Backups");
        public static void LoadBackupList()
        {
            bool flag = BlepOut.IsMyPathCorrect;
            if (!Directory.Exists(BackupFolderPath)) Directory.CreateDirectory(BackupFolderPath);
            if (flag)
            {
                ActiveSave = new UserDataStateRelay(UserDataFolder) { CreationTime = DateTime.Now } ;
            }
            AllBackups.Clear();
            foreach (DirectoryInfo dir in new DirectoryInfo(BackupFolderPath).GetDirectories())
            {
                
                AllBackups.Add(new UserDataStateRelay(dir) { } );
            }
            
        }
        public static void StashActiveSave()
        {
            if (!BlepOut.IsMyPathCorrect) return;
            UserDataStateRelay udsr = ActiveSave ?? new UserDataStateRelay(UserDataFolder);
            AllBackups.Add( udsr.CloneTo(PathForNewBackup));
        }
        public static void DeleteSave(UserDataStateRelay toDelete)
        {
            try
            {
                Directory.Delete(toDelete.Location, true);
                if (toDelete.Location == ActiveSave?.Location) Directory.CreateDirectory(toDelete.Location);
                if (AllBackups.Contains(toDelete)) AllBackups.Remove(toDelete);
                else if (ActiveSave == toDelete) ActiveSave = null;
            }
            catch (IOException ioe)
            {
                Wood.WriteLine($"ERROR DELETING SAVE {toDelete.MyName}:");
                Wood.Indent();
                Wood.WriteLine(ioe);
                Wood.Unindent();
            }
            
        }
        public static bool RestoreActiveSaveFromBackup(UserDataStateRelay backup)
        {
            if (backup.Location == ActiveSave?.Location) return false;
            if (ActiveSave?.CurrState != UserDataStateRelay.StateState.Empty) { return false; };
            
            return false;
        }
        public static bool RestoreActiveSaveFromBackup(int index)
        {
            try
            {
                return RestoreActiveSaveFromBackup(AllBackups[index]);
            }
            catch (IndexOutOfRangeException)
            {
                return false;
            }
        }
        public static void SaveSettingsForAll()
        {
            ActiveSave?.RecordData();
            foreach (UserDataStateRelay udsr in AllBackups) udsr.RecordData();
        }

        public static string PathForNewBackup => Path.Combine(BackupFolderPath, $"{DateTime.Now.Ticks}");
        public static string UserDataFolder => Path.Combine(BlepOut.RootPath, "UserData");
        public static UserDataStateRelay ActiveSave { get; set; }
        public static List<UserDataStateRelay> AllBackups { get { if (_abu == null) _abu = new List<UserDataStateRelay>(); return _abu; } set { _abu = value; } }
        private static List<UserDataStateRelay> _abu;


        public class UserDataStateRelay
        {
            public UserDataStateRelay(string path)
            {
                Location = path;
                Locdir = new DirectoryInfo(path);
                ReadData();
                
            }
            public UserDataStateRelay(DirectoryInfo dir)
            {
                Locdir = dir;
                ReadData();
                if (Data.Name == string.Empty && !IsActiveSave) Data.Name = Data.CreationTime.ToString();
            }
            public bool TryMoveToNewLocation(string newpath)
            {

                return false;
            }

            public string DateTimeString => (!IsActiveSave) ? Data.CreationTime.ToString() : "N/A";
            public string UserDefinedName { get { return Data.Name; } set { Data.Name = value; } }
            public string MyName => (IsActiveSave) ? "Current save" : UserDefinedName;
            public string UserNotes { get => Data.Notes; set { Data.Notes = value; } }
            public DateTime CreationTime { get { return Data.CreationTime; } set { Data.CreationTime = value; } }

            internal bool ReadData()
            {
                try
                {
                    Data = JsonConvert.DeserializeObject<AttachedData>(File.ReadAllText(DataJsonPath));
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            internal bool RecordData()
            {
                if (CurrState == StateState.Invalid) return false;
                try
                {
                    string outjson = JsonConvert.SerializeObject(Data, Formatting.Indented);
                    File.WriteAllText(DataJsonPath, outjson);
                    return true;
                }
                catch (IOException ioe)
                {
                    Wood.WriteLine($"Error saving backup data for savefile backup {MyName}");
                    Wood.Indent();
                    Wood.Write(ioe);
                    Wood.Unindent();
                }
                return false;
            }


            public string Location { get { return Locdir?.FullName ?? string.Empty; } set { Locdir = new DirectoryInfo(value); } }
            private string DataJsonPath => Path.Combine(Location, "UDBACKUPDATA.json");
            public DirectoryInfo Locdir { get; set; }
            public bool IsActiveSave => Location == BackupManager.ActiveSave.Location;

            public StateState CurrState
            {
                get
                {
                    if (!Locdir.Exists) return StateState.Invalid;
                    if (Locdir.GetFiles().Length > 1) return StateState.Normal;
                    return StateState.Empty;
                }
            }            
            public enum StateState
            {
                Invalid,
                Empty,
                Normal
            }

            public UserDataStateRelay CloneTo(string to)
            {

                int terrc = BoiCustom.BOIC_RecursiveDirectoryCopy(Location, to);
                Wood.WriteLine((terrc == 0) ? $"Savefolder state successfully copied to {to}" : $"Attempt to copy a savefolder from {Location} to {to} complete; total of {terrc} errors encountered.");
                UserDataStateRelay Nudsr = new UserDataStateRelay(to);
                Nudsr.CreationTime = DateTime.Now;
                Nudsr.UserDefinedName = this.UserDefinedName;
                Nudsr.UserNotes = this.UserNotes;
                return Nudsr;
            }
            public override string ToString()
            {
                return $"{MyName} : {CurrState}";
            }

            private AttachedData Data;
            public struct AttachedData
            {
                public string Notes { get { return _notes ?? string.Empty; } set { _notes = value; } }
                private string _notes;
                public string Name { get { return _name ?? string.Empty; } set { _name = value; } }
                private string _name;

                public DateTime CreationTime { get => _ct; set { _ct = value; } }
                private DateTime _ct;
            }

        }
    }
}
