using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlaygroundIntro : MonoBehaviour
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
            var endScene = new CustomFunction();
            endScene.custom_function = new CustomFunction.functionDelegate(EndScene);

            textManager.AddSpeakerChange("Narrator");
            textManager.AddMessage("[0.06]You find yourself on your school playground, [0.2][0.06]surrounded by swings, [0.15][0.06]slides, [0.15][0.06]and laughter. [0.2][0.06]The sight brings back memories, [0.2][0.04]both joyful [0.1]and anxious.");
            textManager.AddSpeakerChange("You");
            textManager.AddMessage("[0.06]The school playground is a place of fun and fear. [0.2][0.06]The swings, [0.15][0.06]the slides, [0.15][0.06]the laughter[0.2]... [0.06]but also the teasing, [0.15][0.06]the height [0.15][0.06]and the [0.08]loneliness.");
            textManager.AddMessage("[0.06]Everything feels so uncertain [0.1][0.06]and wrong when it was supposed to be just fun.");
            textManager.AddSpeakerChange("Imaginary Friend");
            textManager.AddCharacterChange("characters/imaginary");
            textManager.AddMessage("[0.06]Remember the good moments, and learn from the difficult ones.[0.2][0.1] This is what is going to shape who you are.");
            textManager.AddMessage("[0.08]It’s okay to feel scared. [0.2][0.06]Just keep moving, one step at a time. [0.2][0.06]You'll get through this.");
            textManager.AddMessage("[0.06]Each challenge you face is a chance to [0.1]grow. [0.2][0.06]Embrace them, [0.15][0.06]and you'll find your way.");
            textManager.AddCustomFunction(endScene);
            textManager.PlayMessageQue();
        }
    }

    private void EndScene()
    {
        SceneManager.LoadScene(sceneName);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
