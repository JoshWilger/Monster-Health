using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoLogicScript : MonoBehaviour
{
    public Camera cam;
    VideoPlayer player;

    // Start is called before the first frame update
    void Start()
    {
        player = cam.GetComponent<VideoPlayer>();
        player.loopPointReached += EndReached;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void EndReached(UnityEngine.Video.VideoPlayer vp)
    {
        SceneManager.LoadScene("WalkingScene");
    }

}
