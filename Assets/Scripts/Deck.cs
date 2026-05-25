using System.Collections.Generic;
using UnityEngine;
using Cards;

public class Deck : MonoBehaviour
{
    [Header("All Card Assets")]
    public List<Card> allCards = new List<Card>();

    private List<Card> currentDeck = new List<Card>();

    private void Start()
    {
        BuildDeck();
        ShuffleDeck();
    }

    public void BuildDeck()
    {
        currentDeck.Clear();

        foreach (Card card in allCards)
        {
            currentDeck.Add(card);
        }
    }

    public void ShuffleDeck()
    {
        for (int i = 0; i < currentDeck.Count; i++)
        {
            int randomIndex = Random.Range(i, currentDeck.Count);

            Card temp = currentDeck[i];
            currentDeck[i] = currentDeck[randomIndex];
            currentDeck[randomIndex] = temp;
        }
    }

    public Card DrawCard()
    {
        if (currentDeck.Count == 0)
        {
            Debug.Log("Deck is empty!");
            return null;
        }

        Card drawnCard = currentDeck[0];
        currentDeck.RemoveAt(0);

        return drawnCard;
    }
}