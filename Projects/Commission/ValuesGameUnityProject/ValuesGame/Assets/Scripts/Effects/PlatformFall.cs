using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformFall : MonoBehaviour
{
    [SerializeField] public List<GameObject> platforms;
    [SerializeField]int speed;
    [SerializeField] int shakeAmount; 
    bool shaking;
    public int currentPlatform;
    Vector3 originalPos;

    // Start is called before the first frame update
    void Start()
    {
        currentPlatform = 0;
    }

    public void FallPlatform()
    {
        platforms[currentPlatform].AddComponent<ShakeAndFall>();
        ShakeAndFall sk = platforms[currentPlatform].GetComponent<ShakeAndFall>();
        sk.StartShaking();

        if (currentPlatform < platforms.Count - 1)
        {
            currentPlatform++;
        }
    }
}
