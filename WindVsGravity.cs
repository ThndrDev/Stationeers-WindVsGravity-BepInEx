using System;
using BepInEx;
using HarmonyLib;
using UnityEngine;

namespace WindVsGravity.Scripts
{
    [BepInPlugin("net.ThndrDev.stationeers.WindVsGravity.Scripts", "Wind Vs Gravity", "0.1.2.0")]   
    public class WindVsGravityPlugin : BaseUnityPlugin
    {
        public static WindVsGravityPlugin Instance;


        public void Log(string line)
        {
            Debug.Log("[WindVsGravity]: " + line);
        }

        void Awake()
        {
            WindVsGravityPlugin.Instance = this;
            Log("Hello World");

            try
            {
                // Harmony.DEBUG = true;
                var harmony = new Harmony("net.ThndrDev.stationeers.WindVsGravity.Scripts");
                harmony.PatchAll();
                Log("Patch succeeded");
            }
            catch (Exception e)
            {
                Log("Patch Failed");
                Log(e.ToString());
            }
        }
    }
}