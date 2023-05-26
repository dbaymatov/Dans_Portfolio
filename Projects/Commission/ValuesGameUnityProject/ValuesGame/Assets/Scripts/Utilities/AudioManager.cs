using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource source;
    public static AudioManager Instance;
    // Start is called before the first frame update
    void Start()
    {
        DisableAudio();
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetSceneByName("Game1").isLoaded)
        {
            Debug.Log("Audio disabled");
            source.enabled = false;
            enabled = false;//disables it self
        }
    }

    public void DisableAudio()
    {
        source.enabled = false;
    }
    public void EnableAudio()
    {
        Debug.Log("EnableAudio");
        source.enabled = true;
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this);
    }

}
