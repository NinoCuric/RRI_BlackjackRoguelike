using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeckVisuals : MonoBehaviour
{
    [SerializeField] private GameObject deckImage;
    [SerializeField] private TMP_Text cardCount;
    [SerializeField] private Button drawButton;

    private void Awake()
    {
        drawButton.onClick.AddListener(OnDrawButtonClicked);
    }

    private void Start()
    {
        UpdateDeckVisual();
    }

    private void OnDestroy()
    {
        drawButton.onClick.RemoveListener(OnDrawButtonClicked);
    }

    private void OnDrawButtonClicked()
    {
        GameManager.Instance.DeckManager.DrawCard();
    }

    public void UpdateDeckVisual()
    {
        int deckCount = GameManager.Instance.DeckManager.currentDeck.Count;
        Debug.Log($"UpdateDeckVisual called count = {deckCount}");

        deckImage.SetActive(deckCount > 0);
        cardCount.text = deckCount.ToString();
    }

}
