using EFT;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Tarkov.Base.Core;

namespace TestMod.Patches
{
    internal class RunFilesCheckingPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return typeof(CommonClientApplication<IBackEndSession2>).GetRuntimeMethods().Where(x=>x.MetadataToken == 0x06007BD5).FirstOrDefault();
        }

        [PatchPrefix]
        private static bool PatchPrefix(ref object __result)
        {
            __result = Task.CompletedTask;
            return false;
        }
    }
}
