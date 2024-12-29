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
    }
}
