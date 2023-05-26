using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackdropManager : MonoBehaviour
{
    [SerializeField] List<GameObject> backdrops;
    [SerializeField] List<GameObject> rooms;
    [SerializeField] GameObject EndRoom;
    int selectedBackdrop;
    int selectedRoom;

    void Start()
    {
        PickRandomBackground();
    }

    //picks a random background and foreground from the list and sends out event to reset lava/rock
    public void PickRandomBackground()
    {

        backdrops[selectedBackdrop].SetActive(false);
        rooms[selectedRoom].SetActive(false);

        selectedBackdrop = Random.Range(0, backdrops.Count);
        selectedRoom = Random.Range(0, backdrops.Count);

        backdrops[selectedBackdrop].SetActive(true);
        rooms[selectedRoom].SetActive(true);

        EventManager.ResetRock.Invoke();
    }

    public void Disable()
    {
        for (int i = 0; i < backdrops.Count; i++)
        {
            rooms[i].SetActive(false);
            backdrops[i].SetActive(false);
        }
        EventManager.ResetRock.Invoke();
    }

    public void EnableEndGame()
    {
        EndRoom.SetActive(true);
    }
}
