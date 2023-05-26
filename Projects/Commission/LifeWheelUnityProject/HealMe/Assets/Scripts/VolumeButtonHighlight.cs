using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeButtonHighlight : MonoBehaviour
{
    [SerializeField] Image MuteOn, MuteOff;
    [SerializeField] Color HighlightColor;
    [SerializeField] BoxCollider2D HighlightRegion;
    bool isLit = false;
    void OnMouseOver()
    {
        if(!isLit)
        {
            isLit = true;
        }
    }

    public void OnMouseExit()
    {
        if(isLit)
        {
            isLit = false;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        isLit = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isLit)
        {
            // Set the button
            MuteOn.color = HighlightColor;
            MuteOff.color = HighlightColor;
        }
        else
        {
            MuteOn.color = Color.white;
            MuteOff.color = Color.white;
        }
    }
}
