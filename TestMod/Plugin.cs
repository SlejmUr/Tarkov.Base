using BepInEx;

namespace TestMod
{
    [BepInPlugin("TestMod", "TestMod", "0.1")]
    [BepInDependency("TarkovBase","0.1")]
    public class Plugin : BaseUnityPlugin
    {
        private void Start()
        {
            Logger.LogMessage("say hi to the camera!");
        }
        private void Awake()
        {
            Tarkov.Base.Plugin.SceneLoadActions.Add(Test);
        }

        private void Test()
        {
            Logger.LogMessage("from the TestMod!");
        }
    }
}