using Mono.Cecil;
using System.IO;


namespace BlepOutLinx
{
    public class ModRelay
    {
        public ModRelay(string path)
        {
            ModPath = path;
            this.isValid = !ModData.AbsolutelyIgnore(ModPath);
            if (isValid)
            {
                if (new FileInfo(path).Name.Contains("PublicityStunt"))
                {
                    this.AssociatedModData = new InvalidModData(path);
                    return;
                }
                ModType mt = GetModType(path);
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
            File.Copy(AssociatedModData.OrigPath, AssociatedModData.TarFolder + AssociatedModData.TarName);
        }

        public void Disable()
        {
            if (AssociatedModData is InvalidModData) return;
            if (!enabled) return;
            File.Delete(AssociatedModData.TarFolder + AssociatedModData.TarName);
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
}
