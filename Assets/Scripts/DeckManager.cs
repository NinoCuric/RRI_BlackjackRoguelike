using System.Collections.Generic;
using UnityEngine;
using Cards;

public class DeckManager : MonoBehaviour
{
    [Header("All Card Assets")]
    public List<Card> allCards = new List<Card>();

    public List<Card> currentDeck = new List<Card>();
    public int drawStartTurn = 5;
    public int DeckCount => currentDeck.Count;

    private HandManager handManager;
    private DeckVisuals deckVisuals;

    void Awake()
    {
        deckVisuals = FindAnyObjectByType<DeckVisuals>();
        handManager = FindAnyObjectByType<HandManager>();
    }

    void Start()
    {
        //load all card assets from resources folder
        Card[] cards = Resources.LoadAll<Card>("Cards");

        //add all loaded cards to the allCards list
        allCards.AddRange(cards);

        BuildDeck();
        ShuffleDeck();

        deckVisuals.UpdateDeckVisual();
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
        for (int i = 0; i < DeckCount; i++)
        {
            int randomIndex = Random.Range(i, DeckCount);

            Card temp = currentDeck[i];
            currentDeck[i] = currentDeck[randomIndex];
            currentDeck[randomIndex] = temp;
        }
    }

    public void DrawCard()
    {
        Debug.Log("DrawCard called on: " + gameObject.GetEntityId() + " | Count: " + DeckCount);
        if (DeckCount == 0)
        {
            GameManager.Instance.DiscardPileManager.ShuffleDiscardIntoDeck();
        }

        Card drawnCard = currentDeck[0];
        bool canDraw = handManager.AddCardToHand(drawnCard);
        if (canDraw)
        {
            currentDeck.RemoveAt(0);
            deckVisuals.UpdateDeckVisual();
        }
    }

    public void AddCardsToDeck(List<Card> cards)
    {
        currentDeck.AddRange(cards);

        ShuffleDeck();
        deckVisuals.UpdateDeckVisual();
    }

}