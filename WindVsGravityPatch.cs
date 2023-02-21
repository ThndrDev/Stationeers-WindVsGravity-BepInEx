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
                Debug.Log($"World Gravity: {WorldManager.CurrentWorldSetting.Gravity}");
                Debug.Log($"Character's name:{__instance.DisplayName}  Original AtmosphereDampeningScale Value: {__instance.AtmosphereDampeningScale} ");
                __instance.AtmosphereDampeningScale = Mathf.Clamp(0.5f + (WorldManager.CurrentWorldSetting.Gravity / 100), 0.2f, 0.5f);
                Debug.Log($"Character's name:{__instance.DisplayName}  Patched AtmosphereDampeningScale Value: {__instance.AtmosphereDampeningScale} ");
            }
            else
            {
            Debug.Log($"Thing Name:{__instance.DisplayName}  Original AtmosphereDampeningScale Value: {__instance.AtmosphereDampeningScale} ");
            __instance.AtmosphereDampeningScale = Mathf.Clamp(__instance.AtmosphereDampeningScale + (WorldManager.CurrentWorldSetting.Gravity / 50), 0.1f, 1f);
            Debug.Log($"Thing Name:{__instance.DisplayName}  New AtmosphereDampeningScale Value: {__instance.AtmosphereDampeningScale} ");
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
                    Debug.Log($"Jetpack Activated. Human Name:{__instance.ParentHuman.DisplayName}  New AtmosphereDampeningScale Value: {__instance.ParentHuman.AtmosphereDampeningScale} ");
                }
                else
                {
                    __instance.ParentHuman.AtmosphereDampeningScale = Mathf.Clamp(0.5f + (WorldManager.CurrentWorldSetting.Gravity / 100), 0.2f, 0.5f);
                    Debug.Log($"Jetpack Deactivated. Human Name:{__instance.ParentHuman.DisplayName}  New AtmosphereDampeningScale Value: {__instance.ParentHuman.AtmosphereDampeningScale} ");
                }
            }
        }
    }
}
