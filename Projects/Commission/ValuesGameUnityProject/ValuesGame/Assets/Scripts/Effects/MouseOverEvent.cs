using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseOverEvent : MonoBehaviour
{
    [SerializeField] GameObject HighlightRegion;
    bool isLit = false;
    void OnMouseOver()
    {
        if(!isLit)
        {
            isLit = true;
            HighlightRegion.SetActive(true);
        }
    }

    public void OnMouseExit()
    {
        if(isLit)
        {
            isLit = false;
            HighlightRegion.SetActive(false);
        }
    }
}
