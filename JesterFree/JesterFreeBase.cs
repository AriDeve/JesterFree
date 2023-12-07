
using System.IO;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using LCSoundTool;
using UnityEngine;

namespace JesterFree;

[BepInPlugin(ModGuid, "Jester Free", "1.0.6")]
[BepInDependency("LCSoundTool")]
public class JesterFreeBase : BaseUnityPlugin
{
    private const string ModGuid = "AriDev.JesterFree";
    
    private readonly Harmony _harmony = new(ModGuid);

    private static JesterFreeBase _instance;
    
    internal static AudioClip NewSfx;

    private void Awake()
    {
        if (_instance == null) _instance = this;
        Logger.Log(LogLevel.Info, "Starting Jester Free");

        NewSfx = SoundTool.GetAudioClip(Path.GetDirectoryName(_instance.Info.Location), "freebird.wav");

        _harmony.PatchAll(typeof(JesterAiPatch));
        _harmony.PatchAll(typeof(JesterFreeBase));
        
        Logger.Log(LogLevel.Info, "Jester Free is loaded");
    }
}
