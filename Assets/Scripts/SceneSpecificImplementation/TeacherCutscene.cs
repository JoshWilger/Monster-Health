using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class TeacherCutscene : MonoBehaviour
{
    public string sceneName;
    public float startingDelay = 4f;
    public float endingDelay = 3f;
    private TextManager textManager;
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
        if (!TryGetComponent(out textManager))
        {
            Debug.Log($"No {textManager.GetType()} found for {name}");
        }
        else
        {
            StartCoroutine(BeginDialogue());
        }
    }

    IEnumerator BeginDialogue()
    {
        fadeToScene();
        yield return new WaitForSeconds(1.1f);

        textManager.AddPause(startingDelay);
        textManager.AddSpeakerChange("Narrator");
        textManager.AddMessage("[0.06]After overcoming the [0.1]Anxiety Monster, [0.06]a gentle, calming presence approaches you. [0.2][0.06]It’s your teacher, Miss Thompson, [0.15][0.06]who always knows how to make you feel safe.");
        textManager.AddSpeakerChange("Ms Thompson");
        textManager.AddCharacterChange("characters/teacher");
        textManager.AddMessage("[0.04]You did it![0.5] [0.04]I'm so proud of you.[0.5] [0.06]I know it wasn't easy, but you faced your fears with such [0.1]bravery.");
        textManager.AddSpeakerChange("You");
        textManager.AddCharacterChange("");
        textManager.AddMessage("[0.06]Thank you, Miss Thompson.[0.5] [0.06]It was really [0.2]scary, [0.06]but I feel better now.");
        textManager.AddSpeakerChange("Ms Thompson");
        textManager.AddCharacterChange("characters/teacher");
        textManager.AddMessage("[0.06]Whenever you feel overwhelmed, remember to use the tools you've learned.");
        textManager.AddMessage("[0.1]You're never truly alone. [0.06]You’ll always have yourself to guide you through anything.");
        textManager.AddPause(endingDelay);
        var endScene = new CustomFunction();
        endScene.custom_function = new CustomFunction.functionDelegate(EndScene);
        textManager.AddCustomFunction(endScene);
        textManager.PlayMessageQue();
        yield return false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void fadeToScene()
    {
        AudioManager.instance.PlayTransitionOutEvent();
        print("FADE!");
        Color initialColor = new Color(1, 1, 1, 1.0f);
        Color targetColor = new Color(initialColor.r, initialColor.g, initialColor.b, 0.0f); // Target color with 0 alpha

        // Use DOTween to tween the background color
        DOTween.To(() => initialColor, x => {
            initialColor = x;
            root_ve.Q<VisualElement>("WhiteCover").style.backgroundColor = new StyleColor(initialColor);
        }, targetColor, 1f).SetEase(Ease.Linear);
    }
    private void EndScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
