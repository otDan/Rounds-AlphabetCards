using System.Collections.Generic;
using UnboundLib.Cards;
using UnityEngine;

public class CardHolder: MonoBehaviour 
{
    public List<GameObject> Cards;
    public List<GameObject> HiddenCards;

    internal void RegisterCards()
    {
        var mod = AlphabetCards.ModInitials;
        foreach(var Card in Cards)
        {
            CustomCard.RegisterUnityCard(Card, mod, Card.GetComponent<CardInfo>().cardName, true, null);
        }

        foreach(var Card in HiddenCards)
        {
            //CustomCard.RegisterUnityCard(Card, mod, Card.GetComponent<CardInfo>().cardName, true, null);
            CustomCard.RegisterUnityCard(Card, mod, Card.GetComponent<CardInfo>().cardName, false, null);
            ModdingUtils.Utils.Cards.instance.AddHiddenCard(Card.GetComponent<CardInfo>());
        }
    }
}