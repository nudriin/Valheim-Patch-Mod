﻿using BepInEx;
using HarmonyLib;
using UnityEngine;

namespace ValheimPatch
{
    [BepInPlugin(modGUI, modName, modVersion)]
    [BepInProcess("valheim.exe")]
    public class ValheimPatch : BaseUnityPlugin
    {
        private const string modGUI = "Nudriin.ValheimPatch";
        private const string modName = "Valheim patch by nudriin";
        private const string modVersion = "1.0.0";

        private readonly Harmony harmony = new Harmony(modGUI);

        void Awake()
        {
            harmony.PatchAll();
        }

        // patch CraftStation class on valheim
        // and patch in "Start" function
        [HarmonyPatch(typeof(CraftingStation), "Start")]
        class Crafting_Patch
        {
            //public bool m_craftRequireRoof = true;
            //public bool m_craftRequireFire = true;

            // reference the original valheim m_craftRequireRoof and m_craftRequireFire properties
            // to be changed in BepInEx
            [HarmonyPrefix]
            static void setCraftingPatch(CraftingStation __instance)
            {
                // Gunakan refleksi untuk mengubah properti field m_craftRequireRoof dan m_craftRequireFire
                var roofField = AccessTools.Field(typeof(CraftingStation), "m_craftRequireRoof");
                var fireField = AccessTools.Field(typeof(CraftingStation), "m_craftRequireFire");

                if (roofField != null)
                {
                    roofField.SetValue(__instance, false); // Ubah nilai m_craftRequireRoof menjadi false
                }

                if (fireField != null)
                {
                    fireField.SetValue(__instance, false); // Ubah nilai m_craftRequireFire menjadi false
                }

                Debug.Log("Patched CraftingStation: m_craftRequireRoof and m_craftRequireFire set to false");
            }

            [HarmonyPatch(typeof(Player), "Awake")]
            class Player_Patch
            {
                [HarmonyPrefix]
                static void setStamina(Player __instance)
                {
                    var staminaRegen = AccessTools.Field(typeof(Player), "m_staminaRegen");
                    var staminaRegenDelay = AccessTools.Field(typeof(Player), "m_staminaRegenDelay");
                    var staminaRunDrain = AccessTools.Field(typeof(Player), "m_runStaminaDrain");

                    if (staminaRegen != null)
                    {
                        staminaRegen.SetValue(__instance, 200f);
                    }

                    if (staminaRegenDelay != null)
                    {
                        staminaRegenDelay.SetValue(__instance, 0f);
                    }

                    if (staminaRunDrain != null)
                    {
                        staminaRunDrain.SetValue(__instance, 0f);
                    }
                }
            }
        }
    }
}
