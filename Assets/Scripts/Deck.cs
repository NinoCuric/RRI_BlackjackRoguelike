using System.Collections.Generic;
using UnityEngine;
using Cards;

public class Deck : MonoBehaviour
{
    [Header("All Card Assets")]
    public List<Card> allCards = new List<Card>();

    private List<Card> currentDeck = new List<Card>();

    public GameObject cardPrefab;
    public Transform playerCardArea;

    private void Start()
    {
        BuildDeck();
        ShuffleDeck();

        Card drawnCard = DrawCard();

        Debug.Log("Drew: " + drawnCard.cardName);
        SpawnCard(drawnCard);
    }

    public void SpawnCard(Card cardData)
    {
        GameObject newCard = Instantiate(cardPrefab, playerCardArea);
        Vector3 camPos = Camera.main.transform.position;
        newCard.transform.position = new Vector3(camPos.x, camPos.y, 0);
        CardDisplay display = newCard.GetComponent<CardDisplay>();

        display.Setup(cardData);
    }

    public void BuildDeck()
    {
        currentDeck.Clear();

        foreach (Card card in allCards)
        {
            card.InitializeNumberValue();
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