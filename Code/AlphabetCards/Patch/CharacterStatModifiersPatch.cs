using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[HarmonyPatch(typeof(CharacterStatModifiers))]
class CharacterStatModifiersPatch
{
    [HarmonyPostfix]
    [HarmonyPriority(Priority.Last)]
    [HarmonyPatch("ConfigureMassAndSize")]
    static void ApplySizeAdjustment(CharacterStatModifiers __instance)
    {
        Transform playerTransform = __instance.gameObject.transform;

        switch (__instance.GetAlphabetData().sizeMod)
        {
            case AlphabetConstants.ModType.sizeNormalize:
                playerTransform.localScale = Vector3.one * 1.2f;
                break;
        }
    }
}