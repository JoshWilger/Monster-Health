using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfLifeCutscene : MonoBehaviour
{
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
            Color friendColor = new Color32(152, 146, 134, 255);

            textManager.AddSpeakerChange("Imaginary Friend", friendColor);
            textManager.AddMessage("[0.06]You have walked the path of life[0.2][0.06], and now you may rest eternally.");
            textManager.AddMessage("[0.06]You don’t have to be scared any longer. [0.06]You faced your fears and learned from them. [0.2][0.06]You became the person you are. [0.2][0.06]You changed the world of those around you.");
            textManager.AddMessage("[0.1]You created a beautiful life with every step you took. [0.06]Look back on your journey and see how far you've come.");
            textManager.PlayMessageQue();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
