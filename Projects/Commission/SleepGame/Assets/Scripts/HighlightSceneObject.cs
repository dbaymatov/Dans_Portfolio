using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HighlightSceneObject : MonoBehaviour
{
    [SerializeField] Button button;
    bool isOver = false;
    Vector3 scale;
    float factor = 1.20f;
    // Start is called before the first frame update
    void Start()
    {
        scale = transform.localScale;
    }

    void SetScale(Vector3 scale) => transform.localScale = scale;

    void OnMouseOver()
    {
        if(!isOver)
        {
            isOver = true;
            SetScale(scale * factor);
        }
    }

    void OnMouseExit()
    {
        if(isOver)
        {
            isOver = false;   
            SetScale(scale);
        }
    }
}
