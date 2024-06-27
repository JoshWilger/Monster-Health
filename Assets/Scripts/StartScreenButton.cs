using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreenButton : MonoBehaviour
{
	public void LoadGame(){
		//2 corresponds to VideoScene
		SceneManager.LoadScene(2);
	}
}
