using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDestroy : MonoBehaviour
{
    [SerializeField] List<ObjectCollection> rooms;

    [SerializeField] BoxCollider scene1trigger;
    [SerializeField] BoxCollider scene2trigger;
    [SerializeField] BoxCollider scene3trigger;

    // Start is called before the first frame update
    void Start()
    {
        //sets inital location of the rubble
        for (int i = 0; i < rooms.Count;)
        {
            rooms[i].SetPositions();
            i++;
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        Debug.Log("I collided" + other.tag);

        if (other.gameObject.tag == "room0")
        {
            rooms[0].ActivateRB();
        }
        if (other.gameObject.tag == "room1")
        {
            rooms[1].ActivateRB();
        }
        if (other.gameObject.tag == "room2")
        {
            rooms[2].ActivateRB();
        }
    }

    public void ResetRoom()
    {
        for (int i = 0; i < rooms.Count;)
        {
            rooms[i].Reset();
            i++;
        }
    }
}

//class used to store rubble references within
[System.Serializable]
public class ObjectCollection
{
    public List<GameObject> rubble;
    public List<Vector3> InitialPositions;

    public void SetPositions()
    {
        InitialPositions = new List<Vector3>();

        for (int i = 0; i < rubble.Count;)
        {
            InitialPositions.Add(rubble[i].transform.localPosition);

            i++;
        }
    }

    public void ActivateRB()
    {
        for (int i = 0; i < rubble.Count;)
        {
            Rigidbody rb = rubble[i].GetComponent<Rigidbody>();
            rb.isKinematic = false;
            i++;

        }
    }

    public void Reset()
    {
        for (int i = 0; i < rubble.Count;)
        {
            Rigidbody rb = rubble[i].GetComponent<Rigidbody>();
            rb.isKinematic = true;
            rubble[i].transform.localPosition = InitialPositions[i];
            //rubble[i].transform.localPosition = new Vector3(0, 0, 0);
            rubble[i].transform.rotation = new Quaternion(0, 0, 0, 1);
            i++;
        }
        Debug.Log("pos reset");

    }
}
