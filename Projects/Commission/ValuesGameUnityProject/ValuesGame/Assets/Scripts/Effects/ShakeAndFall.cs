using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeAndFall : MonoBehaviour
{
    bool falling = false;
    bool returnPos = false;
    bool shaking = false;
    float speed = 30;
    float timer = 0.1f;
    Vector3 originalPos;
    Vector3 randomPos;

    private void Awake()
    {
        originalPos = transform.position;
    }


    // Update is called once per frame
    void FixedUpdate()
    {

        if (shaking)
        {
            if (returnPos)
            {
                transform.position += (originalPos - transform.position) * Time.deltaTime*speed;//platform returning to original loactions
            }
            else
            {
                transform.position += randomPos * speed * Time.deltaTime;//platform going to random location

                //transform.position += (randomPos - transform.position) * Time.deltaTime*speed;
            }

            TimerSetup();
        }
        if (falling)
        {
            transform.position += new Vector3(0, -4 * Time.deltaTime * 1, 0);
        }
    }

    void TimerSetup()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer = 0.1f;
            returnPos = !returnPos;
            randomPos = Random.insideUnitSphere;
        }
    }

    //will start falling and disable the gameobject
    IEnumerator UnloadPlatform()
    {
        yield return new WaitForSeconds(10);
        falling = true;
        shaking = false;
        yield return new WaitForSeconds(20);
        gameObject.SetActive(false);
    }

    public void StartShaking()
    {
        shaking = true;
        StartCoroutine(UnloadPlatform());

    }
}
