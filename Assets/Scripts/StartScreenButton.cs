using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreenButton : MonoBehaviour
{
	public AudioSource buttonSource;
	public AudioClip hoverSound;
	public AudioClip clickSound;

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
		//2 corresponds to VideoScene
		SceneManager.LoadScene(2);
	}
}
