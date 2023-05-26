using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorGradient : MonoBehaviour
{
    [SerializeField] RawImage img;
    [SerializeField] ProgressBar progressBar;
    float point => (progressBar.currentScale.x / progressBar.MaxScale);
    // Start is called before the first frame update
    void Start()
    {
        img.color = new Color(0,0,1,1);
    }

    // Update is called once per frame
    void Update()
    {
        img.color = new Color(0, point, 1 - point, 1);
    }
}
