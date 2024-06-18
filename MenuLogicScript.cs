using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuLogicScript : MonoBehaviour
{
    public GameObject menuUI;
    public GameObject settingsUI;
    public GameObject help;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            ExitSettings();
            ExitHelp();
        }
    }

    //Enter Settings
    public void EnterSettings()
    {
        settingsUI.SetActive(true);
    }

    //Exit Settings
    public void ExitSettings()
    {
        settingsUI.SetActive(false);
    }

    //Enter Help
    public void EnterHelp()
    {
        help.SetActive(true);
    }

    //Exit Help
    public void ExitHelp()
    {
        help.SetActive(false);
    }

    //Start game
    public void StartGame()
    {
        SceneManager.LoadScene("WalkingScene");
    }

    //Enter highscores screen
    //public void Highscores()
    //{
    //    SceneManager.LoadScene("Highscores");
    //}

}
