using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartScreenButton : MonoBehaviour
{
	public AudioSource buttonSource;
	public AudioClip hoverSound;
	public AudioClip clickSound;
	Animator fadeAnimator;

    private void Start(){
		fadeAnimator = FindAnyObjectByType<Animator>();
	}

    public void HoverSound() {
		AudioManager.instance.PlayLightsOffEvent();
		buttonSource.PlayOneShot(hoverSound);
	}

	public void ClickSound()
	{
		AudioManager.instance.PlayButtonEvent();
		buttonSource.PlayOneShot(clickSound);
	}

	public void LoadGame(){
		StartCoroutine(LoadFade());
	}

	IEnumerator LoadFade()
	{
        fadeAnimator.SetTrigger("FadeOut");
		AudioManager.instance.PlayTransitionInEvent();
		yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(2);
    }
}
