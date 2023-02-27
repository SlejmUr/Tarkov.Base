using BepInEx;
using Comfort.Common;
using EFT;
using System;
using System.Collections.Generic;
using Tarkov.Base.BasePatch;
using UnityEngine.SceneManagement;

namespace Tarkov.Base
{
    [BepInPlugin("TarkovBase", "TarkovBase", "0.1")]
    public class Plugin : BaseUnityPlugin
    {
        public static Plugin Instance;
        public static GameWorld gameWorld { get; private set; }
        public static List<Action> SceneLoadActions = new();
        public static List<Action> AwakeBeforeScene = new();
        private void Awake()
        {
            CorePatch();
            NonSercurePatch();
            foreach (Action action in AwakeBeforeScene)
            {
                try
                {
                    action();
                }
                catch (Exception ex)
                {
                    Logger.LogError($"{GetType().Name}: {ex}");
                    throw;
                }
            }
            Logger.LogInfo($"Plugin TarkovBase is loaded!");
            SceneManager.sceneLoaded += SceneManager_sceneLoaded;
            Instance = this;
        }

        private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            gameWorld = Singleton<GameWorld>.Instance;
            foreach (Action action in SceneLoadActions)
            {
                try
                {
                    action();
                }
                catch (Exception ex)
                {
                    Logger.LogError($"{GetType().Name}: {ex}");
                    throw;
                }
            }
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

            new ConsistencySinglePatch().Enable();
            new ConsistencyMultiPatch().Enable();
            new BattlEyePatch().Enable();
            new SslCertificatePatch().Enable();
            new UnityWebRequestPatch().Enable();
        }
    }
}