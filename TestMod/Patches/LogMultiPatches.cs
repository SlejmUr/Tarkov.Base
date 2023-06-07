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
    public class LogMultiPatches
    {
        static IEnumerable<MethodBase> TargetMethods()
        {
            return typeof(UnityEngine.Debug).GetRuntimeMethods()
                .Where(method => method.Name.Contains("Log") && method.Name != "get_unityLogger")
                .Cast<MethodBase>();
        }

        static void Prefix(object[] __args, MethodBase __originalMethod)
        {
            File.AppendAllText("LogMultiPatches.txt", $"Token: {__originalMethod.MetadataToken}\n");
            File.AppendAllText("LogMultiPatches.txt", $"Fullname: {__originalMethod.GetFullName()}\n");
            File.AppendAllText("LogMultiPatches.txt", $"Method {__originalMethod.FullDescription()} \n");
            File.AppendAllText("LogMultiPatches.txt", "Args: " + string.Join(" ,", __args) + "\n");
            File.AppendAllText("LogMultiPatches.txt", "\n");
        }
    }
}
