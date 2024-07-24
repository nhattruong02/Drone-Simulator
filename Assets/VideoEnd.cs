using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;

public class VideoEnd : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public GameObject gameObject;
    public GameObject finish;
    public GameObject Timer;
    public Logic logic;
    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        Timer.SetActive(false);
        logic.timerOn = false;
    }

    void Update()
    {
        if ( (float)videoPlayer.time+ 0.3 >= (float)videoPlayer.length)
        {
            gameObject.SetActive(false);
            finish.SetActive(true);
            logic.timerOn = true;
            logic.DisableMoveDrone();
        }
    }
}
