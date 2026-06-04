using System.Collections.Generic;
using UnityEngine;
using Cards;
using UnityEngine.UI;

public class DiscardPileManager : MonoBehaviour
{
    public List<Card> discardedCards = new List<Card>();

    public Image discardImage;

    private void Start()
    {
        UpdateDiscardVisual();
    }
    public void AddToDiscard(Card card)
    {
        discardedCards.Add(card);

        UpdateDiscardVisual();
    }

    public bool HasCards()
    {
        return discardedCards.Count > 0;
    }

    public void UpdateDiscardVisual()
    {
        discardImage.enabled = discardedCards.Count > 0;    //alternativa u DeckMangeru
    }

}