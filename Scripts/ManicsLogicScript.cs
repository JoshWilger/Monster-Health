using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class ManicsLogicScript : MonoBehaviour
{
    

    void Start()
    {
        StartCoroutine(Gemini());
    }

    void Update()
    {
        
    }

    IEnumerator Gemini()
    {
        yield return new WaitForSeconds(25);
        SceneManager.LoadScene("WalkingScene");
    }

}
