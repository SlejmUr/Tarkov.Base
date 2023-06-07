using HarmonyLib;
using Tarkov.Base.Core;
using Sirenix.Utilities;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.IO;

namespace TestMod.Patches
{
    [HarmonyPatch]
    public class SerializeMultiPatch
    {
        static IEnumerable<MethodBase> TargetMethods()
        {
            return PatchConstants.EftTypes
                .SelectMany(type => type.GetMethods())
                .Where(method => method.Name.Contains("Serialize"))
                .Cast<MethodBase>();
        }

        static void Prefix(object[] __args, MethodBase __originalMethod)
        {
            File.AppendAllText("SerializeMultiPatch.txt", $"Token: {__originalMethod.MetadataToken}\n");
            File.AppendAllText("SerializeMultiPatch.txt", $"Fullname: {__originalMethod.GetFullName()}\n");
            File.AppendAllText("SerializeMultiPatch.txt", $"Method {__originalMethod.FullDescription()}\n");
            File.AppendAllText("SerializeMultiPatch.txt", "Args: " + string.Join(" ,", __args) + "\n");
            File.AppendAllText("SerializeMultiPatch.txt", "\n");
        }
    }
}
