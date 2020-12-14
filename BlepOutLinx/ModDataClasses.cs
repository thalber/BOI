using Mono.Cecil;
using System.IO;
using System.Security.Cryptography;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Windows.Forms;


namespace BlepOutLinx
{
    //
    //  CODE MODS
    //

    public class ModRelay
    {
        public ModRelay(string path)
        {
            ModPath = path;
            this.isValid = !ModData.AbsolutelyIgnore(ModPath);
            if (isValid)
            {
                if (BlepOut.AintThisPS(path))
                {
                    this.AssociatedModData = new InvalidModData(path);
                    this.MyType = ModType.Invalid;
                    return;
                }
                ModType mt = GetModType(ModPath);
                switch (mt)
                {
                    case ModType.Unknown:
                        this.AssociatedModData = new ModData(path);
                        this.MyType = ModType.Unknown;
                        break;
                    case ModType.Patch:
                        this.AssociatedModData = new PtModData(path);
                        this.MyType = ModType.Patch;
                        break;
                    case ModType.Partmod:
                        this.AssociatedModData = new HkModData(path);
                        this.MyType = ModType.Partmod;
                        break;
                    case ModType.BepPlugin:
                        this.AssociatedModData = new BepPluginData(path);
                        this.MyType = ModType.BepPlugin;
                        break;
                    case ModType.Invalid:
                        this.AssociatedModData = new InvalidModData(path);
                        this.MyType = ModType.Invalid;
                        break;
                }
            }
        }

        public static ModType GetModType(string path)
        {
            mttup ultstate = new mttup(false, false, false);
            try
            {
                using (ModuleDefinition md = ModuleDefinition.ReadModule(path))
                {

                    foreach (TypeDefinition t in md.Types)
                    {
                        mttup tstate = new mttup(false, false, false);
                        CheckThisType(t, out tstate);
                        ultstate.ishk = (tstate.ishk) ? true : ultstate.ishk;
                        ultstate.ispt = (tstate.ispt) ? true : ultstate.ispt;
                        ultstate.isbeppl = (tstate.isbeppl) ? true : ultstate.isbeppl;
                    }

                }
            }
            catch (IOException ioe)
            {
                Debug.WriteLine("ERROR CHECKING ASSEMBLY TYPE: IOException occured");
                Debug.Indent();
                Debug.WriteLine(ioe);
                Debug.Unindent();
            }
            int ftc = 0;
            if (ultstate.ishk) ftc++;
            if (ultstate.ispt) ftc++;
            if (ultstate.isbeppl) ftc++;
            switch (ftc)
            {
                case 0:
                    return ModType.Unknown;
                case 3:
                    return ModType.Invalid;
                case 2:
                    return (ultstate.ispt) ? ModType.Invalid : ModType.Unknown;
                case 1:
                    if (ultstate.ishk) return ModType.Partmod;
                    else if (ultstate.ispt) return ModType.Patch;
                    else return ModType.BepPlugin;
                default:
                    return ModType.Unknown;
            }
            
        }

        public static void CheckThisType(TypeDefinition td, out mttup state)
        {
            state.ishk = false;
            state.ispt = false;
            state.isbeppl = false;
            if (td.BaseType != null && td.BaseType.Name == "PartialityMod") state.ishk = true;
            if (td.HasCustomAttributes)
            {
                foreach (CustomAttribute catr in td.CustomAttributes)
                {
                    if (catr.AttributeType.Name == "MonoModPatch") state.ispt = true;
                    if (catr.AttributeType.Namespace == "BepInEx") state.isbeppl = true;
                }
            }
            if (td.HasNestedTypes)
            {
                foreach (TypeDefinition ntd in td.NestedTypes)
                {
                    mttup nestate;
                    CheckThisType(ntd, out nestate);
                    state.ishk = (nestate.ishk) ? true : state.ishk;
                    state.ispt = (nestate.ispt) ? true : state.ispt;
                    state.isbeppl = (nestate.isbeppl) ? true : state.isbeppl;
                }
            }
        }

        public struct mttup
        {
            public mttup(bool hk, bool pt, bool beppl)
            {
                isbeppl = beppl;
                ishk = hk;
                ispt = pt;
            }
            public bool ishk;
            public bool ispt;
            public bool isbeppl;
        }
        public byte[] origchecksum
        {
            get
            {
                using (FileStream fs = File.OpenRead(ModPath))
                {
                    SHA256 sha = new SHA256Managed();
                    return sha.ComputeHash(fs);
                }
            }
        }
        public byte[] TarCheckSum
        {
            get
            {
                if (AssociatedModData is InvalidModData)
                {
                    return origchecksum;
                }
                using (FileStream fs = File.OpenRead(TarPath))
                {
                    SHA256 sha = new SHA256Managed();
                    return sha.ComputeHash(fs);
                }
            }
        }
        public string TarPath
        {
            get
            {
                return (AssociatedModData.TarFolder + AssociatedModData.TarName);
            }
        }

