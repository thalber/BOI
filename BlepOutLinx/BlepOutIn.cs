using Mono.Cecil;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;


namespace BlepOutLinx
{
    public partial class BlepOut : Form
    {
        public BlepOut()
        {
            InitializeComponent();
            targetFiles = new List<ModRelay>();
            pluginBlacklist = new List<string>();
            patchBlacklist = new List<string>();
            RetrieveConfig();
            UpdateTargetPath(RootPath);
        }

        public void UpdateTargetPath(string path)
        {
            btnLaunch.Enabled = false;
            Modlist.Enabled = false;
            RootPath = path;
            targetFiles.Clear();
            pluginBlacklist.Clear();
            patchBlacklist.Clear();
            if (IsMyPathCorrect) Setup();
            SaveConfig();
            StatusUpdate();
            Process[] searchres = Process.GetProcessesByName("Rain World");
            foreach (Process pr in searchres)
            {
                Console.WriteLine(pr.Id);
            }
        }

        private void RetrieveConfig()
        {
            if (File.Exists(cfgpath))
            {
                string[] cfgstrings = File.ReadAllLines(cfgpath);
                try
                {
                    RootPath = cfgstrings[0];
                }
                catch
                {
                    CreateConfig();
                    RootPath = string.Empty;
                }
            }
            else
            {
                CreateConfig();
                RootPath = string.Empty;
            }
        }

        private void CreateConfig()
        {
            StreamWriter sw = File.CreateText(cfgpath);
            sw.WriteLine(string.Empty);
            sw.Close();
        }
        private void SaveConfig()
        {
            StreamWriter sw = File.CreateText(cfgpath);
            sw.WriteLine(RootPath);
            sw.Close();
        }

        private void Setup()
        {
            PubstuntFound = false;
            
            ReadyForRefresh = false;
            Modlist.Items.Clear();
            
            targetFiles.Clear();
            Rootout();
            PrepareModsFolder();
            ResolveBlacklists();
            RetrieveAllDlls();
            CompileModList();
            RefreshList();
            Modlist.Enabled = true;
            btnLaunch.Enabled = true;
            TargetSelect.SelectedPath = RootPath;
            if (PubstuntFound)
            {
                this.popup = new BlepOutIn.PubstuntInfoPopup();
                this.AddOwnedForm(popup);
                popup.Show();
            }
            ReadyForRefresh = true;
        }

        private void ResolveBlacklists()
        {
            RetrieveHkBlacklist();
            RetrievePtBlacklist();
        }

        private void Rootout()
        {
            string[] patchfoldercontents = Directory.GetFiles(PatchesFolder);
            string[] modfoldercontents = Directory.GetFiles(ModFolder);
            foreach (string s in patchfoldercontents)
            {
                if (s.Contains("PublicityStunt") && s.EndsWith(".dll"))
                {
                    File.Delete(s);
                    PubstuntFound = true;
                }
            }
            foreach (string s in modfoldercontents)
            {
                if (s.Contains("PublicityStunt") && s.EndsWith(".dll"))
                {
                    File.Delete(s);
                    PubstuntFound = true;
                }
            }
        }
        private void PrepareModsFolder()
        {
            if (!Directory.Exists(ModFolder))
            {
                Directory.CreateDirectory(ModFolder);
            }
        }
        private void RetrieveHkBlacklist()
        {
            if (File.Exists(hkblacklistpath))
            {
                string[] sl = File.ReadAllLines(hkblacklistpath);
                foreach (string s in sl)
                {
                    pluginBlacklist.Add(s);
                }
            }
            if (pluginBlacklist.Count == 0)
            {
                CreateHkBlacklist();
                pluginBlacklist.Add("LogFix.dll");
            }
        }
        private void CreateHkBlacklist()
        {
            StreamWriter sw = File.CreateText(hkblacklistpath);
            sw.WriteLine("LogFix.dll");
            sw.Close();
        }
        private void RetrievePtBlacklist()
        {
            if (File.Exists(ptblacklistpath))
            {
                string[] pblcts = File.ReadAllLines(ptblacklistpath);
                foreach (string s in pblcts)
                {
                    patchBlacklist.Add(s);
                }
            }
            if (patchBlacklist.Count == 0)
            {
                CreatePtBlacklist();
                patchBlacklist.Add("Assembly-CSharp.PatchNothing.mm.dll");
            }
        }
        private void CreatePtBlacklist()
        {
            StreamWriter sw = File.CreateText(ptblacklistpath);
            sw.WriteLine("Assembly-CSharp.PatchNothing.mm.dll");
            sw.Close();
        }
        private void RetrieveAllDlls()
        {
            string[] pluginFolderContents = Directory.GetFiles(PluginsFolder);
            foreach (string s in pluginFolderContents)
            {
                var fi = new FileInfo(s);
                if (fi.Extension == ".dll" && !pluginBlacklist.Contains(fi.Name) && !fi.Attributes.HasFlag(FileAttributes.ReparsePoint))
                {
                    if (File.Exists(pathtomods(fi)))
                    {
                        try
                        {
                            if (fi.CreationTime > new FileInfo(pathtomods(fi)).CreationTime)
                            {
                                File.Delete(pathtomods(fi));
                                File.Copy(s, pathtomods(fi));
                            }
                            if (fi.CreationTime < new FileInfo(pathtomods(fi)).CreationTime)
                            {
                                File.Delete(s);
                                File.Copy(pathtomods(fi), s);
                            }
                        }
                        catch
                        {
                            //Console.WriteLine("Failed ")
                        }

                    }
                    if (!File.Exists(pathtomods(fi))) File.Copy(s, pathtomods(fi));
                }

            }

            string[] patchFolderContents = Directory.GetFiles(PatchesFolder);
            foreach (string s in patchFolderContents)
            {
                var fi = new FileInfo(s);
                if (fi.Extension == ".dll" && !patchBlacklist.Contains(fi.Name) && !fi.Attributes.HasFlag(FileAttributes.ReparsePoint) && !File.Exists(ModFolder + PtModData.GiveMeBackMyName(fi.Name)))
                {
                    File.Copy(s, ModFolder + PtModData.GiveMeBackMyName(fi.Name));
                }
            }



            string pathtomods(FileInfo fi)
            {
                return ModFolder + fi.Name;
            }
        }
        private void CompileModList()
        {
            string[] ModsFolderContents = Directory.GetFiles(ModFolder);
            foreach (string s in ModsFolderContents)
            {
                var fi = new FileInfo(s);
                if (fi.Extension == ".dll" && !fi.Attributes.HasFlag(FileAttributes.ReparsePoint))
                {
                    targetFiles.Add(new ModRelay(s));
                }

            }
            foreach (ModRelay mr in targetFiles)
            {
                Modlist.Items.Add(mr);

            }
            for (int i = 0; i < Modlist.Items.Count; i++)
            {
                if (Modlist.Items[i] is ModRelay)
                {
                    ModRelay mr = Modlist.Items[i] as ModRelay;
                    if (mr.enabled) Modlist.SetItemChecked(i, true);
                }
            }
        }
        private void RefreshList()
        {
            for (int i = 0; i < Modlist.Items.Count; i++)
            {
                if (Modlist.Items[i] is ModRelay)
                {
                    ModRelay mr = Modlist.Items[i] as ModRelay;
                    if (Modlist.GetItemChecked(i))
                    {
                        mr.Enable();
                    }
                    else mr.Disable();
                }
            }
        }
        
