using UnityEngine;

public class Parallax1 : MonoBehaviour
{
    public GameObject cam;
    public float parallaxEffect;
    public Vector3 moveOverTime;

    //will leave it for now
    private float length;

    private Vector3 camCurrentLoc;
    private Vector3 camOldLoc;
    private Vector3 camLocDifference;

    // Start is called before the first frame update
    void Start()
    {
        camOldLoc = cam.transform.position;


        float left = 0;
        float right = 0;

        foreach (Transform child in transform)
        {
            if(child.transform.position.x>right)
            {
                left = child.transform.position.x;
            }
            if (child.transform.position.x > left)
            {
                left = child.transform.position.x;
            }
        }
        //length = GetComponent<SpriteRenderer>().bounds.size.x;
        length = right - left;

    }

    //paralax effect here along with movement of layer
    void FixedUpdate()
    {

        //here finds the difference in locations of the camera to use in parallax calculations
        camCurrentLoc = cam.transform.position;
        camLocDifference = camCurrentLoc - camOldLoc;
        camOldLoc = camCurrentLoc;

        //applies parallax effect and layer movement
       // camLocDifference.x = camLocDifference.x / 3;//decreases vertical parallax, idk if i wanna use it or not, will leave it here for now

        transform.position -= (camLocDifference * parallaxEffect) * Time.deltaTime;
        transform.position += moveOverTime * Time.deltaTime;


        //image layer reset here, sets layer location to that of a camera thus centering it whenever its origin gets to far from cam
        //if (Mathf.Abs(cam.transform.position.x - transform.position.x) > length)
        //{
        //    transform.position = new Vector2(cam.transform.position.x, transform.position.y);

        //}

    }

    //crazy clouds xddd use at ur own descretion
    /*
    void FixedUpdate()
    {

        //paralax effect here along with movement of layer
        transform.position += new Vector3(cam.transform.position.x * parallaxEffect, cam.transform.position.y * parallaxEffect / 2, 0);

        transform.position += moveOverTime * Time.deltaTime;

        //image layer reset here, sets layer location to that of a camera thus centering it


        if (Mathf.Abs(cam.transform.position.x - transform.position.x) > length)
        {
            transform.position = new Vector2(cam.transform.position.x, transform.position.y);
            Debug.Log("layer reseted");
        }


        Debug.Log("cam difference is " + (cam.transform.position.x - transform.position.x) + " lenght is " + length);

    }
    */
}
