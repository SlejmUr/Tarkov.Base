﻿using FilesChecker;
using System;
using System.Linq;
using System.Reflection;

namespace Tarkov.Base.Core
{
    public class Constants
    {
        public static Constants _instance;
        public static Constants Instance { get { _instance ??= new Constants(); return _instance; } }
        public Constants()
        {
            DefaultBindingFlags = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly;
            PublicInstanceFlag = BindingFlags.Public | BindingFlags.Instance;
            NonPublicInstanceFlag = BindingFlags.NonPublic | BindingFlags.Instance;
            NonPublicInstanceDeclaredOnlyFlag = BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly;
            NonPublicFlag = BindingFlags.NonPublic;

            TargetAssembly = typeof(ActionTrigger).Assembly;
            FileCheckerAssembly = typeof(ICheckResult).Assembly;
            MainApplicationType = typeof(EFT.TarkovApplication);
            UnityCertificateHandlerType = typeof(UnityEngine.Networking.CertificateHandler);
            UnityUnityWebRequestType = typeof(UnityEngine.Networking.UnityWebRequestTexture);
            TargetAssemblyTypes = TargetAssembly.GetTypes();
            FileCheckerAssemblyTypes = FileCheckerAssembly.GetTypes();

            LocalGameType = TargetAssemblyTypes.Single(x => x.Name == "LocalGame");
            ExfilPointManagerType = TargetAssemblyTypes.Single(x => x.GetMethod("InitAllExfiltrationPoints") != null);
            MenuControllerType = TargetAssemblyTypes.Single(x => x.Name.StartsWith("GClass") && x.GetField("menuUI_0", BindingFlags.NonPublic | BindingFlags.Instance) != null);
            BackendInterfaceType = TargetAssemblyTypes.Single(
                x => x.GetMethods().Select(y => y.Name).Contains("CreateClientSession") && x.IsInterface);
            SessionInterfaceType = TargetAssemblyTypes.Single(
                x => x.GetMethods().Select(y => y.Name).Contains("GetPhpSessionId") && x.IsInterface);
            ProfileInfoType = TargetAssemblyTypes.Single(x => x.GetMethod("GetExperience") != null);
            ProfileType = TargetAssemblyTypes.Single(x => x.GetMethod("AddToCarriedQuestItems") != null);
            FenceTraderInfoType = TargetAssemblyTypes.Single(x => x.GetMethod("NewExfiltrationPrice") != null);
            FirearmControllerType = typeof(EFT.Player.FirearmController)
                .GetNestedTypes()
                .Single(x => x.GetFields(DefaultBindingFlags).Count(y => y.Name.Contains("gclass")) > 0 && x.GetFields(DefaultBindingFlags).Count(y => y.Name.Contains("callback")) > 0 && x.GetMethod("UseSecondMagForReload", DefaultBindingFlags) != null);
            WeaponControllerFieldName = FirearmControllerType
                .GetFields(DefaultBindingFlags)
                .Single(x => x.Name.Contains("gclass")).Name;


        }

        #region Binding flags
        public BindingFlags DefaultBindingFlags;
        public BindingFlags PublicInstanceFlag;
        public BindingFlags NonPublicInstanceFlag;
        public BindingFlags NonPublicInstanceDeclaredOnlyFlag;
        public BindingFlags NonPublicFlag;
        #endregion

        public Assembly TargetAssembly;
        public Assembly FileCheckerAssembly;
        public Type[] TargetAssemblyTypes;
        public Type[] FileCheckerAssemblyTypes;
        public Type MainApplicationType;
        public Type LocalGameType;
        public Type ExfilPointManagerType;
        public Type UnityCertificateHandlerType;
        public Type UnityUnityWebRequestType;
        public Type MenuControllerType;
        public Type BackendInterfaceType;
        public Type SessionInterfaceType;
        public Type ProfileInfoType;
        public Type ProfileType;
        public Type FenceTraderInfoType;
        public Type FirearmControllerType;
        public string WeaponControllerFieldName;
    }
}
