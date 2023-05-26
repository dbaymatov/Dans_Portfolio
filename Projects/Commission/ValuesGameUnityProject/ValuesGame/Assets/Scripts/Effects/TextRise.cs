using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextRise : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] GameObject startLocation;
    [SerializeField] Scene2DataManager dm;
    [SerializeField] MoevementManagerScene2 mv;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (dm.gameOn)
        {
            transform.position += new Vector3(0, Time.deltaTime * speed, 0);
        }
        if (mv.moving && !dm.gameEnd)
        {
            transform.position += new Vector3(0, Time.deltaTime * speed * 2, 0);
        }
    }

    public void ResetText()
    {
        transform.position = startLocation.transform.position;
    }
}
