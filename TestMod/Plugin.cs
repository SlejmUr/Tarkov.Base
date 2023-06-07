using BepInEx;
using HarmonyLib;
using Tarkov.Base.Core;

namespace TestMod
{
    [BepInPlugin("TestMod", "TestMod", "0.1")]
    [BepInDependency("TarkovBase","1.0")]
    public class Plugin : BaseUnityPlugin
    {
        private void Awake()
        {
            PatchConstants.GetBackendUrl();

            Harmony harmony = new("MTGA.Coop");
            new Patches.RunFilesCheckingPatch().Enable();
            new Patches._x06007CE3_Patch().Enable();
            new Patches._x06008188_Patch().Enable();
            new Patches._x060081E1_Patch().Enable();
            harmony.PatchAll(typeof(Patches.LogMultiPatches));
            /*
            try
            {
                harmony.PatchAll(typeof(Patches.DeserializeMultiPatch));
            }
            catch (System.Exception ex)
            {
                Logger.LogInfo(ex);
            }
            try
            {
                harmony.PatchAll(typeof(Patches.SerializeMultiPatch));
            }
            catch (System.Exception ex)
            {
                Logger.LogInfo(ex);
            }
            try
            {
                harmony.PatchAll(typeof(Patches.TarkovAppMethodNamesPrint));
            }
            catch (System.Exception ex)
            {
                Logger.LogInfo(ex);
            }
            */
            // Plugin startup logic
            Logger.LogInfo($"Plugin TestMod is loaded!");
        }
    }
}