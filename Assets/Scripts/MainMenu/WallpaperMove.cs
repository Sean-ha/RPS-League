using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallpaperMove : MonoBehaviour
{
    private RectTransform rectTransform;
    private bool goingUp;

    private void Start()
    {
        rectTransform = gameObject.GetComponent<RectTransform>();
        goingUp = true;
    }

    // Moves the wallpaper slowly
    void Update()
    {
        float positionY = rectTransform.anchoredPosition.y;

        // Wallpaper moving upwards
        if(goingUp)
        {
            rectTransform.anchoredPosition = new Vector2(0, positionY - 0.5f);
            if(positionY <= -900)
            {
                goingUp = false;
            }
        }
        // Wallpaper moving downwards
        else
        {
            rectTransform.anchoredPosition = new Vector2(0, positionY + 0.5f);
            if(positionY >= 100)
            {
                goingUp = true;
            }
        }
    }
}
