using System.Collections.Generic;
using UnityEngine;
using Cards;
using UnityEngine.UI;

public class DiscardPileManager : MonoBehaviour
{
    public List<Card> discardedCards = new List<Card>();

    private DiscardPileVisuals discardVisuals;

    private void Awake()
    {
        discardVisuals = FindAnyObjectByType<DiscardPileVisuals>();
    }

    private void Start()
    {
        discardVisuals.UpdateDiscardVisual();
    }

    public void AddToDiscard(Card card)
    {
        discardedCards.Add(card);
        if (discardedCards.Count > 0)
        {
            discardVisuals.UpdateDiscardVisual();
        }
    }

    public bool HasCards()
    {
        return discardedCards.Count > 0;
    }


    public void ShuffleDiscardIntoDeck()
    {
        if (discardedCards.Count == 0)
        {
            Debug.Log("No cards in discard pile to shuffle back into deck.");
            return;
        }

        foreach (Card card in discardedCards)
        {
            GameManager.Instance.DeckManager.currentDeck.Add(card);
        }
        GameManager.Instance.DeckManager.ShuffleDeck();

        discardedCards.Clear();
        discardVisuals.UpdateDiscardVisual();
    }
}