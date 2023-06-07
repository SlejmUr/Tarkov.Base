using EFT;
using HarmonyLib;
using Sirenix.Utilities;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace TestMod.Patches
{
    [HarmonyPatch]
    public class TarkovAppMethodNamesPrint
    {
        static List<string> DeniedList = new()
        {
            "Awake",
            "get_",
            "get_",
            "Coroutine",
            "GetClientBackEndSession",
            "OnDestroy",
            "OnApplicationFocus",
            "Invoke",
            "Component",
            "CompareTag",
            "Message",
            "GetInstanceID",
            "Equals",
            "GetHashCode",
            "ToString",
            "GetType"
        };
        static IEnumerable<MethodBase> TargetMethods()
        {
            IEnumerable<MethodBase> methods = new List<MethodBase>();
            var x = typeof(TarkovApplication).GetRuntimeMethods();
            foreach (var m in x) 
            {
                File.AppendAllText("TarkovAppMethodNamesPrint_TargetMethods.txt", $"{m.Name}\n");

                int contains = 0;
                foreach (var item in DeniedList)
                {
                    if (m.Name.Contains(item))
                    {
                        contains++;
                    }
                }
                if (contains == 0) 
                {
                    File.AppendAllText("TarkovAppMethodNamesPrint_TargetMethods.txt", $"Append: {m.Name}\n");
                    methods.Append(m);
                }
                
            }
            return methods;
        }

        static void Prefix(object[] __args, MethodBase __originalMethod)
        {
            File.AppendAllText("TarkovAppMethodNamesPrint.txt", $"Token: {__originalMethod.MetadataToken}\n");
            File.AppendAllText("TarkovAppMethodNamesPrint.txt", $"Fullname: {__originalMethod.GetFullName()}\n");
            File.AppendAllText("TarkovAppMethodNamesPrint.txt", $"Method {__originalMethod.FullDescription()} \n");
            File.AppendAllText("TarkovAppMethodNamesPrint.txt", "Args: " + string.Join(" ,", __args) + "\n");
            File.AppendAllText("TarkovAppMethodNamesPrint.txt", "\n");
        }
    }
}
