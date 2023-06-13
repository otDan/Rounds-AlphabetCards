using HarmonyLib;
using System;
using UnityEngine;

[Serializable]
[HarmonyPatch(typeof(CardChoice), "StartPick")]
internal class CardChoiceStartPickPatch
{
    [HarmonyPriority(int.MinValue)]
    private static void Prefix(int picksToSet, int pickerIDToSet)
    {
        if (pickerIDToSet == GameController.GetHostPlayerId())
        {
            GameController.Round++;
        }
    }
}