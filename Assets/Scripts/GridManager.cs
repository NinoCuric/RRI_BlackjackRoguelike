using Cards;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int boardSize = 4;
    public GameObject cardPrefab; //assign in inspector
    public GameObject gridCellPrefab; //assign in inspector
    public List<GameObject> gridObjects = new List<GameObject>();
    public GameObject[] gridCells;
    public GameObject gridHighlightOverlay;
    public Transform gridTransform; //root grid position

    private Vector2 centerOffset;
    private float scaleCellX;
    private float scaleCardX;

    void Start()
    {
        GridClear();
    }

    public void CreateGrid()
    {
        if (gridCells != null)
        {
            Debug.LogWarning("Grid already exists.");
            return;
        }

        gridCells = new GameObject[boardSize];
        centerOffset = new Vector2(boardSize / 2f - 0.5f, 0f);
        scaleCellX = gridCellPrefab.GetComponent<Transform>().localScale.x;
        scaleCardX = cardPrefab.GetComponent<Transform>().localScale.x * 0.8f;
        PositionOverlay();


        for (int x = 0; x < boardSize; x++)
        {
            Vector2 gridPosition = new Vector2(x, 0);
            Vector2 spawnPosition = (gridPosition - centerOffset) * scaleCellX;

            GameObject gridCell = Instantiate(gridCellPrefab, spawnPosition, Quaternion.identity);

            gridCell.transform.SetParent(gridHighlightOverlay.GetComponent<Transform>());

            gridCell.GetComponent<GridCell>().gridIndex = gridPosition;

            gridCells[x] = gridCell;
        }
    }


    public bool AddObjectToGrid(Card cardData)
    {
        if (gridObjects.Count >= boardSize)
        {
            Debug.LogWarning("Board is full");
            return false;
        }
        else
        {
            for (int x = 0; x < boardSize; x++)
            {
                if (!gridCells[x].GetComponent<GridCell>().cellFull)
                {
                    Vector2 gridPosition = new Vector2(x, 0);
                    Vector3 spawnPosition = (gridPosition - centerOffset) * scaleCellX * scaleCardX + (gridPosition - centerOffset) * scaleCardX;
                    
                    GameObject newCard = Instantiate(cardPrefab, gridTransform.position + spawnPosition, Quaternion.identity);
                    newCard.transform.SetParent(gridCells[x].GetComponent<Transform>());
                    newCard.GetComponent<RectTransform>().localScale *= 0.8f;
                    gridObjects.Add(newCard);
                    newCard.GetComponent<CardDisplay>().cardData = cardData;
                    gridCells[x].GetComponent<GridCell>().objectInCell = newCard;
                    gridCells[x].GetComponent<GridCell>().cellFull = true;
                    return true;
                }
            }
            return false; //already checked at start but added for safety
        }
    }


    private void PositionOverlay()
    {
        float size = boardSize * scaleCellX; // match spacing
        float scaleCellY = gridCellPrefab.GetComponent<Transform>().localScale.y;

        gridHighlightOverlay.transform.position = new Vector2(0, 0);
        gridHighlightOverlay.transform.localScale = new Vector2(size, scaleCellY);
    }

    public int CalculateBoardValue() //ovo triba minjat za blackjack logiku, provjeri ovo da ide za nase karte
    {
        int total = 0;

        foreach (GameObject cardObj in gridObjects)
        {
            CardDisplay display = cardObj.GetComponent<CardDisplay>();

            if (display != null)
            {
                total += display.cardData.valueNumber;
            }
        }

        return total;
    }

    public void GridClear()
    {
        foreach (GameObject obj in gridObjects)
        {
            GameManager.Instance.DiscardPileManager.AddToDiscard(obj.GetComponent<CardDisplay>().cardData);
            if (boardSize == gridCells.Length) { Destroy(obj); }
        }
        gridObjects.Clear();

        if (gridCells != null)
        {
            if (boardSize != gridCells.Length)          //when changing board size 
            {
                foreach (GameObject cell in gridCells)
                {
                    if (cell != null)
                    {
                        Destroy(cell);
                    }
                }
                gridCells = null;
                CreateGrid();
            }
            else                                        //for when board stays the same
            {
                foreach (GameObject cell in gridCells)
                {
                    cell.GetComponent<GridCell>().cellFull = false;
                    cell.GetComponent<GridCell>().objectInCell = null;
                }
            }
        }

    }

}
