using TMPro;
using System.Collections.Generic;
using UnityEngine;
using Cards;

public class CardSelectionManager : MonoBehaviour
{
    public GameObject cardPrefab;       //assign
    public GameObject gridCellPrefab;   //assign
    public List<GameObject> gridObjects = new List<GameObject>();
    public GameObject[,] GridCells;
    public RectTransform selectionPanel;

    [SerializeField] private TMP_Text selectionEffectText;

    /*


    public void CreateCardSelectionGrid(List<Card> cards)
    {
        //needs to be based on space on screen
        float selectionHeight = selectionPanel.rect.width; ;  
        float selectionWidth = selectionPanel.rect.width; ;

        int selectWidthAmount;      //how many cards to show in a row, and height for how many rows
        int selectHeightAmount = (cards.Count + selectWidthAmount - 1) / selectWidthAmount;     //round up int division
        
        GridCells = new GameObject[selectHeightAmount, selectWidthAmount];



        PositionCardInSelection(cards);
    }


  /*  private void PositionCardInSelection(List<Card> cards, int index, int selectWidthAmount, float selectionWidth, float selectionHeight)
    {
        int row = index / selectWidthAmount;
        int col = index % selectWidthAmount;
        float xPos = (col - (selectWidthAmount - 1) / 2f) * (selectionWidth / selectWidthAmount);
        float yPos = ((selectHeightAmount - 1) / 2f - row) * (selectionHeight / selectHeightAmount);
        card.transform.localPosition = new Vector3(xPos, yPos, 0f);
    }*/


}