        public string ModPath;
        public ModData AssociatedModData;
        public bool isValid;

        public enum ModType
        {
            Patch,
            Partmod,
            Invalid,
            BepPlugin,
            Unknown
        }
        public ModType MyType;

        public bool enabled
        {
            get { return AssociatedModData.Enabled; }
        }

        public void Enable()
        {
            if (AssociatedModData is InvalidModData) return;
            if (enabled) return;
            File.Copy(AssociatedModData.OrigPath, TarPath);
        }

        public void Disable()
        {
            if (AssociatedModData is InvalidModData) return;
            if (!enabled) return;
            File.Delete(TarPath);
        }

        public override string ToString()
        {
            return AssociatedModData.DisplayedName + " : " + this.MyType.ToString().ToUpper();
        }
    }

    public class ModData
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
            get { return BlepOut.RootPath + @"\BepInEx\Plugins\"; }
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

    public class HkModData : ModData
    {
        public HkModData(string path) : base(path)
        {

        }

        public override string ToString()
        {
            return DisplayedName + " : HOOK";
        }
    }

    public class PtModData : ModData
    {
        public PtModData(string path) : base(path)
        {

        }

        public override string TarName => "Assembly-CSharp." + DisplayedName.Replace(".dll", string.Empty) + ".mm.dll";

        public override string TarFolder => BlepOut.RootPath + @"\BepInEx\Monomod\";

        public static string GiveMeBackMyName(string partname)
        {

            string sl = partname;
            if (sl.StartsWith("Assembly-CSharp.") && sl.EndsWith(".mm.dll"))
            {
                sl = sl.Replace("Assembly-CSharp.", string.Empty);
                sl = sl.Replace(".mm.dll", ".dll");
            }
            return sl;
        }

        //public override bool Enabled => File.Exists(this.TarFolder + this.TarName);

        public override string ToString()
        {
            return DisplayedName + " : PATCH";
        }
    }

    public class BepPluginData : ModData
    {
        public BepPluginData(string path) : base(path)
        {

        }

        public override string ToString()
        {
            return DisplayedName + " : PLUGIN";
        }
    }

    public class InvalidModData : ModData
    {
        public InvalidModData(string path) : base(path)
        {

        }

        public override bool Enabled => false;

        public override string TarName => null;

        public override string ToString()
        {
            return DisplayedName + ": INVALID";
        }


    }

    //
    //  REGMODS
    //

    public class RegModData
    {
        public RegModData(string pth)
        {
            path = pth;
            hasBeenChanged = false;
            ReadRegInfo();
        }

        
        private JObject jo;
        private string path;
        public bool hasBeenChanged;
        public enum CfgState
        {
            RegInfo,
            PackInfo,
            None
        }
        public CfgState CurrCfgState
        {
            get
            {
                if (File.Exists(path + @"\packInfo.json"))
                {
                    return CfgState.PackInfo;
                }
                else if (File.Exists(path + @"\regionInfo.json"))
                {
                    return CfgState.RegInfo;
                }
                else return CfgState.None;
            }
        }
        public string pathToCfg
        {
            get
            {
                switch (CurrCfgState)
                {
                    case CfgState.PackInfo:
                        return path + @"\packInfo.json";
                    case CfgState.RegInfo:
                        return path + @"\regionInfo.json";
                    case CfgState.None:
                        return null;
                    default: return null;
                }
            }
        }
        public string regionName
        {
            get
            {
                if (jo == null || !jo.ContainsKey("regionName")) return new DirectoryInfo(path).Name;
                return (string)jo["regionName"];
            }
        }
        public string description
        {
            get
            {
                if (jo == null || !jo.ContainsKey("description")) return "Settings file could not have been loaded; description inaccessible.";
                return (string)jo["description"];
            }
        }
        public bool activated
        {
            get
            {
                if (jo == null || !jo.ContainsKey("activated")) return false;
                return (bool)jo["activated"];
            }

            set 
            {
                if (jo == null || !jo.ContainsKey("activated")) return;
                hasBeenChanged = true;
                jo["activated"] = value;
            }
        }
        public bool structureValid
        {
            get
            {
                return (Directory.Exists(path + @"\World") || Directory.Exists(path + @"\Levels"));
            }
        }
        public int? loadOrder
        {
            get
            {
                if (jo == null || !jo.ContainsKey("loadOrder")) return null;
                return (int)jo["loadOrder"];
            }
            set
            {
                if (jo == null || !jo.ContainsKey("loadOrder")) return;
                hasBeenChanged = true;
                jo["loadOrder"] = value;
            }
        }

        public void ReadRegInfo()
        {
            if (pathToCfg != null)
            {
                string jscts = File.ReadAllText(pathToCfg);
                jo = JObject.Parse(jscts);
            }
        }
        public void WriteRegInfo()
        {
            if (jo == null)
            {
                Debug.WriteLine($"Region mod {regionName} does not have a config file; cannot apply any changes.");
                return;
            }
            hasBeenChanged = false;
            File.WriteAllText(pathToCfg, jo.ToString());
        }

        public override string ToString()
        {
            return regionName;
        }
    }

    //
    // EDT CONFIG it's not a mod config but who the fuck cares lol
    //

    public static class EDTCFGDATA
    {
        public static JObject jo;
        public static bool hasBeenChanged = false;
        public static string edtConfigPath => BlepOut.RootPath + @"\edtSetup.json";
        public static bool edtConfigExists => File.Exists(edtConfigPath);
        public static void loadJo()
        {
            jo = null;
            if (!edtConfigExists) return;
            try
            {
                string jsf = File.ReadAllText(edtConfigPath);
                jo = JObject.Parse(jsf);
            }
            catch (IOException ioe)
            {
                Debug.WriteLine("Error reading EDT config file:");
                Debug.Indent();
                Debug.WriteLine(ioe);
                Debug.Unindent();
            }
            catch (JsonReaderException jre)
            {
                Debug.WriteLine("Error parsing EDT config:");
                Debug.Indent();
                Debug.WriteLine(jre);
                Debug.Unindent();
            }
            hasBeenChanged = false;
            
        }
        public static void SaveJo()
        {
            if (!hasBeenChanged) return;
            try
            {
                File.WriteAllText(edtConfigPath, jo.ToString());
            }
            catch (IOException ioe)
            {
                Debug.WriteLine("Error writing EDT config file:");
                Debug.Indent();
                Debug.WriteLine(ioe);
                Debug.Unindent();
            }
            catch (System.ArgumentNullException)
            {
                Debug.WriteLine("JO is null; nothing to write.");
            }
        }
        public static string startmap
        {
            get
            {
                if (jo == null || !jo.ContainsKey("start_map")) return null;
                return (string)jo["start_map"];
            }
            set
            {
                if (jo == null) return;
                hasBeenChanged = true;
                jo["start_map"] = value;
            }
        }
        public static bool? skiptitle
        {
            get
            {
                if (jo == null || !jo.ContainsKey("skip_title")) return null;
                return (bool)jo["skip_title"];
            }
            set
            {
                if (jo == null) return;
                hasBeenChanged = true;
                jo["skip_title"] = value;
            }
        }
        public static int? forcechar
        {
            get
            {
                if (jo == null || !jo.ContainsKey("force_selected_character")) return null;
                return (int)jo["force_selected_character"];
            }
            set
            {
                if (jo == null) return;
                hasBeenChanged = true;
                jo["force_selected_character"] = value;
            }
        }
        public static bool? norain
        {
            get
            {
                if (jo == null || !jo.ContainsKey("no_rain")) return null;
                return (bool)jo["no_rain"];
            }
            set
            {
                if (jo == null) return;
                hasBeenChanged = true;
                jo["no_rain"] = value;
            }
        }
        public static bool? devtools
        {
            get
            {
                if (jo == null || !jo.ContainsKey("devtools")) return null;
                return (bool)jo["devtools"];
            }
            set
            {
                if (jo == null) return;
                hasBeenChanged = true;
                jo["devtools"] = value;
            }
        }
        public static int? cheatkarma
        {
            get
            {
                if (jo == null || !jo.ContainsKey("cheat_karma")) return null;
                return (int)jo["cheat_karma"];
            }
            set
            {
                if (jo == null) return;
                hasBeenChanged = true;
                jo["cheat_karma"] = value;
            }
        }
        public static bool? revealmap
        {
            get
            {
                if (jo == null || !jo.ContainsKey("reveal_map")) return null;
                return (bool)jo["reveal_map"];
            }
            set
            {
                if (jo == null) return;
                hasBeenChanged = true;
                jo["reveal_map"] = value;
            }
        }
        public static bool? forcelight
        {
            get
            {
                if (jo == null || !jo.ContainsKey("force_light")) return null;
                return (bool)jo["force_light"];
            }
            set
            {
                if (jo == null) return;
                hasBeenChanged = true;
                jo["force_light"] = value;
            }
        }
        public static bool? bake
        {
            get
            {
                if (jo == null || !jo.ContainsKey("bake")) return null;
                return (bool)jo["bake"];
            }
            set
            {
                if (jo == null) return;
                hasBeenChanged = true;
                jo["bake"] = value;
            }
        }
        public static bool? encrypt
        {
            get
            {
                if (jo == null || !jo.ContainsKey("encrypt")) return null;
                return (bool)jo["encrypt"];
            }
            set
            {
                if (jo == null) return;
                hasBeenChanged = true;
                jo["devtools"] = value;
            }
        }
    }
}
