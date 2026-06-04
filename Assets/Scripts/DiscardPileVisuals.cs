using UnityEngine;

public class DiscardPileVisuals : MonoBehaviour
{
    [SerializeField] private GameObject discardImage;

    void Start()
    {
        UpdateDiscardVisual();
    }

    public void UpdateDiscardVisual()
    {
        discardImage.SetActive(GameManager.Instance.DiscardPileManager.HasCards());
    }

}
