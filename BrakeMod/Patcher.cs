using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using UnityEngine;
using Il2CppLirp;

namespace BrakeMod
{
    [HarmonyPatch(typeof(SnowboardController), "Show")]
    internal class SnowboardControllerPatcher
    {
        [HarmonyPostfix]
        public static void Postfix(System.Reflection.MethodBase __originalMethod, SnowboardController __instance)
        {
            try
            {
                if (__instance == ModManager.userSession.sc && !ModManager.gameplayRider)
                {
                    ModManager.gameplayRider = __instance.gameObject;
                    ModManager.snowboardConfig = ModManager.gameplayRider.GetComponent<SnowboardConfig>();
                    ModManager.GetPhysicsBrake();
                }
            }
            catch (System.Exception ex)
            {
                //MelonLogger.Msg($"Exception in patch of SnowboardSounds.OnLand:\n{ex}");
            }
        }
    }
}
