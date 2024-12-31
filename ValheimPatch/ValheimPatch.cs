using BepInEx;
using HarmonyLib;
using UnityEngine;

namespace ValheimPatch
{
    [BepInPlugin(modGUI, modName, modVersion)]
    [BepInProcess("valheim.exe")]
    public class ValheimPatch : BaseUnityPlugin
    {
        private const string modGUI = "Nudriin.ValheimPatch";
        private const string modName = "Patch by nudriin";
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
        }

        [HarmonyPatch(typeof(Character), "Awake")]
        class Character_Patch
        {
            [HarmonyPrefix]
            static void setSpeed(Character __instance)
            {
                var walkSpeed = AccessTools.Field(typeof(Character), "m_walkSpeed");
                var speed = AccessTools.Field(typeof(Character), "m_speed");
                if(walkSpeed != null)
                {
                    walkSpeed.SetValue(__instance, 40f);
                }
                if (speed != null)
                {
                    speed.SetValue(__instance, 40f);
                }
            }
        }
    }
}
