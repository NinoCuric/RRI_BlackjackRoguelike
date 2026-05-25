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
    public float fanSpread = -10f;

    public float cardSpacing = 115f;
    public float verticalSpacing = 100f;
    public List<GameObject> cardsInHand = new List<GameObject>();


    void Start()
    {
        for(int i = 0; i < 8; i++)
            AddCardToHand();
    }

    void Update()
    {
        UpdateHandVisuals();
    }

    public void AddCardToHand()
    {   //instatiate the card
        GameObject newCard = Instantiate(cardPrefab, handTransform.position, Quaternion.identity, handTransform);
        cardsInHand.Add(newCard);

        UpdateHandVisuals();
    }

    private void UpdateHandVisuals()
    {
        int cardCount = cardsInHand.Count;
        for (int i = 0; i < cardCount; i++)    {
            float rotationAngle = (fanSpread * (i - (cardCount - 1) / 2f));
            cardsInHand[i].transform.localRotation = Quaternion.Euler(0f, 0f, rotationAngle);

            float horizontalOffset = (cardSpacing * (i - (cardCount - 1) / 2f));
            
            float normalizedPosition = cardCount == 1 ? 0f : (2f * i / (cardCount - 1) - 1f);  //normalise between -1 and 1
            float verticalOffset = verticalSpacing * (1 - normalizedPosition * normalizedPosition);
            
            //set card position
            cardsInHand[i].transform.localPosition = new Vector3(horizontalOffset, verticalOffset, 0f);
        
        
        }
    }
}
