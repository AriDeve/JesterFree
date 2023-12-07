using HarmonyLib;
using UnityEngine;

namespace JesterFree;

[HarmonyPatch(typeof(JesterAI))]
public class JesterAiPatch
{
    [HarmonyPatch(nameof(JesterAI.Start))]
    [HarmonyPrefix]
    public static void FreeJesterPatch(ref AudioClip ___screamingSFX)
    {
        ___screamingSFX = JesterFreeBase.NewSfx;
    }
}