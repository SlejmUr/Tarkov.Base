using EFT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using Tarkov.Base.Core;

namespace TestMod.Patches
{
    internal class _x06008188_Patch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return typeof(LocalBackendEvent).GetNestedTypes().Where(x => x.MetadataToken == 0x020014E2).FirstOrDefault().GetRuntimeMethods().Where(x=>x.MetadataToken == 0x06008188).FirstOrDefault();
        }

        [PatchPrefix]
        static void PatchPrefix()
        {
            System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
            System.IO.File.AppendAllText("_x06008188_Patch.txt", t.ToString());
        }
    }

    internal class _x06007CE3_Patch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return typeof(TarkovApplication).GetNestedTypes(BindingFlags.NonPublic).Where(x=>x.MetadataToken == 0x02001404).FirstOrDefault().GetRuntimeMethods().Where(x => x.MetadataToken == 0x06007CE3).FirstOrDefault();
        }

        [PatchPrefix]
        static void PatchPrefix()
        {
            System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
            System.IO.File.AppendAllText("_x06007CE3_Patch.txt", t.ToString());
        }
    }

    internal class _x060081E1_Patch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return typeof(LocalBackendEvent).GetRuntimeMethods().Where(x => x.MetadataToken == 0x060081E1).FirstOrDefault();
        }

        [PatchPrefix]
        static void PatchPrefix()
        {
            System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
            System.IO.File.AppendAllText("_x060081E1_Patch.txt", t.ToString());
        }
    }
}
