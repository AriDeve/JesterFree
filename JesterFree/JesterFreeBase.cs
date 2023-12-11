using System.IO;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using LCSoundTool;
using UnityEngine;

namespace JesterFree;

[BepInPlugin(ModGuid, "Jester Free", "1.0.6")]
[BepInDependency("LCSoundTool")]
public class JesterFreeBase : BaseUnityPlugin {
    private const string ModGuid = "AriDev.JesterFree";

    public static JesterFreeBase Instance { get; private set; }

    private readonly Harmony _harmony = new(ModGuid);

    public AudioClip Intro { get; private set; }
    public AudioClip Solo { get; private set; }
    
    public ConfigEntry<bool> IntroEnabled { get; private set; }
    public ConfigEntry<int> IntroVolume { get; private set; }
    public ConfigEntry<bool> SoloEnabled { get; private set; }
    public ConfigEntry<int> SoloVolume { get; private set; }

    private void Awake() {
        if (Instance != null) {
            Logger.Log(LogLevel.Info, "Jester Free instance already running");
            return;
        }
        Instance = this;
        
        var isEnabled = Config.Bind("Mod", "EnableMod", true, "Enables the mod, otherwise doesn't load it");
        IntroEnabled = Config.Bind("Sound", "EnableCrankingIntro", true, "Enables the cranking to be replaced by Free Bird intro");
        IntroVolume = Config.Bind("Sound", "IntroVolume", 50, new ConfigDescription("Sets the volume of the cranking intro (in %)", new AcceptableValueRange<int>(0, 200)));
        SoloEnabled = Config.Bind("Sound", "EnableScreamSolo", true, "Enables the scream to be replaced by Free Bird solo");
        SoloVolume = Config.Bind("Sound", "SoloVolume", 100, new ConfigDescription("Sets the volume of the screaming solo (in %)", new AcceptableValueRange<int>(0, 200)));
        
        if (!isEnabled.Value) {
            Logger.Log(LogLevel.Info, "Jester Free disabled in config");
            return;
        }
        
        Logger.Log(LogLevel.Info, "Starting Jester Free");

        Intro = SoundTool.GetAudioClip(Path.GetDirectoryName(Info.Location), "intro.wav");
        Solo = SoundTool.GetAudioClip(Path.GetDirectoryName(Info.Location), "solo.wav");

        _harmony.PatchAll(typeof(JesterAiPatch));
        _harmony.PatchAll(typeof(JesterFreeBase));
        
        Logger.Log(LogLevel.Info, "Jester Free is loaded");
    }
}
