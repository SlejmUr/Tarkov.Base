using System.Reflection;
using Tarkov.Base.Core;

namespace TestMod.Patches
{
    public class BEPatches
    {
        internal class BattlEyePatchFirstPassRun : ModulePatch
        {
            protected override MethodBase GetTargetMethod()
            {
                return PatchConstants.GetMethodForType(typeof(BattlEye.BEClient), "Run", false);
            }

            [PatchPrefix]
            private static bool PatchPrefix()
            {
                return false;
            }
        }

        internal class BattlEyePatchFirstPassUpdate : ModulePatch
        {
            protected override MethodBase GetTargetMethod()
            {
                return PatchConstants.GetMethodForType(typeof(BattlEye.BEClient), "Update", false);
            }

            [PatchPrefix]
            private static bool PatchPrefix()
            {
                return false;
            }
        }

        internal class BattlEyePatchFirstPassReceivedPacket : ModulePatch
        {
            protected override MethodBase GetTargetMethod()
            {
                return PatchConstants.GetMethodForType(typeof(BattlEye.BEClient), "ReceivedPacket", false);
            }

            [PatchPrefix]
            private static bool PatchPrefix()
            {
                return false;
            }
        }

        internal class BattlEyePatchFirstPassStop : ModulePatch
        {
            protected override MethodBase GetTargetMethod()
            {
                return PatchConstants.GetMethodForType(typeof(BattlEye.BEClient), "Stop", false);
            }

            [PatchPrefix]
            private static bool PatchPrefix()
            {
                return false;
            }
        }
    }
}
