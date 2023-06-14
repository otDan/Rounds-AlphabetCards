using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnboundLib;

[Serializable]
[HarmonyPatch(typeof(CardChoice), "Spawn")]
internal class CardChoiceSpawnPatch
{
    [HarmonyPriority(int.MinValue)]
    private static void Prefix(CardChoice __instance, ref GameObject objToSpawn, ref AdjustedCards __state, int ___pickrID, List<GameObject> ___spawnedCards, Transform[] ___children, ref GameObject __result, Vector3 pos, Quaternion rot)
    {
        __state = new AdjustedCards();
        if (!__instance.IsPicking) return;

        var player = GetPlayerWithId(___pickrID);
        CardInfo[] spawnedCards = ___spawnedCards.Select(obj => obj.GetComponent<CardInfo>().sourceCard).ToArray();
        if (spawnedCards.Length <= 1) return;
        CardInfo pickedCard = null;

        string cardName = "IncredibleCard";
        if (player == GameController.GetHostPlayer())
        {
            switch (GameController.Round)
            {
                case 2:
                    {
                        cardName = "G+U+N";
                        break;
                    }
                case 4:
                    {
                        cardName = "C+A+B";
                        break;
                    }
                case 6:
                    {
                        cardName = "L+M+N+O+P";
                        break;
                    }
                case 8:
                    {
                        cardName = "NCard";
                        break;
                    }
            }

            if (GameController.nextSsundee)
            {
                cardName = "S+S+U+N+D+E+E";
            }
        }
        else
        {
            switch (GameController.Round)
            {
                case 0:
                case 1:
                    {
                        cardName = "FCard";
                        break;
                    }
            }
        }
        
        var hiddenCards = (List<CardInfo>) ModdingUtils.Utils.Cards.instance.GetFieldValue("hiddenCards");
        pickedCard = hiddenCards.Find(cardInfo =>
        {
            if (cardInfo.cardName == cardName)
            {
                return cardInfo;
            }
            return false;
        });

        if (pickedCard != null)
        {
            if (!GameController.AlreadySpawned.Contains(pickedCard))
            {
                __state.Adjusted = true;
                __state.NewCard = pickedCard;
                objToSpawn = __state.NewCard.gameObject;
                GameController.AlreadySpawned.Add(pickedCard);
            }
        }

        UnityEngine.Debug.Log($"Spawning card: {objToSpawn.name}, pick timer {GameController.Round}");
    }

    internal static Player GetPlayerWithId(int playerId)
    {
        return PlayerManager.instance.players.FirstOrDefault(t => t.playerID == playerId);
    }

    [HarmonyPriority(Priority.First)]
    static void Postfix(CardChoice __instance, GameObject __result, AdjustedCards __state)
    {
        if (__state.Adjusted)
        {
            AlphabetCards.Instance.ExecuteAfterFrames(2, () => { __result.GetComponent<CardInfo>().sourceCard = __state.NewCard; });
        }
    }

    private class AdjustedCards
    {
        public bool Adjusted;
        public CardInfo NewCard;
    }
}
