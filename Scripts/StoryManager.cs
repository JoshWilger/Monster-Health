using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UIElements;

public class StoryManager : MonoBehaviour
{

    private DecisionManager decision_manager;
    private TextManager text_manager;
    private VisualElement root_ve;

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] rootObjects = SceneManager.GetActiveScene().GetRootGameObjects();
        foreach (GameObject obj in rootObjects)
        {
            if (obj.name == "DecisionManager")
            {
                decision_manager = obj.GetComponent<DecisionManager>();
            }
            if (obj.name == "UIDocument")
            {
                text_manager = obj.GetComponent<TextManager>();
            }
            if (obj.name == "UIDocument")
            {
                root_ve = obj.GetComponent<UIDocument>().rootVisualElement;
            }
        }
        text_manager.AddSpeakerChange("Narrator");
        text_manager.AddMessage("Two shadowy figures,[0.1][0.06] their appearances indistinct yet comforting,[0.1][0.06] gently set you down on a vibrant path.");
        text_manager.AddMessage("Their eyes,[0.1][0.06] brimming with parental love,[0.1][0.06] follow your every move.");
        text_manager.AddMessage("Unbeknownst to you,[0.1][0.06] the world that surrounds is full of wonder,[0.1][0.06] a place where dreams and reality intertwine.");
        text_manager.AddMessage("Not always good and whimsical,[0.1][0.06] but always where you belong.");
        text_manager.AddSpeakerChange("Mom");
        text_manager.AddCharacterChange("characters/mom");
        text_manager.AddMessage("Welcome,[0.2][0.06] little one,[0.2][0.06] to this grand adventure called life.[0.3][0.06] You're small now,[0.1][0.06] but there's so much magic in your tiny hands and feet.");
        CustomFunction fade_CF = new CustomFunction();
        fade_CF.custom_function = fadeToTransparent;
        text_manager.AddCustomFunction(fade_CF);
        text_manager.AddPause(2f);
        text_manager.AddMessage("You’ve always been filled with courage and your heart with curiosity.[0.2][0.06] Remember that as you grow.");
        text_manager.AddMessage("You might feel small and powerless at times,[0.1][0.06] and scared of monsters when it gets dark,[0.1][0.06] but know that your size doesn't define your strength.");
        text_manager.AddSpeakerChange("Dad");
        text_manager.AddCharacterChange("characters/dad");
        text_manager.AddMessage("The world is vast and full of wonders,[0.1][0.06] waiting for you to uncover its secrets.[0.2][0.06] There will be moments of joy and moments of fear,[0.1][0.06] but each one will shape who you become.");
        text_manager.AddSpeakerChange("Narrator");
        text_manager.AddMessage("Press {W} to Walk");
        text_manager.PlayMessageQue();


    }

    public void fadeToTransparent()
    {
        print("FADE!");
        Color initialColor = new Color(0, 0, 0, 1.5f);
        Color targetColor = new Color(initialColor.r, initialColor.g, initialColor.b, 0f); // Target color with 0 alpha

        // Use DOTween to tween the background color
        DOTween.To(() => initialColor, x => {
            initialColor = x;
            root_ve.Q<VisualElement>("BlackCover").style.backgroundColor = new StyleColor(initialColor);
        }, targetColor, 2f).SetEase(Ease.Linear);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
