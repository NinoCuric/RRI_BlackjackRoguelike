using System.Collections.Generic;
using UnityEngine;
using Cards;
using UnityEngine.UI;
using TMPro;

public class DeckManager : MonoBehaviour
{
    [Header("All Card Assets")]
    public List<Card> allCards = new List<Card>();

    private List<Card> currentDeck = new List<Card>();
    public GameObject deckImage;
    public TMP_Text cardCount;

    private void Start()
    {
        //load all card assets from resources folder
        Card[] cards = Resources.LoadAll<Card>("Cards");

        //add all loaded cards to the allCards list
        allCards.AddRange(cards);

        BuildDeck();
        ShuffleDeck();

        HandManager hand = FindAnyObjectByType<HandManager>();
        for (int i = 0; i < 5; i++)
        {
            DrawCard(hand);
        }
        UpdateDeckVisual();
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

    public void DrawCard(HandManager handManager)
    {
        if (currentDeck.Count == 0)
        {
            Debug.Log("Deck is empty!");
            return;
        }

        Card drawnCard = currentDeck[0];
        bool canDraw = handManager.AddCardToHand(drawnCard);
        if (canDraw)
        {
            currentDeck.RemoveAt(0);
            UpdateDeckVisual();
        }
    }

    public void UpdateDeckVisual()
    {
        deckImage.SetActive(currentDeck.Count > 0);     //alternativa u DiscardPileu
        cardCount.text = currentDeck.Count.ToString();
    }

    public void DrawCardFromButton()
    {
        HandManager hand = FindAnyObjectByType<HandManager>();
        DrawCard(hand);
    }
}