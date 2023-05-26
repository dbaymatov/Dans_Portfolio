using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyLightDissappear : MonoBehaviour
{
    public bool increaseIntensity;
    [SerializeField] Light light;
    [SerializeField] Material KeyMaterial;
    [SerializeField] float lightRate;
    // Start is called before the first frame update
    void Start()
    {
        EventManager.EndGame.AddListener(CorotineStarter);
        increaseIntensity = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (increaseIntensity)
        {
            Color emissiveColor = Color.yellow;
            light.intensity += Time.deltaTime * lightRate;
           // light.range = light.range * lightRate;
            KeyMaterial.SetColor("_EmissiveColor", emissiveColor * lightRate);
        }
    }

    public void CorotineStarter()
    {
        increaseIntensity = true;
        StartCoroutine(GlowAndDissapear());
    }

    IEnumerator GlowAndDissapear()
    {
        yield return new WaitForSeconds(7);
        gameObject.SetActive(false);
    }

}
