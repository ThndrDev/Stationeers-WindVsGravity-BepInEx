using Assets.Scripts.UI;
using HarmonyLib;
using JetBrains.Annotations;
using UnityEngine;
using Assets.Scripts.Objects;
using Assets.Scripts.Objects.Items;


namespace WindVsGravity.Scripts
{
    [HarmonyPatch(typeof(DynamicThing))]
    public static class DynamicThingAtmosphereDampeningScalePatch
    {
        [HarmonyPatch("Awake")]
        [HarmonyPostfix]
        [UsedImplicitly]
        static private void AtmosphereDampeningScalePatch(DynamicThing __instance)
        {
            if (__instance is Entity)
            {
                __instance.AtmosphereDampeningScale = Mathf.Clamp(0.5f + (WorldManager.CurrentWorldSetting.Gravity / 100), 0.2f, 0.5f);
            }
            else
            {
            __instance.AtmosphereDampeningScale = Mathf.Clamp(__instance.AtmosphereDampeningScale + (WorldManager.CurrentWorldSetting.Gravity / 60), 0.3f, 1f);
            }
        }
    }

    [HarmonyPatch(typeof(Jetpack))]
    public static class JetpackAtmosphereDampeningScalePatch
    {
        [HarmonyPatch("set_JetPackActivate")]
        [HarmonyPostfix]
        [UsedImplicitly]
        static private void JetpackPatch(Jetpack __instance)
        {
            if (__instance.ParentHuman != null)
            {
                if (__instance.JetPackActivate)
                {
                    __instance.ParentHuman.AtmosphereDampeningScale = 0.5f;
                }
                else
                {
                    __instance.ParentHuman.AtmosphereDampeningScale = Mathf.Clamp(0.5f + (WorldManager.CurrentWorldSetting.Gravity / 100), 0.2f, 0.5f);
                }
            }
        }
    }
}
