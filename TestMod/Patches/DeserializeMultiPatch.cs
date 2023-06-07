using HarmonyLib;
using Sirenix.Utilities;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Tarkov.Base.Core;

namespace TestMod.Patches
{
    [HarmonyPatch]
    public class DeserializeMultiPatch
    {
        static List<string> DeniedList = new()
        {
            "GClass1215",
            "GClass1214",
            "GClass1213",
            "GClass1178",
            "GClass1117",
            "GClass1102",
            "FullItemLocationSerializer"
        };
        static IEnumerable<MethodBase> TargetMethods()
        {
            return PatchConstants.EftTypes
                .Where(type => !DeniedList.Contains(type.Name))
                .SelectMany(type => type.GetMethods())
                .Where(method => method.Name.Contains("Deserialize") && method.Name != "OnAfterDeserialize")
                .Cast<MethodBase>();
        }

        static void Prefix(object[] __args, MethodBase __originalMethod)
        {
            File.AppendAllText("DeserializeMultiPatch.txt", $"Token: {__originalMethod.MetadataToken}\n");
            File.AppendAllText("DeserializeMultiPatch.txt", $"Fullname: {__originalMethod.GetFullName()}\n");
            File.AppendAllText("DeserializeMultiPatch.txt", $"Method {__originalMethod.FullDescription()} \n");
            File.AppendAllText("DeserializeMultiPatch.txt", "Args: " + string.Join(" ,", __args) + "\n");
            File.AppendAllText("DeserializeMultiPatch.txt", "\n");
        }
    }
}
