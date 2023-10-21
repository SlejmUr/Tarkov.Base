using EFT;
using System.Reflection;
using Tarkov.Base.Core;

namespace TestMod.Patches
{
    public class NetworkGameSessionPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return typeof(AbstractGameSession).Assembly.GetType("EFT.NetworkGameSession", true)
               .GetMethod("method_9", BindingFlags.NonPublic | BindingFlags.Instance);
        }

        [PatchPrefix]
        private static bool Prefix()
        {
            return false;
        }
    }

    public class NetworkGameSessionPatch2 : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return typeof(AbstractGameSession).Assembly.GetType("EFT.NetworkGameSession", true)
               .GetMethod("method_10", BindingFlags.NonPublic | BindingFlags.Instance);
        }

        [PatchPrefix]
        private static bool Prefix()
        {
            return false;
        }
    }
}
