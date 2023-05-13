using Assets.Scripts.UI;
using HarmonyLib;
using JetBrains.Annotations;
using UnityEngine;
using Assets.Scripts.Objects;
using Assets.Scripts.Objects.Items;
using Assets.Scripts.Objects.Entities;

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
            Human parentHuman = null;
            var parentHumanProperty = AccessTools.Property(typeof(Backpack), "ParentHuman");
            parentHuman = parentHumanProperty.GetValue(__instance, null) as Human;

            if (parentHuman != null)
            {
                if (__instance.JetPackActivate)
                {
                    parentHuman.AtmosphereDampeningScale = 0.5f;
                }
                else
                {
                    parentHuman.AtmosphereDampeningScale = Mathf.Clamp(0.5f + (WorldManager.CurrentWorldSetting.Gravity / 100), 0.2f, 0.5f);
                }
            }
        }
    }
}

