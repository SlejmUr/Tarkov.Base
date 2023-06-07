using FilesChecker;
using HarmonyLib;
using Sirenix.Utilities;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Tarkov.Base.Core;

namespace TestMod.Patches
{
    internal class LoggerPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            var method = typeof(AbstractLogger).GetRuntimeMethods().Where(x => x.MetadataToken == 0x06002550).FirstOrDefault();
            File.AppendAllText("LoggerPatch.txt", $"{method.GetFullName()}\n");
            File.AppendAllText("LoggerPatch.txt", $"{method.MetadataToken}\n");
            return method;
        }

        [PatchPrefix]
        private static void PatchPrefix(ref object[] __args)
        {
            File.AppendAllText("LoggerPatch.txt", $"__args: {string.Join(" ,", __args)}\n");
            var y = (object[])__args[3];
            File.AppendAllText("LoggerPatch.txt", $"__args (3): {string.Join(" ,", y)}\n");
        }
    }
}
