using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockFall : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] GameObject startLocation;
    [SerializeField] Scene3DataManager dm;
    [SerializeField] MoevementManagerScene3 mv;
    // Start is called before the first frame update
    void Start()
    {
        EventManager.ResetRock.AddListener(ResetRock);
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
            transform.position += new Vector3(0, Time.deltaTime * speed, 0);
        }


    }

    public void ResetRock()
    {
        transform.position = startLocation.transform.position;
    }
}
