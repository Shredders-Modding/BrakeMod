using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lirp;
using MelonLoader;
using UnhollowerRuntimeLib;
using UnityEngine;

namespace BrakeMod
{
    public class ModManager : MelonMod
    {
        public static bool isDebugActivated;
        public static UserSession userSession;
        public static ModManager instance;
        public static GameObject gameplayRider;
        public static SnowboardConfig snowboardConfig;
        private AssetManager assetManager;
        private static PhysicsBrake physicsBrake;

        public static Vector2 initCourseBrakeValue;
        public static Vector2 initPowderBrakeValue;

        public static float courseBrakeValue;
        public static float powderBrakeValue;

        public static MelonPreferences_Category brakePrefCategory;
        public static MelonPreferences_Entry<float> courseBrakePref;
        public static MelonPreferences_Entry<float> powderBrakePref;

        public override void OnApplicationStart()
        {
            ClassInjector.RegisterTypeInIl2Cpp<MenuBuilder>();

            instance = this;
            isDebugActivated = false;

            assetManager = new AssetManager();
            assetManager.Init();

            brakePrefCategory = MelonPreferences.CreateCategory("brakePrefCategory");
            courseBrakePref = brakePrefCategory.CreateEntry("courseBrakePref", 1f);
            powderBrakePref = brakePrefCategory.CreateEntry("powderBrakePref", 1f);
            courseBrakeValue = courseBrakePref.Value;
            powderBrakeValue = powderBrakePref.Value;
        }

        public override void OnLateUpdate()
        {
            if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Z))
            {
                if (assetManager.instantiatedMenu.active)
                    assetManager.instantiatedMenu.SetActive(false);
                else
                    assetManager.instantiatedMenu.SetActive(true);
            }
        }

        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            if (sceneName == "Loader")
            {
                assetManager.CreateMenu();
                MelonLogger.Msg("Brake mod initialized.");
            }

            if (sceneName == "GameBase")
            {
                ModLogger.Log("Try get UserSession");
                userSession = GameObject.Find("UserSession").GetComponent<UserSession>();
                ModLogger.Log("UserSession found");
            }
        }

        public static void GetPhysicsBrake()
        {
            for (int i = 0; i < snowboardConfig.configurableObjects.Count; i++)
            {
                PhysicsBrake _physicsBrake = snowboardConfig.configurableObjects[i].TryCast<PhysicsBrake>();
                if (_physicsBrake != null)
                {
                    ModLogger.Log("Brake found");
                    physicsBrake = _physicsBrake;
                    InitBrakeValue();
                }
            }
        }

        public static void InitBrakeValue()
        {
            initCourseBrakeValue = physicsBrake.MinMaxBrake;
            initPowderBrakeValue = physicsBrake.MinMaxDepthBrake;
            ModLogger.Log($"initCourseBrakeValue = {initCourseBrakeValue.y} and initPowderBrakeValue = {initPowderBrakeValue.y}");
            SetBrakeValue(courseBrakeValue, powderBrakeValue);
        }

        public static void SetBrakeValue(float in_courseBrake, float in_powderBrake)
        {
            courseBrakeValue = in_courseBrake;
            powderBrakeValue = in_powderBrake;
            courseBrakePref.Value = courseBrakeValue;
            powderBrakePref.Value = powderBrakeValue;
            physicsBrake.MinMaxBrake = new Vector2(initCourseBrakeValue.x, courseBrakeValue);
            physicsBrake.MinMaxDepthBrake = new Vector2(initPowderBrakeValue.x, powderBrakeValue - courseBrakeValue);
        }
    }
}
