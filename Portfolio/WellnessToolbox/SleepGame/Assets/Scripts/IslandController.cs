using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class IslandController : MonoBehaviour
{
    [SerializeField] int selectedScene;
    [SerializeField] Color selectedColor;
    MeshRenderer meshRender;

    bool selected;

    // Start is called before the first frame update
    void Start()
    {
        meshRender = GetComponent<MeshRenderer>();
        selected = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (selected&&Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(selectedScene);

        }
    }

    private void OnMouseEnter()
    {
        meshRender.material.color = selectedColor;
        selected = true;
        

    }
    private void OnMouseExit()
    {
        meshRender.material.color = Color.white;
        selected = false;

    }
}
