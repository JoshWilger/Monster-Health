using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeacherCutscene : MonoBehaviour
{
    public string sceneName;
    private TextManager textManager;

    // Start is called before the first frame update
    void Start()
    {
        if (!TryGetComponent(out textManager))
        {
            Debug.Log($"No {textManager.GetType()} found for {name}");
        }
        else
        {
            Color teacherColor = new Color32(152, 146, 134, 255);
            Color playerColor = new Color32(80, 83, 89, 255);
            var endScene = new CustomFunction();
            endScene.custom_function = new CustomFunction.functionDelegate(EndScene);

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
            textManager.AddCustomFunction(endScene);
            textManager.PlayMessageQue();
        }
    }

    private void EndScene() {
        SceneManager.LoadScene(sceneName);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
