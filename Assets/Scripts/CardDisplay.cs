using UnityEngine;

using UnityEngine.UI;
using TMPro;
using Cards;


public class CardDisplay : MonoBehaviour
{

    public Card cardData;

    public TMP_Text nameText;
    public TMP_Text valueNumber;
    public TMP_Text[] baseNumbers;
    public Image[] typeIcons;
    public Image TypeImage;

    private Color[] typeColors =    {
        Color.red,   //Hearts, Diamonds 0,1
        Color.black, //Clubs, Spades    2.3
    }; 

    void Start()
    {
        UpdateCardDisplay();
    }
  
/*    public void InitializeNumber()
    {
        valueNumber.text = ((int)cardData.baseNumber).ToString();
    }*/

    public void letterToDisplay(TMP_Text baseNumber)
    {
        if (cardData.baseNumber == Card.CardNumber.Ace)
        {
            baseNumber.text = "A";
        }
        else if (cardData.baseNumber == Card.CardNumber.Jack)
        {
            baseNumber.text = "J";
        }
        else if (cardData.baseNumber == Card.CardNumber.Queen)
        {
            baseNumber.text = "Q";
        }
        else if (cardData.baseNumber == Card.CardNumber.King)
        {
            baseNumber.text = "K";
        }
        else
            baseNumber.text = ((int)cardData.baseNumber).ToString();

    }

    public void UpdateCardDisplay()
    {
        //TypeImage.color = typeColors[(int)cardData.cardType];

        nameText.text = cardData.cardName;
        valueNumber.text = cardData.valueNumber.ToString();
        for (int i = 0; i < baseNumbers.Length; i++)
        {
            letterToDisplay(baseNumbers[i]);
            baseNumbers[i].color = typeColors[(int)cardData.cardType / 2];
        }
        valueNumber.text = ((int)cardData.baseNumber).ToString();


    }
}