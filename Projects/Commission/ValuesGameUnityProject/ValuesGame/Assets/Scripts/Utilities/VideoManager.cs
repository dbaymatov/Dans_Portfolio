using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using System;

public class VideoManager : MonoBehaviour
{
    [SerializeField] VideoPlayer videoPlayer;
    [SerializeField] GameObject hintScreen;
    [SerializeField] string url;
    [SerializeField] Animator animator;
    [SerializeField] GameObject loader;

    private void Start()
    {
        if (!string.IsNullOrEmpty(url))
        {
            videoPlayer.url = url;
        }
        videoPlayer.loopPointReached += EndReached;
        videoPlayer.prepareCompleted += PrepareCompleted;

        videoPlayer.Prepare();
    }

    private void PrepareCompleted(VideoPlayer source)
    {
        Debug.Log("prep completed");
        animator.SetTrigger("Crossfade_End");
        videoPlayer.Play();
    }

    void EndReached(UnityEngine.Video.VideoPlayer vp)
    {

        loader.SetActive(true);
        SceneManager.LoadScene("Game1");

    }

    public void ContinueButton()
    {
        videoPlayer.playbackSpeed = 1;
        hintScreen.SetActive(false);
    }
}
