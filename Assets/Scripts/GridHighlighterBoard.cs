using UnityEngine;

public class GridHighlighterBoard : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Color disableColor = new(1, 1, 1, 0);
    private Color highlightColor;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        highlightColor = spriteRenderer.color;
        spriteRenderer.color = disableColor;
    }

    private void OnMouseEnter()
    {
        spriteRenderer.color = highlightColor;
    }

    private void OnMouseExit()
    {
        spriteRenderer.color = disableColor;
    }

}