        private bool IsMyPathCorrect
        {
            get { return (Directory.Exists(PluginsFolder) && Directory.Exists(PatchesFolder)); }
        }

        private static string RootPath;
        private bool changetracker;
        private bool TSbtnMode = true;

        private string BOIpath
        {
            get { return Assembly.GetExecutingAssembly().Location.Replace("BlepOutIn.exe", string.Empty); }
        }

        private string cfgpath
        {
            get { return BOIpath + @"cfg.txt"; }
        }

        private List<string> patchBlacklist;
        private List<string> pluginBlacklist;
        private List<ModRelay> targetFiles;
        private Process rw;
        private bool ReadyForRefresh;
        private bool PubstuntFound;
        private BlepOutIn.PubstuntInfoPopup popup;
        

        //List<TargetFileData> IgnoredFailed;
        private string hkblacklistpath
        {
            get { return PluginsFolder + @"\plugins_blacklist.txt"; }
        }

        private string ptblacklistpath
        {
            get { return PatchesFolder + @"\patches_blacklist.txt"; }
        }

        private string ModFolder
        {
            get { return RootPath + @"\Mods\"; }
        }

        private string PluginsFolder
        {
            get { return RootPath + @"\BepInEx\plugins\"; }
        }

        private string PatchesFolder
        {
            get { return RootPath + @"\BepInEx\monomod\"; }
        }

        private class ModRelay
        {
            public ModRelay(string path)
            {
                ModPath = path;
                this.isValid = !ModData.AbsolutelyIgnore(ModPath);
                if (isValid)
                {
                    bool ishk = false;
                    bool ispt = false;
                    using (ModuleDefinition md = ModuleDefinition.ReadModule(path))
                    {
                        foreach (TypeDefinition t in md.Types)
                        {
                            if (t.BaseType != null && t.BaseType.FullName.Contains("PartialityMod")) ishk = true;
                            if (t.HasCustomAttributes)
                            {
                                foreach (CustomAttribute catr in t.CustomAttributes)
                                {
                                    if (catr.AttributeType.Namespace == "MonoMod") ispt = true;
                                }
                            }
                        }
                        if (ishk && ispt) this.AssociatedModData = new MixedModData(ModPath);
                        else if (ishk && !ispt) this.AssociatedModData = new HkModData(ModPath);
                        else if (ispt && !ishk) this.AssociatedModData = new PtModData(ModPath);
                        else this.AssociatedModData = new ModData(ModPath);
                    }
                    //Type[] tps = asm.GetTypes();

                }
            }

            private string ModPath;
            private ModData AssociatedModData;
            public bool isValid;
            public bool enabled
            {
                get { return AssociatedModData.Enabled; }
            }

            public void Enable()
            {
                if (AssociatedModData is MixedModData) return;
                if (enabled) return;
                File.Copy(AssociatedModData.OrigPath, AssociatedModData.TarFolder + AssociatedModData.TarName);
            }

            public void Disable()
            {
                if (AssociatedModData is MixedModData) return;
                if (!enabled) return;
                File.Delete(AssociatedModData.TarFolder + AssociatedModData.TarName);
            }

            public override string ToString()
            {
                return AssociatedModData.ToString();
            }
        }

        private class ModData
        {
            public ModData(string path)
            {
                this.OrigPath = path;

            }
            public virtual string TarName
            {
                get { return DisplayedName; }
            }

            public virtual string TarFolder
            {
                get { return RootPath + @"\BepInEx\Plugins\"; }
            }
            public string OrigPath;

            public virtual bool Enabled
            {
                get { return (File.Exists(TarFolder + this.TarName)); }
            }
            public virtual string DisplayedName
            {
                get { return new FileInfo(OrigPath).Name; }
            }
            public static bool AbsolutelyIgnore(string tpath)
            {
                return (new FileInfo(tpath).Extension != @".dll" || new FileInfo(tpath).Attributes.HasFlag(FileAttributes.ReparsePoint));
            }
            public override string ToString()
            {
                return DisplayedName + " : UNKNOWN";
            }
        }

        private class HkModData : ModData
        {
            public HkModData(string path) : base(path)
            {

            }

            public override string ToString()
            {
                return DisplayedName + " : HOOK";
            }
        }

        private class PtModData : ModData
        {
            public PtModData(string path) : base(path)
            {

            }

            public override string TarName => "Assembly-CSharp." + DisplayedName.Replace(".dll", string.Empty) + ".mm.dll";

            public override string TarFolder => RootPath + @"\BepInEx\Monomod\";

            public static string GiveMeBackMyName(string partname)
            {
                string sl = partname.Replace("Assembly-CSharp.", string.Empty);
                sl = sl.Replace(".mm.dll", ".dll");
                return sl;
            }

            //public override bool Enabled => File.Exists(this.TarFolder + this.TarName);

            public override string ToString()
            {
                return DisplayedName + " : PATCH";
            }
        }

        private class MixedModData : ModData
        {
            public MixedModData(string path) : base(path)
            {

            }

            public override bool Enabled => false;

            public override string TarName => null;


        }

        private void buttonSelectPath_Click(object sender, EventArgs e)
        {

            if (TSbtnMode)
            {
                TargetSelect.ShowDialog();
                btnSelectPath.Text = "Press again to load modlist";
                TSbtnMode = false;
                btnLaunch.Enabled = false;

            }
            else
            {
                UpdateTargetPath(TargetSelect.SelectedPath);
                btnSelectPath.Text = "Select path";
                TSbtnMode = true;
            }
        }

        private void checklistModlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshList();
        }

        private void BlepOut_Activated(object sender, EventArgs e)
        {
            fsw_modsfolder.EnableRaisingEvents = false;
			fsw_pluginsfolder.EnableRaisingEvents = false;
			if (changetracker && ReadyForRefresh)
			{
				UpdateTargetPath(RootPath);
			}
            StatusUpdate();
        }

        private void BlepOut_Deactivate(object sender, EventArgs e)
        {
            if (IsMyPathCorrect)
            {
                changetracker = false;
				fsw_modsfolder.Path = ModFolder;
				fsw_pluginsfolder.Path = PluginsFolder;
				fsw_modsfolder.EnableRaisingEvents = true;
				fsw_pluginsfolder.EnableRaisingEvents = true;
            }
        }

        public void SomethingChanged()
        {
            changetracker = true;
        }

        private void StatusUpdate()
        {
            lblPathStatus.Text = IsMyPathCorrect ? "Path valid" : "Path invalid";
            lblPathStatus.BackColor = IsMyPathCorrect ? System.Drawing.Color.FromKnownColor(System.Drawing.KnownColor.LightGreen) : System.Drawing.Color.FromKnownColor(System.Drawing.KnownColor.LightSalmon);
            lblProcessStatus.Visible = IsMyPathCorrect;
            lblProcessStatus.Text = (rw != null && !rw.HasExited) ? "Running" : "Not running";
            lblProcessStatus.BackColor = (rw != null && !rw.HasExited) ? System.Drawing.Color.FromKnownColor(System.Drawing.KnownColor.Orange) : System.Drawing.Color.FromKnownColor(System.Drawing.KnownColor.Gray);
            if (rw != null && !rw.HasExited)
            {
                Modlist.Enabled = false;
            }
            else Modlist.Enabled = true;
            btnLaunch.Enabled = Modlist.Enabled;
            btnSelectPath.Enabled = Modlist.Enabled;

        }

        private void btnLaunch_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                rw = new System.Diagnostics.Process();
                rw.StartInfo.FileName = RootPath + @"\RainWorld.exe";
                rw.Start();
            }

            catch
            {

            }
            btnLaunch.Enabled = false;
            StatusUpdate();

        }

        private void fsw_plugins_Changed(object sender, FileSystemEventArgs e)
        {
            SomethingChanged();
        }
    }
}
