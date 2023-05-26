using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class HintsController: MonoBehaviour
{
    [SerializeField] GameObject hintScreen;
    [SerializeField] float enableTime;
    [SerializeField] AudioSource videoMusic;
    [SerializeField] AudioSource gameMusic;
    [SerializeField] string url;

    [SerializeField] VideoPlayer player;

    void Start()
    {
        if (!string.IsNullOrEmpty(url))
        {
            player.url = url;
        }
        player.Play();
        StartCoroutine(EnableHint());
    }

    void Update()
    {
        //cheks if video ends
        if ((player.frame) > 0 && (player.isPlaying == false))
        {
            VideoEnd();
        }
    }

    //upon end of counter enables a hint screen at particular moment and pauses the video
    IEnumerator EnableHint()
    {
        yield return new WaitUntil(() => player.time>enableTime);
        hintScreen.SetActive(true);
        player.playbackSpeed = 0;
    }

    //upon end of video, disables video game object and activates the default scene sound
    public void VideoEnd()
    {
        enableSound();
        if (SceneManager.GetActiveScene().buildIndex != 3 || SceneManager.GetActiveScene().buildIndex != 4)
        {
            gameObject.SetActive(false);
            Debug.Log("Test");
        }
    }

    //activates the sound player
    private void enableSound()
    {
        videoMusic.Stop();
        if (gameMusic!= null)
        {
            gameMusic.Play();
        }
    }

    //upon click deactivates the hint and unpauses the video
    public void ContinueButton()
    {
        player.playbackSpeed = 1;
        hintScreen.SetActive(false);
    }
}
