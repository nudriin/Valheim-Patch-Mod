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
            static void setCraftingPatch(ref bool __m_craftRequireFire, ref bool __m_craftRequireRoof)
            {
                __m_craftRequireFire = false; // change initial value of m_craftRequireFire to false
                __m_craftRequireRoof = false;
            }
        }

        //m_walkSpeed
        [HarmonyPatch(typeof(Character), "Awake")]
        class Character_Patch
        {
            [HarmonyPrefix]
            static void setWalkSpeed(ref float __m_walkSpeed)
            {
                __m_walkSpeed = 15f; // set walk speed of character
            }
        }

    }


}
