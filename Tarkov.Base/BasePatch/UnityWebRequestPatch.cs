using System.Reflection;
using Tarkov.Base.Core;
using UnityEngine.Networking;

namespace Tarkov.Base.BasePatch
{
    public class UnityWebRequestPatch : ModulePatch
    {
        private static CertificateHandler _certificateHandler = new FakeCertificateHandler();

        protected override MethodBase GetTargetMethod()
        {
            return typeof(UnityWebRequestTexture).GetMethod(nameof(UnityWebRequestTexture.GetTexture), new[] { typeof(string) });
        }

        [PatchPostfix]
        private static void PatchPostfix(UnityWebRequest __result)
        {
            __result.certificateHandler = _certificateHandler;
            __result.disposeCertificateHandlerOnDispose = false;
            __result.timeout = 1000;
        }

        internal class FakeCertificateHandler : CertificateHandler
        {
            public override bool ValidateCertificate(byte[] certificateData)
            {
                return true;
            }
        }
    }
}
