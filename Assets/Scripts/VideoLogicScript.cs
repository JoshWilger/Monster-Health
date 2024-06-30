using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoLogicScript : MonoBehaviour
{
    public Camera cam;
    VideoPlayer player;
    Animator fadeAnimator;

    // Start is called before the first frame update
    void Start()
    {
        player = cam.GetComponent<VideoPlayer>();
        fadeAnimator = FindAnyObjectByType<Animator>();
        player.loopPointReached += EndReached;
        Pause();
        player.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void EndReached(UnityEngine.Video.VideoPlayer vp)
    {
        AudioManager.instance.PlayTransitionOutEvent();
        fadeAnimator.SetTrigger("Fade");
        StartCoroutine(Pause2());
    }

    IEnumerator Pause()
    {
        yield return new WaitForSeconds(1.5f);
    }

    IEnumerator Pause2()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("WalkingScene");
    }
}
