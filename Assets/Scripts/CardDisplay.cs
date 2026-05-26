using UnityEngine;

using UnityEngine.UI;
using TMPro;
using Cards;


public class CardDisplay : MonoBehaviour
{

    public Card cardData;

    public TMP_Text nameText;
    public TMP_Text valueNumber;
    public Image CardImage;

    void Start()
    {
        InitializeNumber();

        UpdateCardDisplay();
    }
  
    public void InitializeNumber()      //temporary
    {
        valueNumber.text = ((int)cardData.baseNumber).ToString();
    }


    public void UpdateCardDisplay()
    {
        nameText.text = cardData.cardName;
//        valueNumber.text = cardData.valueNumber.ToString();
        CardImage.sprite = cardData.cardSprite;

    }
}