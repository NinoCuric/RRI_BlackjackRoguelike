using UnityEngine;

public class GridHighlighterCell : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Color highlightColor = Color.yellow;
    private Color originalColor;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    private void OnMouseEnter()
    {
        spriteRenderer.color = highlightColor;
    }

    private void OnMouseExit()
    {
        spriteRenderer.color = originalColor;

    }

}
