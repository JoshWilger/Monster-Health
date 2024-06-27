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
		buttonSource.PlayOneShot(hoverSound);
	}

	public void ClickSound()
	{
		buttonSource.PlayOneShot(clickSound);
	}

	public void LoadGame(){
		//2 corresponds to VideoScene
		SceneManager.LoadScene(2);
	}
}
