using Mono.Cecil;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Security;

namespace BlepOutLinx
{
	public partial class BlepOut : Form
    {

		TextWriterTraceListener tr = new TextWriterTraceListener(File.CreateText("BOILOG.txt"));
		public BlepOut()
		{
			Debug.Listeners.Add(tr);
			Debug.WriteLine("BOI starting " + DateTime.Now);
			InitializeComponent();
			targetFiles = new List<ModRelay>();
			pluginBlacklist = new List<string>();
			patchBlacklist = new List<string>();
			outrmixmods = new List<string>();
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
				Debug.WriteLine("Config retrieved. Root path: [" + RootPath + "].");
			}
			else
			{
				CreateConfig();
				Debug.WriteLine("Config file not found, creating an empty one instead.");
				RootPath = string.Empty;
			}
			
		}
		private void CreateConfig()
		{
			StreamWriter sw = File.CreateText(cfgpath);
			sw.WriteLine(string.Empty);
			sw.Close();
			sw.Dispose();
			Debug.WriteLine("Empty config file created.");
		}
		private void SaveConfig()
		{
			StreamWriter sw = File.CreateText(cfgpath);
			sw.WriteLine(RootPath);
			sw.Close();
			sw.Dispose();
			Debug.WriteLine("Config file updated, new target path: [" + RootPath + "].");
		}
		private void Setup()
		{
			Debug.WriteLine("Path valid, starting setup.");
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
			ApplyModlist();
			Modlist.Enabled = true;
			btnLaunch.Enabled = true;
			TargetSelect.SelectedPath = RootPath;
			if (PubstuntFound)
			{
				BlepOutIn.PubstuntInfoPopup popup;
				popup = new BlepOutIn.PubstuntInfoPopup();
				this.AddOwnedForm(popup);
				popup.Show();
			}
			if (MixmodsFound)
            {
				BlepOutIn.MixmodsPopup mixmodsPopup = new BlepOutIn.MixmodsPopup(outrmixmods);
				this.AddOwnedForm(mixmodsPopup);
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

						if (mt != ModRelay.ModType.Patch && !patchBlacklist.Contains(s) && !fi.Attributes.HasFlag(FileAttributes.ReparsePoint))
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
				Debug.WriteLineIf(metafiletracker, "Found modhash/modmeta files in mods folder.");
            }
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
			foreach(string s in pluginBlacklist)
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
						try
						{	
							if (fi.CreationTime > new FileInfo(pathtomods(fi)).CreationTime)
							{
								File.Delete(pathtomods(fi));
								File.Copy(s, pathtomods(fi));
								Debug.WriteLine("Updated mod: " + fi.Name + " from Plugins to Mods folder.");
							}
							if (fi.CreationTime < new FileInfo(pathtomods(fi)).CreationTime)
							{
								File.Delete(s);
								File.Copy(pathtomods(fi), s);
								Debug.WriteLine("Updated mod: " + fi.Name + " from Mods folder to Plugins");
							}
						}
						catch
						{

						}

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
					Modlist.SetItemCheckState(i, (mr.enabled) ? CheckState.Checked : CheckState.Unchecked);
				}
			}
		}

		//apply/unapply all mods needed
		//overload for autorefreshes
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
		

		private bool IsMyPathCorrect
		{
			get { return (Directory.Exists(PluginsFolder) && Directory.Exists(PatchesFolder)); }
		}

		public static string RootPath;
		private bool changetracker;
		private bool metafiletracker;
		private bool TSbtnMode = true;

		private string BOIpath
		{
			get { return Assembly.GetExecutingAssembly().Location.Replace("BlepOutIn.exe", string.Empty); }
		}

		private string cfgpath
		{
			get { return BOIpath + @"cfg.txt"; }
		}

		public static bool AintThisPS(string path)
        {
			using (ModuleDefinition md = ModuleDefinition.ReadModule(path))
            {

                return (md.Assembly.FullName.Contains("PublicityStunt"));
            }
			
			
        }

		private List<string> patchBlacklist;
		private List<string> pluginBlacklist;
		private List<string> outrmixmods;
		private List<ModRelay> targetFiles;
		private Process rw;
		private bool ReadyForRefresh;
		private bool PubstuntFound;
		private bool MixmodsFound;
		private string hkblacklistpath
		{
			get { return PluginsFolder + @"\plugins_blacklist.txt"; }
		}
		private string ptblacklistpath
		{
			get { return PatchesFolder + @"\patches_blacklist.txt"; }
		}

		public static string ModFolder
		{
			get { return RootPath + @"\Mods\"; }
		}
		public static string PluginsFolder
		{
			get { return RootPath + @"\BepInEx\plugins\"; }
		}
		public static string PatchesFolder
		{
			get { return RootPath + @"\BepInEx\monomod\"; }
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
			fsw_modsfolder.EnableRaisingEvents = false;
			fsw_pluginsfolder.EnableRaisingEvents = false;
			if (changetracker && ReadyForRefresh)
			{
				Debug.WriteLine("Changes detected in target folders, refreshing and reapplying modlist.");
				UpdateTargetPath(RootPath);
			}
			StatusUpdate();
			buttonUprootPart.Visible = Directory.Exists(RootPath + @"\RainWorld_Data\Managed_backup");
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

		private void fsw_plugins_Changed(object sender, FileSystemEventArgs e)
		{
			SomethingChanged();
		}

        private void btn_Help_Click(object sender, EventArgs e)
        {
			BlepOutIn.InfoWindow inw;
			inw = new BlepOutIn.InfoWindow(this);
			this.AddOwnedForm(inw);
			inw.Show();
        }

        private void buttonUprootPart_Click(object sender, EventArgs e)
        {
			BlepOutIn.PartYeet py = new BlepOutIn.PartYeet(this);
			this.AddOwnedForm(py);
			py.ShowDialog();
        }

        private void buttonClearMeta_Click(object sender, EventArgs e)
        {
			BlepOutIn.MetafilePurgeSuggestion psg = new BlepOutIn.MetafilePurgeSuggestion(this);
			this.AddOwnedForm(psg);
			psg.ShowDialog();
        }

        private void Modlist_ItemCheck(object sender, ItemCheckEventArgs e)
        {
			if (!ReadyForRefresh || Modlist.Items[e.Index] is ModRelay && (Modlist.Items[e.Index] as ModRelay).MyType == ModRelay.ModType.Invalid) return;
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
			Debug.Flush();
        }
    }
}
