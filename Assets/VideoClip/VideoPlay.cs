using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoPlay : MonoBehaviour
{
    private VideoPlayer videoPlayer;
    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.loopPointReached += LoadScene;
    }

    // Update is called once per frame
    public void LoadScene(VideoPlayer vp)
    {
        SceneManager.LoadScene("Start");
    }
}
