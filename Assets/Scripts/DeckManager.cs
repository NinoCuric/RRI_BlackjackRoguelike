using System.Collections.Generic;
using UnityEngine;
using Cards;
using UnityEngine.UI;
using TMPro;

public class DeckManager : MonoBehaviour
{
    [Header("All Card Assets")]
    public List<Card> allCards = new List<Card>();

    public List<Card> currentDeck = new List<Card>();
    public int drawStartTurn = 5;

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

        for (int i = 0; i < drawStartTurn; i++)
        {
            DrawCard();
        }
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

    public void DrawCard()
    {
        Debug.Log("DrawCard called on: " + gameObject.GetEntityId() + " | Count: " + currentDeck.Count);
        if (currentDeck.Count == 0)
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
        GridManager gridManager = FindAnyObjectByType<GridManager>();       //TEMPORARY GRID TESTING
        gridManager.CreateGrid();
    }

    public int GetDeckCount()
    {
        return currentDeck.Count;
    }

}