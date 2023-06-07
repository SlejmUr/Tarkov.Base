using BepInEx;
using Comfort.Common;
using EFT;
using Tarkov.Base.BasePatch;
using Tarkov.Base.BasePatch.NonSecure;
using UnityEngine.SceneManagement;

namespace Tarkov.Base
{
    [BepInPlugin("TarkovBase", "TarkovBase", "1.0")]
    public class Plugin : BaseUnityPlugin
    {
        public static Plugin Instance;
        public static GameWorld gameWorld { get; private set; }
        private void Awake()
        {
            CorePatch();
            NonSercurePatch();

            Logger.LogInfo($"Plugin TarkovBase is loaded!");
            SceneManager.sceneLoaded += SceneManager_sceneLoaded;
            Instance = this;
        }

        private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            gameWorld = Singleton<GameWorld>.Instance;
        }

        private void CorePatch()
        {
            var enabled = Config.Bind<bool>("Core Patches", "Enable", true, "Core patches without running tarkov will fail");
            if (!enabled.Value)
            {
                Logger.LogInfo("Core Patches has been disabled! Ignoring Patches.");
                return;
            }

            new ConsistencySinglePatch().Enable();
            new ConsistencyMultiPatch().Enable();
            new BattlEyePatch().Enable();
            new SslCertificatePatch().Enable();
            new UnityWebRequestPatch().Enable();
        }

        private void NonSercurePatch()
        {
            var enabled = Config.Bind<bool>("Secure Patches", "Enable", true, "It enable or disable to HTTPS to be HTTP and WSS to be WS");
            if (!enabled.Value)
            {
                Logger.LogInfo("Secure Patches has been disabled! Ignoring Patches.");
                return;
            }

            new TransportPrefixPatch().Enable();
            new WebSocketPatch().Enable();
        }
    }
}