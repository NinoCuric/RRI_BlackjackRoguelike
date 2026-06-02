using UnityEngine;

public class UIObjectPositioner : MonoBehaviour
{
    public RectTransform objectToPosition;

    public int widthDivider = 2;
    public int heightDivider = 2;
    public float widthMultiplier = 1f;
    public float heightMultiplier = 1f;

    public bool updatePosition = false;    //testing

    void Start()
    {
        SetUIObjectPositoon();
    }

    
    void Update()
    {
        if (updatePosition)
        {
            SetUIObjectPositoon();
        }
    }

    public void SetUIObjectPositoon()
    {
        if (objectToPosition != null && widthDivider != 0 && heightDivider != 0)
        {
            //calculate anchor position
            float anchorX = widthMultiplier / widthDivider;
            float anchorY = heightMultiplier / heightDivider;

            //set anchor and pivot
            objectToPosition.anchorMin = new Vector2(anchorX, anchorY);
            objectToPosition.anchorMax = new Vector2(anchorX, anchorY);
            objectToPosition.pivot = new Vector2(0.5f, 0.5f);

            //set local position to zero to align with anchor point
            objectToPosition.anchoredPosition = Vector2.zero;
        }
    }

}
