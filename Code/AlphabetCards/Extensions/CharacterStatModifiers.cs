using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using HarmonyLib;
using CardChoiceSpawnUniqueCardPatch.CustomCategories;


public static class AlphabetConstants
{
    public enum ModType
    {
        disabled = -2,
        none = -1,

        sizeNormalize = 20
    }
}

public class CharacterStatModifiersAlphabetData
{
    public AlphabetConstants.ModType sizeMod;

    public float t_uniqueMagickCooldown;

    public CharacterStatModifiersAlphabetData()
    {
        sizeMod = AlphabetConstants.ModType.none;
    }
}

public static class CharacterStatModifiersAlphabetDataExtensions
{
    public static readonly ConditionalWeakTable<CharacterStatModifiers, CharacterStatModifiersAlphabetData> data =
        new ConditionalWeakTable<CharacterStatModifiers, CharacterStatModifiersAlphabetData>();

    public static CharacterStatModifiersAlphabetData GetAlphabetData(this CharacterStatModifiers characterStat)
    {
        return data.GetOrCreateValue(characterStat);
    }

    public static void AddData(this CharacterStatModifiers characterStat, CharacterStatModifiersAlphabetData value)
    {
        try
        {
            data.Add(characterStat, value);
        }
        catch (Exception) { }
    }
}

[HarmonyPatch(typeof(CharacterStatModifiers), "ResetStats")]
class CharacterStatModifiersPatchResetStats
{
    private static void Prefix(CharacterStatModifiers __instance)
    {
        __instance.GetAlphabetData().sizeMod = AlphabetConstants.ModType.none;
    }
}
