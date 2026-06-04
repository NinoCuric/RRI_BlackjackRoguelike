using UnityEngine;
using NUnit.Framework;
using System.Collections.Generic;
using Cards;
using System;
using UnityEditor.ShaderGraph;

public class HandManager : MonoBehaviour
{
    public GameObject cardPrefab; //assign in inspector
    public Transform handTransform; //root hand position
    public float fanSpread = -2.5f;

    public float cardSpacing = 120f;
    public float verticalSpacing;
    public int maxHandSize = 10;
    public List<GameObject> cardsInHand = new List<GameObject>();


    void Start()
    {

    }

    void Update()
    {
        //UpdateHandVisuals();
    }

    public bool AddCardToHand(Card cardData)
    {   
        if (cardsInHand.Count >= maxHandSize)
        {
            Debug.Log("Hand is full!");
            return false;
        }

        //instatiate the card
        GameObject newCard = Instantiate(cardPrefab, handTransform.position, Quaternion.identity, handTransform);
        cardsInHand.Add(newCard);

        //set cardData of instatiated card
        newCard.GetComponent<CardDisplay>().cardData = cardData;

        UpdateHandVisuals();

        return true;
    }

    public void UpdateHandVisuals()
    {
        int cardCount = cardsInHand.Count;

        if (cardCount == 1)
        {
            cardsInHand[0].transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            cardsInHand[0].transform.localPosition = new Vector3(0f, 0f, 0f);
            return;
        }

        verticalSpacing = (cardCount - 2) * 6f; //adjust vertical spacing based on card count

        for (int i = 0; i < cardCount; i++)    {
            float rotationAngle = (fanSpread * (i - (cardCount - 1) / 2f));
            cardsInHand[i].transform.localRotation = Quaternion.Euler(0f, 0f, rotationAngle);

            float horizontalOffset = (cardSpacing * (i - (cardCount - 1) / 2f));
            
            float normalizedPosition = (2f * i / (cardCount - 1) - 1f);  //normalise between -1 and 1
            float verticalOffset = verticalSpacing * (1 - normalizedPosition * normalizedPosition);
            
            //set card position
            cardsInHand[i].transform.localPosition = new Vector3(horizontalOffset, verticalOffset, 0f);
        
        }
    }

}
