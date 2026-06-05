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

        discardVisuals.UpdateDiscardVisual();
    }

    public bool HasCards()
    {
        return discardedCards.Count > 0;
    }

    public List<Card> GetDiscardedCards()
    {
        return new List<Card>(discardedCards);
    }

    public void ShuffleDiscardIntoDeck()
    {
        if (discardedCards.Count == 0)
        {
            Debug.Log("No cards in discard pile to shuffle back into deck.");
            return;
        }

        GameManager.Instance.DeckManager.AddCardsToDeck(discardedCards);

        discardedCards.Clear();
        discardVisuals.UpdateDiscardVisual();
        Debug.Log("Discard pile shuffled into deck");
    }
}