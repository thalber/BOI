using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.IO;

namespace Blep.Backend
{
    public static class VoiceOfBees
    {
        public static void FetchList()
        {
            using (var wc = new WebClient())
            {
                try
                {
                    string euv_json = wc.DownloadString("https://beestuff.pythonanywhere.com/audb/api/v2/enduservisible");
                    var list = JsonConvert.DeserializeObject<List<AUDBEntryRelay>>(euv_json);
                    EntryList = list;
                }
                
                catch (WebException we) { Wood.WriteLine("Error fetching AUDB entries:");  Wood.WriteLine(we.Response, 1); }
            }
        }
        public static List<AUDBEntryRelay> EntryList { get { _el = _el ?? new List<AUDBEntryRelay>(); return _el; } set { _el = value; } }
        private static List<AUDBEntryRelay> _el;


        public class AUDBEntryRelay : IEquatable<AUDBEntryRelay>
        {
            public List<AUDBEntryRelay> deps { get { _deps = _deps ?? new List<AUDBEntryRelay>(); return _deps; } set { _deps = value; } }
            private List<AUDBEntryRelay> _deps;
            public Dictionary<string, string> key;
            public string name;
            public string author;
            public string description;
            public string download;
            public string sig;

            public override string ToString()
            {
                return $"{name}";
            }

            public bool TryDownload(string TargetDirectory)
            {
                using (var dwc = new WebClient())
                {
                    try
                    {
                        var mcts = dwc.DownloadData(download);
                        var sha = new SHA512Managed();
                        
                        var modhash = sha.ComputeHash(mcts);
                        var sigbytes = Convert.FromBase64String(sig);
                        var keyData = new RSAParameters();
                        keyData.Exponent = Convert.FromBase64String(key["e"]);
                        keyData.Modulus = Convert.FromBase64String(key["n"]);
                        var rsa = RSA.Create();
                        rsa.ImportParameters(keyData);
                        var def = new RSAPKCS1SignatureDeformatter(rsa);
                        def.SetHashAlgorithm("SHA512");
                        
                        if (def.VerifySignature(modhash, sigbytes))
                        {
                            Wood.WriteLine($"Mod sig verified: {this.name}, saving");
                            try
                            {
                                var tfi = new DirectoryInfo(TargetDirectory);
                                if (!tfi.Exists) { tfi.Create(); tfi.Refresh(); }
                                File.WriteAllBytes(Path.Combine(TargetDirectory, $"{this.name}.dll"), mcts);
                                if (deps.Count > 0)
                                {
                                    Wood.WriteLine("");
                                    foreach (var dep in deps)
                                    {
                                        if (dep.TryDownload(TargetDirectory)) { }
                                    }
                                }
                                
                                
                            }
                            catch (IOException ioe) 
                            { Wood.WriteLine($"Can not write the downloaded mod {this.name}:"); Wood.WriteLine(ioe, 1); return false; }
                            return true;
                        }
                        else
                        {
                            Wood.WriteLine($"Mod sig incorrect: {this.name}, download aborted");
                            return false;
                        }
                    }
                    catch (WebException we)
                    {
                        Wood.WriteLine($"Error downloading data from AUDB entry {name}:");
                        Wood.WriteLine(we, 1);
                        
                    }
                    finally
                    {
                        
                    }
                }
                return false;
            }

            public bool Equals(AUDBEntryRelay other)
            {
                return (this.download == other.download);
            }
        }
    }
}
