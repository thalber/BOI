using System;
using System.Reflection;
using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;
using Mono.Cecil;
using System.Resources;
using System.Runtime.InteropServices;


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

		public override void Refresh()
		{
			base.Refresh();
		}

		public void UpdateTargetPath(string path)
		{
			RootPath = path;
			targetFiles.Clear();
			pluginBlacklist.Clear();
			patchBlacklist.Clear();
			if (IsMyPathCorrect) Setup();
			SaveConfig();
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
			Modlist.Items.Clear();
			targetFiles.Clear();
            PrepareModsFolder();
			ResolveBlacklists();
			RetrieveAllDlls();
			CompileModList();
			RefreshList();
			
		}

		private void ResolveBlacklists()
        {
			RetrieveHkBlacklist();
			RetrievePtBlacklist();
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
						if (fi.LastWriteTime > new FileInfo(pathtomods(fi)).LastWriteTime)
                        {
							File.Delete(pathtomods(fi));
							File.Copy(s, pathtomods(fi));
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

		public void RefreshList()
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

		bool IsMyPathCorrect
        {
			get { return (Directory.Exists(PluginsFolder) && Directory.Exists(PatchesFolder)); }
        }
		static string RootPath;
		string BOIpath
        {
			get { return Assembly.GetExecutingAssembly().Location.Replace("BlepOutLinx.exe", string.Empty); }
        }

		string cfgpath
        {
			get { return BOIpath + @"cfg.txt"; }
        }
		List<string> patchBlacklist;
		List<string> pluginBlacklist;
		List<ModRelay> targetFiles;
		
		//List<TargetFileData> IgnoredFailed;
		string hkblacklistpath
        {
			get { return PluginsFolder + @"\plugins_blacklist.txt"; }
        }
		string ptblacklistpath
        {
			get { return PatchesFolder + @"\patches_blacklist.txt"; }
        }
		string ModFolder
		{
			get { return RootPath + @"\Mods\"; }
		}
		string PluginsFolder
		{
			get { return RootPath + @"\BepInEx\plugins\"; }
		}
		string PatchesFolder
		{
			get { return RootPath + @"\BepInEx\monomod\"; }
		}
		class ModRelay
        {
			public ModRelay(string path)
            {
				ModPath = path;
				this.isValid = !ModData.AbsolutelyIgnore(ModPath);
				if (isValid)
                {
					bool ishk = false;
					bool ispt = false;
					ModuleDefinition md = ModuleDefinition.ReadModule(path);
					//Type[] tps = asm.GetTypes();
					try
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
                    catch
                    {
						this.AssociatedModData = null;
						this.AssociatedModData = new ModData(path);
                    }

				}
            }
			string ModPath;
			ModData AssociatedModData;
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
		class ModData
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
			public static bool AbsolutelyIgnore (string tpath)
			{
				return (new FileInfo(tpath).Extension != @".dll" || new FileInfo(tpath).Attributes.HasFlag(FileAttributes.ReparsePoint));
			}
			public override string ToString()
			{
				return DisplayedName + " : UNKNOWN";
			}
		}

		class HkModData : ModData
        {
			public HkModData (string path) : base(path)
            {

            }

            public override string ToString()
            {
                return DisplayedName + " : HOOK";
            }
        }

		class PtModData : ModData
        {
			public PtModData (string path) : base(path)
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

		class MixedModData : ModData
        {
			public MixedModData (string path) : base(path)
            {

            }

            public override bool Enabled => false;

            public override string TarName => null;

			
        }

		private void buttonSelectPath_Click(object sender, EventArgs e)
		{
			TargetSelect.ShowDialog();
		}
		private void TargetSelect_Closed(object sender, EventArgs e)
		{
			this.UpdateTargetPath(TargetSelect.SelectedPath);
		}

        private void checklistModlist_SelectedIndexChanged(object sender, EventArgs e)
        {
			RefreshList();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
			UpdateTargetPath(TargetSelect.SelectedPath);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
			UpdateTargetPath(RootPath);
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void labelHead_Click(object sender, EventArgs e)
        {

        }

        private void BlepOut_Load(object sender, EventArgs e)
        {

        }
    }
}
