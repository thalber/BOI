using Mono.Cecil;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace Blep
{
    public partial class BlepOut : Form
    {
        public BlepOut()
        {
            InitializeComponent();
            firstshow = true;
            TextWriterTraceListener tr = new TextWriterTraceListener(File.CreateText("BOILOG.txt"));
            Debug.Listeners.Add(tr);
            Debug.AutoFlush = true;
            Debug.WriteLine("BOI starting " + DateTime.Now);
            targetFiles = new List<ModRelay>();
            pluginBlacklist = new List<string>();
            patchBlacklist = new List<string>();
            outrmixmods = new List<string>();
            BoiConfigManager.ReadConfig();
            UpdateTargetPath(BoiConfigManager.TarPath);
            firstshow = false;
            if (File.Exists(Path.Combine(RootPath, "BepInEx", "LogOutput.log")))
            {
                string[] lans = File.ReadAllLines(Path.Combine(RootPath, "BepInEx", "LogOutput.log"));
                for (int cuwo = 0; cuwo < lans.Length; cuwo++)
                {
                    string scrpyr = lans[cuwo];
                    if (scrpyr.Contains("Here be dragons!"))
                    {

                        Debug.WriteLine("Dragon thoughts found. Saying hi.");
                        goto iolaa;
                    }
                }
            iolaa:
                {
                    Debug.WriteLine("...");
                    Debug.WriteLine("To you and your parent, greetings. May your work persist for as long as we do.");
                    Debug.WriteLine("Wish you all well. Bzz!");
                }
            }
        }
        public void UpdateTargetPath(string path)
        {
            btnLaunch.Enabled = false;
            Modlist.Enabled = false;
            RootPath = path;
            BoiConfigManager.TarPath = path;
            targetFiles.Clear();
            pluginBlacklist.Clear();
            patchBlacklist.Clear();
            if (IsMyPathCorrect) Setup();
            StatusUpdate();
        }
        private void Setup()
        {
            Debug.WriteLine("Path valid, starting setup " + DateTime.Now);
            Debug.Indent();
            PubstuntFound = false;
            MixmodsFound = false;
            metafiletracker = false;
            ReadyForRefresh = false;
            Modlist.Items.Clear();
            outrmixmods.Clear();
            targetFiles.Clear();

            Rootout();
            PrepareModsFolder();
            ResolveBlacklists();
            RetrieveAllDlls();
            CompileModList();
            BringUpToDate();
            Modlist.Enabled = true;
            btnLaunch.Enabled = true;
            TargetSelect.SelectedPath = RootPath;
            if (PubstuntFound && firstshow)
            {
                Blep.PubstuntInfoPopup popup;
                popup = new Blep.PubstuntInfoPopup();
                AddOwnedForm(popup);
                popup.Show();
            }
            if (MixmodsFound)
            {
                MixmodsPopup mixmodsPopup = new Blep.MixmodsPopup(outrmixmods);
                AddOwnedForm(mixmodsPopup);
                mixmodsPopup.Show();
            }
            ReadyForRefresh = true;
            buttonClearMeta.Visible = metafiletracker;
            Debug.Unindent();
        }

        private void ResolveBlacklists()
        {
            RetrieveHkBlacklist();
            RetrievePtBlacklist();
        }

        //deletes Pubstunt and mixmods
        private void Rootout()
        {
            string[] patchfoldercontents = Directory.GetFiles(PatchesFolder);
            string[] pluginfoldercontents = Directory.GetFiles(PluginsFolder);
            foreach (string s in patchfoldercontents)
            {
                var fi = new FileInfo(s);
                if (fi.Extension == ".dll" && !fi.Attributes.HasFlag(FileAttributes.ReparsePoint))
                {
                    if (AintThisPS(s))
                    {
                        PubstuntFound = true;
                        Debug.WriteLine("Located PublicityStunt in active Plugins folder, removing.");
                        File.Delete(s);
                    }
                    else
                    {
                        ModRelay.ModType mt = ModRelay.GetModType(s);
                        if (!(mt == ModRelay.ModType.Patch || mt == ModRelay.ModType.Invalid) && !patchBlacklist.Contains(s) && !fi.Attributes.HasFlag(FileAttributes.ReparsePoint))
                        {
                            Debug.WriteLine("Found a misplaced mod in Patches folder: " + fi.Name + "; Type: " + mt.ToString());
                            if (File.Exists(ModFolder + PtModData.GiveMeBackMyName(fi.Name)))
                            {
                                File.Delete(s);
                            }
                            else
                            {
                                File.Move(s, ModFolder + PtModData.GiveMeBackMyName(fi.Name));
                            }
                        }
                    }
                }
            }
            foreach (string s in pluginfoldercontents)
            {
                var fi = new FileInfo(s);
                if (fi.Extension == ".dll" && !pluginBlacklist.Contains(s) && !fi.Attributes.HasFlag(FileAttributes.ReparsePoint))
                {

                    if (AintThisPS(s))
                    {
                        File.Delete(s);
                        Debug.WriteLine("Located PublicityStunt in active Plugins folder, removing.");
                        PubstuntFound = true;
                    }
                    else
                    {
                        ModRelay.ModType mt = ModRelay.GetModType(s);

                        if (mt == ModRelay.ModType.Patch || mt == ModRelay.ModType.Invalid)
                        {
                            Debug.WriteLine("Found a misplaced mod in Plugins folder: " + fi.Name + "; Type: " + mt.ToString());
                            if (File.Exists(ModFolder + PtModData.GiveMeBackMyName(fi.Name)))
                            {
                                File.Delete(s);
                                Debug.WriteLine("Duplicate exists in Mods folder, deleting.");
                            }
                            else
                            {
                                Debug.WriteLine("Moving to Mods folder.");
                                File.Move(s, ModFolder + PtModData.GiveMeBackMyName(fi.Name));
                            }
                        }
                    }
                }
            }
        }
        private void PrepareModsFolder()
        {
            metafiletracker = false;
            if (!Directory.Exists(ModFolder))
            {
                Debug.WriteLine("Mods folder not found, creating.");
                Directory.CreateDirectory(ModFolder);
            }
            string[] modfldcontents = Directory.GetFiles(ModFolder);
            foreach (string path in modfldcontents)
            {
                var fi = new FileInfo(path);
                if (fi.Extension == ".modHash" || fi.Extension == ".modMeta")
                {
                    metafiletracker = true;
                }
            }
            Debug.WriteLineIf(metafiletracker, "Found modhash/modmeta files in mods folder.");
        }

        //
        //blacklist stuff
        //
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
            Debug.WriteLine("Plugin folder blacklist contents: ");
            Debug.Indent();
            foreach (string s in pluginBlacklist)
            {
                Debug.WriteLine(new FileInfo(s).Name);
            }
            Debug.Unindent();
        }
        private void CreateHkBlacklist()
        {
            Debug.WriteLine("Creating new plugin blacklist file.");
            StreamWriter sw = File.CreateText(hkblacklistpath);
            sw.WriteLine("LogFix.dll");
            sw.Close();
            sw.Dispose();

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
            Debug.WriteLine("Patch folder blacklist contents: ");
            Debug.Indent();
            foreach (string s in patchBlacklist)
            {
                Debug.WriteLine(new FileInfo(s).Name);
            }
            Debug.Unindent();
        }
        private void CreatePtBlacklist()
        {
            StreamWriter sw = File.CreateText(ptblacklistpath);
            sw.WriteLine("Assembly-CSharp.PatchNothing.mm.dll");
            sw.Close();
        }

        //
        //blacklist stuff over
        //

        //brings mods up to date if necessary
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

                    }
                    else
                    {
                        File.Copy(s, pathtomods(fi));
                        Debug.WriteLine(fi.Name + " from Plugins does not have a counterpart in Mods, copying.");
                    }

                }

            }
            string[] patchFolderContents = Directory.GetFiles(PatchesFolder);
            foreach (string s in patchFolderContents)
            {
                var fi = new FileInfo(s);
                if (fi.Extension == ".dll" && !patchBlacklist.Contains(fi.Name) && !fi.Attributes.HasFlag(FileAttributes.ReparsePoint))
                {
                    if (!File.Exists(ModFolder + PtModData.GiveMeBackMyName(fi.Name)))
                    {
                        File.Copy(s, ModFolder + PtModData.GiveMeBackMyName(fi.Name));
                        Debug.WriteLine(fi.Name + "from patch folder does not have a counterpart in Mods, copying");
                    }
                    else if (fi.LastWriteTime < new FileInfo(ModFolder + PtModData.GiveMeBackMyName(fi.Name)).LastWriteTime)
                    {
                        File.Delete(s);
                        File.Copy(ModFolder + PtModData.GiveMeBackMyName(fi.Name), s);
                        Debug.WriteLine(fi.Name + "from Patches folder brought up to date.");
                    }
                }
            }
            string pathtomods(FileInfo fi)
            {
                return Path.Combine(ModFolder, fi.Name);
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
                if (AintThisPS(s)) PubstuntFound = true;
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
                    Modlist.SetItemCheckState(i, (mr.enabled) ? CheckState.Checked : CheckState.Unchecked);
                }
            }
        }

        //apply/unapply all mods needed
        //overload for autorefreshes (unneeded)
        [Obsolete]
        private void ApplyModlist()
        {
            Debug.WriteLine("Applying modlist.");
            Debug.Indent();
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
                    Debug.WriteLine(mr.AssociatedModData.DisplayedName + " : " + ((mr.enabled) ? "ON" : "OFF"));
                }

            }
            Debug.Unindent();
        }
        //overload for manual checks/unchecks
        private void ApplyModlist(CheckState[] cst)
        {
            Debug.WriteLine("Applying modlist from manual check.");
            if (cst != null && cst.Length == Modlist.Items.Count)
            {
                for (int i = 0; i < cst.Length; i++)
                {
                    if (Modlist.Items[i] is ModRelay)
                    {
                        ModRelay mr = Modlist.Items[i] as ModRelay;
                        if (cst[i] == CheckState.Checked)
                        {
                            mr.Enable();
                        }
                        else mr.Disable();
                    }
                }
            }
        }

        private void BringUpToDate()
        {
            foreach (ModRelay mr in Modlist.Items)
            {
                if (mr.MyType == ModRelay.ModType.Invalid || !mr.enabled) continue;
                byte[] ModOrigSha = mr.origchecksum;
                byte[] ModTarSha = mr.TarCheckSum;

                if (!BoiCustom.BOIC_Bytearr_Compare(ModTarSha, ModOrigSha))
                {
                    FileInfo orfi = new FileInfo(mr.ModPath);
                    FileInfo tarfi = new FileInfo(mr.TarPath);
                    if (orfi.LastWriteTime > tarfi.LastWriteTime)
                    {
                        File.Delete(tarfi.FullName);
                        File.Copy(orfi.FullName, tarfi.FullName);
                        Debug.WriteLine($"Bringing {orfi.Name} up to date: copying from Mods to {((mr.MyType != ModRelay.ModType.Patch) ? "Plugins" : "Monomod")}.");
                    }
                    else
                    {
                        File.Delete(orfi.FullName);
                        File.Copy(tarfi.FullName, orfi.FullName);
                        Debug.WriteLine($"Bringing {orfi.Name} up to date: copying from {((mr.MyType != ModRelay.ModType.Patch) ? "Plugins" : "Monomod")} to Mods.");
                    }
                }
            }
        }
        public bool IsMyPathCorrect
        {
            get { return (Directory.Exists(PluginsFolder) && Directory.Exists(PatchesFolder)); }
        }
        public static string RootPath = string.Empty;
        private bool metafiletracker;
        private bool TSbtnMode = true;
        private Blep.Options opwin;
        private Blep.InvalidModPopup inp;
        private Blep.InfoWindow iw;

        public static string BOIpath
        {
            get { return Assembly.GetExecutingAssembly().Location.Replace("BlepOutIn.exe", string.Empty); }
        }

        public static string cfgpath
        {
            get { return Path.Combine(BOIpath, "cfg.json"); }
        }

        public static bool AintThisPS(string path)
        {
            var fi = new FileInfo(path);
            if (fi.Extension != ".dll" || fi.Attributes.HasFlag(FileAttributes.ReparsePoint)) return false;
            try
            {
                using (ModuleDefinition md = ModuleDefinition.ReadModule(path))
                {
                    return (md.Assembly.FullName.Contains("PublicityStunt"));
                }
            }
            catch (IOException ioe)
            {
                Debug.WriteLine("ATPS: ERROR CHECKING MOD ASSEMBLY :");
                Debug.Indent();
                Debug.WriteLine(ioe);
                Debug.Unindent();
                Debug.WriteLine("Well, it's probably not PS.");
                return false;
            }

        }

        private List<string> patchBlacklist;
        private List<string> pluginBlacklist;
        private List<string> outrmixmods;
        private List<ModRelay> targetFiles;
        private Process rw;
        private bool firstshow;
        private bool ReadyForRefresh;
        private bool PubstuntFound;
        private bool MixmodsFound;
        private string hkblacklistpath
        {
            get { return Path.Combine(PluginsFolder, @"plugins_blacklist.txt"); }
        }
        private string ptblacklistpath
        {
            get { return Path.Combine(PatchesFolder, @"patches_blacklist.txt"); }
        }

        public static string ModFolder
        {
            get { return Path.Combine(RootPath, "Mods"); }
        }
        public static string PluginsFolder
        {
            get { return Path.Combine(RootPath, "BepInEx", "plugins"); }
        }
        public static string PatchesFolder
        {
            get { return Path.Combine(RootPath, "BepInEx", "monomod"); }
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
        private void BlepOut_Activated(object sender, EventArgs e)
        {
            UpdateTargetPath(RootPath);
            StatusUpdate();
            buttonUprootPart.Visible = Directory.Exists(RootPath + @"\RainWorld_Data\Managed_backup");
        }
        private void BlepOut_Deactivate(object sender, EventArgs e)
        {
            if (IsMyPathCorrect && Directory.Exists(ModFolder))
            {

            }
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
                Debug.WriteLine("Game launched.");
            }

            catch (Exception ce)
            {
                Debug.WriteLine("Launch failed");
                Debug.WriteLine(ce);
            }
            btnLaunch.Enabled = false;
            StatusUpdate();

        }
        private void btn_Help_Click(object sender, EventArgs e)
        {
            if (iw == null || iw.IsDisposed) iw = new Blep.InfoWindow(this);
            iw.Show();
        }
        private void buttonUprootPart_Click(object sender, EventArgs e)
        {
            Blep.PartYeet py = new Blep.PartYeet(this);
            AddOwnedForm(py);
            py.ShowDialog();
        }
        private void buttonClearMeta_Click(object sender, EventArgs e)
        {
            Blep.MetafilePurgeSuggestion psg = new Blep.MetafilePurgeSuggestion(this);
            AddOwnedForm(psg);
            psg.ShowDialog();
        }
        private void Modlist_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (!ReadyForRefresh) return;
            if (Modlist.Items[e.Index] is ModRelay && (Modlist.Items[e.Index] as ModRelay).MyType == ModRelay.ModType.Invalid && e.NewValue == CheckState.Checked)
            {
                //e.NewValue = CheckState.Unchecked;
                if (inp == null || inp.IsDisposed) inp = new InvalidModPopup(this, (Modlist.Items[e.Index] as ModRelay).AssociatedModData.DisplayedName);
                inp.ShowDialog();
            }
            CheckState[] ich = new CheckState[Modlist.Items.Count];
            for (int i = 0; i < ich.Length; i++)
            {
                ich[i] = Modlist.GetItemCheckState(i);
            }
            ich[e.Index] = e.NewValue;
            Debug.WriteLine("Mod state about to change: " + (Modlist.Items[e.Index] as ModRelay).AssociatedModData.DisplayedName + "; Type: " + (Modlist.Items[e.Index] as ModRelay).MyType.ToString());
            ApplyModlist(ich);
            Debug.WriteLine("Resulting state: " + ((Modlist.Items[e.Index] as ModRelay).enabled ? "ON" : "OFF"));
        }
        private void BlepOut_FormClosing(object sender, FormClosingEventArgs e)
        {
            Debug.WriteLine("BOI shutting down. " + DateTime.Now);
            BoiConfigManager.WriteConfig();
        }
        private void buttonOption_Click(object sender, EventArgs e)
        {
            if (opwin == null || opwin.IsDisposed) opwin = new Options(this);
            opwin.Show();
        }

        private void Modlist_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            Debug.WriteLine($"Drag&Drop: {files.Length} files were dropped in the mod list.");
            Debug.Indent();
            foreach (string file in files)
            {
                // get the file info for easier operations
                FileInfo ModFileInfo = new FileInfo(file);
                // check if we are dealing with a dll file
                if (!String.Equals(ModFileInfo.Extension, ".dll", StringComparison.CurrentCultureIgnoreCase))
                {
                    Debug.WriteLine($"Error: {ModFileInfo.Name} was ignored, as it is not a dll file.");
                    continue;
                }
                // move the dll file to the Mods folder
                string ModFilePath = Path.Combine(RootPath, "Mods", ModFileInfo.Name);
                if(System.IO.File.Exists(ModFilePath))
                {
                    Debug.WriteLine($"Error: {ModFileInfo.Name} was ignored, as it already exists.");
                    continue;
                }
                // move the dll file to the Mods folder
                System.IO.File.Copy(ModFileInfo.FullName, ModFilePath);
                // get mod data
                var mr = new ModRelay(ModFilePath);
                // add the mod to the mod list
                Modlist.Items.Add(mr);
                Debug.WriteLine($"{ModFileInfo.Name} successfully added.");
                // since it's a new mod just added to the folder, it shouldn't be checked as active, nothing else to do here
            }
            Debug.Unindent();
            Debug.WriteLine("Drag&Drop operation ended.");
        }

        private void Modlist_DragEnter(object sender, DragEventArgs e)
        {
            // if we're about to drop a file, indicate a copy to allow the drop
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }
    }
}
