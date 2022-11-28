using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Scroller : MonoBehaviour
{
    [SerializeField]
    RawImage scrollImage;
    [SerializeField]
    Vector2 speedScroll;

    // Update is called once per frame
    void Update()
    {
        scrollImage.uvRect = new Rect(scrollImage.uvRect.position + speedScroll * Time.deltaTime,scrollImage.uvRect.size);
    }
}
