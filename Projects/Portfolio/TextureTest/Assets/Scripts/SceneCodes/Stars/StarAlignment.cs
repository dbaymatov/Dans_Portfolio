using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine;

public class StarAlignment : MonoBehaviour
{
    [SerializeField] string sceneName;
    [SerializeField] List<Transform> points;
    [SerializeField] List<Transform> stars;
    [SerializeField] float validDistance;

    void Update()
    {
        if (CheckAligned())
        {
            SceneManager.LoadScene(sceneName);
        }
    }

    bool CheckAligned()
    {
        List<bool> bools = new List<bool>();

        foreach (Transform transform in points)
        {
            foreach(Transform star in stars)
            {
                if ((transform.position - star.position).sqrMagnitude <= validDistance * validDistance)
                {
                    bools.Add(true);
                    break;//
                }
            }
            
        }
        if (bools.Count < stars.Count)//counts number of true statements and compares to number of stars, if not enough true then some stars are not in position
        {
            return false;
        }
        return true;
    }
}
