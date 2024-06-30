using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class TeacherCutscene : MonoBehaviour
{
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
        Color teacherColor = new Color32(152, 146, 134, 255);
        Color playerColor = new Color32(80, 83, 89, 255);

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
        textManager.PlayMessageQue();
        yield return false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void fadeToScene()
    {
        print("FADE!");
        Color initialColor = new Color(1, 1, 1, 1.0f);
        Color targetColor = new Color(initialColor.r, initialColor.g, initialColor.b, 0.0f); // Target color with 0 alpha

        // Use DOTween to tween the background color
        DOTween.To(() => initialColor, x => {
            initialColor = x;
            root_ve.Q<VisualElement>("WhiteCover").style.backgroundColor = new StyleColor(initialColor);
        }, targetColor, 1f).SetEase(Ease.Linear);
    }
}
