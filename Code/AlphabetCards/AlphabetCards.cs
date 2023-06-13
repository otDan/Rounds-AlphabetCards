using BepInEx;
using HarmonyLib;
using System;
using UnboundLib;
using UnboundLib.GameModes;

[BepInDependency("com.willis.rounds.unbound")]
[BepInDependency("pykess.rounds.plugins.moddingutils")]
[BepInDependency("io.olavim.rounds.rwf")]
[BepInDependency("root.rarity.lib", BepInDependency.DependencyFlags.HardDependency)]
[BepInDependency("root.cardtheme.lib", BepInDependency.DependencyFlags.HardDependency)]
[BepInPlugin(ModId, CompatibilityModName, Version)]
[BepInProcess("Rounds.exe")]
public class AlphabetCards : BaseUnityPlugin
{
    private const string ModId = "ot.dan.rounds.alphabetcards";
    private const string ModName = "Alphabet Cards";
    public const string Version = "1.0.0";
    public const string ModInitials = "Alphabet";
    private const string CompatibilityModName = "AlphabetCards";
    
    public static AlphabetCards Instance { get; private set; }

    internal void Awake()
    {
        Instance = this;

        var harmony = new Harmony(ModId);
        harmony.PatchAll();

        AssetManager.assets = Jotunn.Utils.AssetUtils.LoadAssetBundleFromResources("alphabetcards", typeof(AlphabetCards).Assembly);
        var cards = AssetManager.Cards;
        Log("Cards: " + cards);
        var cardHolder = cards.GetComponent<CardHolder>();
        Log("CardHolder: " + cardHolder);
        cardHolder.RegisterCards();
    }

    internal void Start()
    {
        GameModeManager.AddHook(GameModeHooks.HookGameStart, GameController.GameStart);
    }

    public void Log(string debug)
    {
        Logger.LogInfo(debug);
    }
}
