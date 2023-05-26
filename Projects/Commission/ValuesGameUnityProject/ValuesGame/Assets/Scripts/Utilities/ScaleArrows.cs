using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleArrows : MonoBehaviour
{
    [SerializeField] GameObject HighlightRegion;
    [SerializeField] float scaleValue;
    float originalScaleX, originalScaleY;
    bool isLit = false;
    void ResetScale()
    {
        var local = HighlightRegion.transform.localScale;
        HighlightRegion.transform.localScale = new Vector3(originalScaleX, originalScaleY, local.z);
    }
    void IncreaseScale()
    {
        var local = HighlightRegion.transform.localScale;
        HighlightRegion.transform.localScale = new Vector3(local.x * scaleValue, local.y * scaleValue, local.z);
    }
    void Start()
    {
        originalScaleX = HighlightRegion.transform.localScale.x;
        originalScaleY = HighlightRegion.transform.localScale.y;
    }
    void OnMouseOver()
    {
        if(!isLit)
        {
            isLit = true;
            // Increase Scaling
            IncreaseScale();
        }
    }

    public void OnMouseExit()
    {
        if(isLit)
        {
            isLit = false;
            ResetScale();
        }
    }
}
