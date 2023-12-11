using System.Diagnostics.CodeAnalysis;
using HarmonyLib;
using UnityEngine;

namespace JesterFree;

[HarmonyPatch(typeof(JesterAI))]
public static class JesterAiPatch {
    [HarmonyPatch(nameof(JesterAI.Start))]
    [HarmonyPrefix]
    public static void FreeJesterPatch(
        [SuppressMessage("ReSharper", "InconsistentNaming")] ref AudioClip ___popGoesTheWeaselTheme,
        [SuppressMessage("ReSharper", "InconsistentNaming")] ref AudioClip ___screamingSFX,
        [SuppressMessage("ReSharper", "InconsistentNaming")] ref AudioSource ___farAudio,
        [SuppressMessage("ReSharper", "InconsistentNaming")] ref AudioSource ___creatureVoice
        ) {
        if (JesterFreeBase.Instance.IntroEnabled.Value) {
            ___popGoesTheWeaselTheme = JesterFreeBase.Instance.Intro;
            ___farAudio.volume = JesterFreeBase.Instance.IntroVolume.Value / 100.0f;
        }
        if (JesterFreeBase.Instance.SoloEnabled.Value) {
            ___screamingSFX = JesterFreeBase.Instance.Solo;
            ___creatureVoice.volume = JesterFreeBase.Instance.SoloVolume.Value / 100.0f;
        }
    }
}