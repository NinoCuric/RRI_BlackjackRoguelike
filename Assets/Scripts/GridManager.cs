using NUnit.Framework;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int boardSize = 4;
    public GameObject gridCellPrefab; //assign prefab in inspector
    public List<GameObject> gridObjects = new List<GameObject>();
    public GameObject[] gridCells;
    public GameObject gridHighlightOverlay;

    void Start()
    {
        CreateGrid();
        PositionOverlay();
    }

    void CreateGrid()
    {
        gridCells = new GameObject[boardSize];
        Vector2 centerOffset = new Vector2(boardSize / 2f - 0.5f, 0); 

        for (int x = 0; x < boardSize; x++)
        {
            Vector2 gridPosition = new Vector2(x, 0);
            Vector2 spawnPosition = (gridPosition - centerOffset) * 1.5f; //1.5 based on prefab scale

            GameObject gridCell = Instantiate(gridCellPrefab, spawnPosition, Quaternion.identity);

            gridCell.transform.SetParent(transform);

            gridCell.GetComponent<GridCell>().gridIndex = gridPosition;

            gridCells[x] = gridCell;
        }
    }


    public bool AddObjectToGrid(GameObject obj, Vector2 gridPosition)
    {
        if (gridPosition.x >= 0 && gridPosition.x < boardSize)
        {
            GridCell cell = gridCells[(int)gridPosition.x].GetComponent<GridCell>();

            if (cell.cellFull)
            {
                Debug.LogWarning("Cell is already occupied");
                return false;
            }
            else
            {
                GameObject newObj = Instantiate(obj, cell.GetComponent<Transform>().position, Quaternion.identity);
                newObj.transform.SetParent(transform);
                gridObjects.Add(newObj);
                cell.objectInCell = newObj;
                cell.cellFull = true;
                return true;
            }
        }
        else 
        {
            Debug.LogError("Cell out of bounds");
            return false; 
        }
    }


    void PositionOverlay()
    {
        float size = boardSize * 1.5f; // match your spacing

        gridHighlightOverlay.transform.position = new Vector2(0, 0);
        gridHighlightOverlay.transform.localScale = new Vector2(size, 2.2f);
    }
}
