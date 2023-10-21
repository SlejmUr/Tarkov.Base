using BepInEx;
using HarmonyLib;
using Tarkov.Base.Core;
using TestMod.Patches;

namespace TestMod
{
    [BepInPlugin("TestMod", "TestMod", "0.1")]
    [BepInDependency("TarkovBase","1.0")]
    public class Plugin : BaseUnityPlugin
    {
        private void Awake()
        {
            PatchConstants.GetBackendUrl();

            Harmony harmony = new("TestMod");
            new BEPatches.BattlEyePatchFirstPassUpdate().Enable();
            new BEPatches.BattlEyePatchFirstPassReceivedPacket().Enable();
            new BEPatches.BattlEyePatchFirstPassRun().Enable();
            new BEPatches.BattlEyePatchFirstPassStop().Enable();
            new NetworkGameSessionPatch().Enable();
            new NetworkGameSessionPatch2().Enable();
            Logger.LogInfo($"Plugin TestMod is loaded!");
        }
    }
}