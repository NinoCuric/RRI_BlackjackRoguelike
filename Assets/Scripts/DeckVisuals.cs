using TMPro;
using UnityEngine;

public class DeckVisuals : MonoBehaviour
{
    public GameObject deckImage;
    public TMP_Text cardCount;


    void Start()
    {

    }

    void Update()
    {

    }

    public void DeckVisual()
    {
        Debug.Log("DeckVisual called");

    }

    public void UpdateDeckVisual(int deckCount)
    {
        Debug.Log($"UpdateDeckVisual called count = {deckCount}");

        deckImage.SetActive(deckCount > 0);     //alternativa u DiscardPileu
        cardCount.text = deckCount.ToString();
    }

}
