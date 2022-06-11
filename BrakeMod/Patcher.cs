using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Harmony;
using UnityEngine;

namespace BrakeMod
{
    [HarmonyPatch(typeof(Lirp.SnowboardController), "Show")]
    class SnowboardControllerPatcher
    {
        [HarmonyPostfix]
        public static void Postfix(System.Reflection.MethodBase __originalMethod, Lirp.SnowboardController __instance)
        {
            try
            {
                if (__instance == ModManager.userSession.sc && !ModManager.gameplayRider)
                {
                    ModManager.gameplayRider = __instance.gameObject;
                    ModManager.snowboardConfig = ModManager.gameplayRider.GetComponent<Lirp.SnowboardConfig>();
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
