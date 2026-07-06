using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    public void EnableAudio(bool enable)
    {
        audioSource.enabled = enable;
        audioSource.gameObject.SetActive(enable);
    }
}
