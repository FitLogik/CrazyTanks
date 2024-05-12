using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] RawImage backgroundImage;
    [SerializeField] float x;
    [SerializeField] float y;

    void Update()
    {
        backgroundImage.uvRect = new Rect(backgroundImage.uvRect.position + new Vector2(x, y) * Time.deltaTime,
                                          backgroundImage.uvRect.size);
    }
}
