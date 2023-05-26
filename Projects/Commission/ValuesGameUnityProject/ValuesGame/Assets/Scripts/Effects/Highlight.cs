using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlight : MonoBehaviour
{
    [SerializeField] Scene2DataManager dm;
    [SerializeField] TimerLevel2 timer;
    [SerializeField] MoevementManagerScene2 mv;
    [SerializeField] List<Renderer> rend;
    [SerializeField] int choice;
    [SerializeField] int button;
    [SerializeField] Camera cam;
    [SerializeField] string ladderName;
    // Start is called before the first frame update
    void Start()
    {

    }
    private void Update()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            if (hitInfo.collider.gameObject.tag == ladderName && dm.gameOn)
            {
                HighlightObjects();

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
                DehighlightObjects();
            }

        }




    }

    void HighlightObjects()
    {
        for (int i = 0; i < rend.Count; i++)
        {
            rend[i].material.color = Color.red;
        }
    }
    void DehighlightObjects()
    {
        for (int i = 0; i < rend.Count; i++)
        {
            rend[i].material.color = Color.white;
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
