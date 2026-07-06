using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] GameObject bar;
    int completed = 0;
    public float MaxScale = 3.6f, MaxValues = 8.0f;
    float scaler => (MaxScale / MaxValues);
    float newScale => scaler * ++completed;
    public Vector3 currentScale => bar.transform.localScale;
    public void ResizeBar() => bar.transform.localScale = new Vector3(newScale, currentScale.y, currentScale.z);

    // Update is called once per frame
    void Update()
    {
        // ResizeBar();
    }
}