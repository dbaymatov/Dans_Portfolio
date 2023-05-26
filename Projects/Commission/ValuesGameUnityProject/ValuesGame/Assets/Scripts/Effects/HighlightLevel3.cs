using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightLevel3 : MonoBehaviour
{
    [SerializeField] Scene3DataManager dm;
    [SerializeField] TimerLevel3 timer;
    [SerializeField] MoevementManagerScene3 mv;
    [SerializeField] List<Renderer> rend;
    [SerializeField] int button, choice;
    [SerializeField] Camera cam;
    [SerializeField] string holeName;
    // Start is called before the first frame update
    void Start()
    {

    }
    private void Update()
    {
        if(dm.gameOn)
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                if (hitInfo.collider.gameObject.tag == holeName)
                {
                    HighlightObjects(true);
                    if (Input.GetMouseButtonDown(0))
                    {
                        Debug.Log("mouse press detected");
                        timer.ResetTimer();
                        dm.ChoiceClick(choice);
                        mv.ButtonPress(button);
                    }
                }
                else
                {
                    HighlightObjects(false);
                }
            }
        }
    }

    void HighlightObjects(bool isLit)
    {
        Color newColor = isLit ? Color.red : Color.white;
        foreach (var mat in rend)
        {
            mat.material.color = newColor;
        }
    }

    private void OnMouseEnter()
    {
        if (dm.gameOn)
        {
            for (int i = 0; i < rend.Count; i++)
            {
                rend[i].material.color = Color.red;
            }

            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("mouse press detected");
                timer.ResetTimer();
                dm.ChoiceClick(choice);
                mv.ButtonPress(button);
            }

        }
        else
        {
            for (int i = 0; i < rend.Count; i++)
            {
                rend[i].material.color = Color.white;
            }
        }

    }
    private void OnMouseExit()
    {
        for (int i = 0; i < rend.Count; i++)
        {
            rend[i].material.color = Color.white;
        }
    }
}
