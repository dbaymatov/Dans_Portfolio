using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoEndDisable : MonoBehaviour
{
    [SerializeField] GameObject disableLoadScreen;

    public VideoPlayer videoPlayer;
    public bool nextScne;
    public bool EndGameScene;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        if ((videoPlayer.frame) > 0 && (videoPlayer.isPlaying == false))
        {
            if (disableLoadScreen != null)
            {
                disableLoadScreen.SetActive(false);
            }

            if (EndGameScene)
            {
                gameObject.SetActive(false);
            }

            if (nextScne)
            {
                SceneManager.LoadScene(4);
            }
        }
    }
}
