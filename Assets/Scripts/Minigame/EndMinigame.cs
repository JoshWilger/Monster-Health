using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class EndMinigame : MonoBehaviour
{
    private VisualElement root_ve;
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] rootObjects = SceneManager.GetActiveScene().GetRootGameObjects();
        foreach (GameObject obj in rootObjects)
        {
            if (obj.name == "UIDocument")
            {
                root_ve = obj.GetComponent<UIDocument>().rootVisualElement;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void fadeToScene()
    {
        AudioManager.instance.PlayTransitionInEvent();
        AudioManager.instance.StopLightsOnEvent();
        print("FADE!");
        Color initialColor = new Color(1, 1, 1, 0.0f);
        Color targetColor = new Color(initialColor.r, initialColor.g, initialColor.b, 1.0f); // Target color with 0 alpha

        // Use DOTween to tween the background color
        DOTween.To(() => initialColor, x => {
            initialColor = x;
            root_ve.Q<VisualElement>("WhiteCover").style.backgroundColor = new StyleColor(initialColor);
        }, targetColor, 1f).SetEase(Ease.Linear);
    }
}
