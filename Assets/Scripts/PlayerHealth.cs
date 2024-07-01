using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PlayerHealth : MonoBehaviour
{
    public float currentHealth = 3;
    public bool hasPopped = false;
    public Animator bubbleAnimator;
    public float deathDelay = 1.5f;
    public float startingDelay = 4f;
    public float endingDelay = 3f;

    private bool end = false;

    private float timePlayerDied = 0;
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
                textManager = obj.GetComponent<TextManager>();
            }
            if (obj.name == "UIDocument")
            {
                root_ve = obj.GetComponent<UIDocument>().rootVisualElement;
            }
        }
    }

    private IEnumerator PlayImaginaryFriendDialogue()
    {
        yield return new WaitForSeconds(1.1f);

        textManager.AddSpeakerChange("Imaginary Friend");
	    textManager.AddCharacterChange("characters/imaginary");
        textManager.AddMessage("[0.06]It's okay to feel defeated sometimes.[0.2][0.06]");
	    textManager.AddMessage("Remember, [0.15][0.06]everything you lose is a step you take,[0.15][0.06] and every step you take is progress.[0.2][0.06] Keep going,[0.15][0.06] and you will find your way.");
        var resetScene = new CustomFunction();
        resetScene.custom_function = new CustomFunction.functionDelegate(ResetScene);
        textManager.AddCustomFunction(resetScene);
        textManager.PlayMessageQue();
        yield return false;
    }

    // Update is called once per frame
    void Update()
    {
        if (bubbleAnimator != null)
        {
            if(currentHealth == 0 && !hasPopped)
            {
                hasPopped = true;
                AudioManager.instance.PlayBurstEvent();
            }
            bubbleAnimator.SetFloat("health", currentHealth);
        }

        if (currentHealth <= 0 && !end)
        {
            if (timePlayerDied != 0 && Time.time - timePlayerDied > deathDelay)
            {
                AudioManager.instance.PlayMonsterEvent();

                StartCoroutine(PlayImaginaryFriendDialogue());
                Debug.Log("Player died. Restarting scene.");
            }
            AudioManager.instance.PlayMonsterEvent();
            Color initialColor = new Color(0, 0, 0, 0.0f);
            Color targetColor = new Color(initialColor.r, initialColor.g, initialColor.b, 1.0f);
            //root_ve.Q<VisualElement>("BlackCover").style.backgroundColor = new StyleColor(targetColor);
            root_ve.Q<VisualElement>("BlackCover").visible = true;
            StartCoroutine(PlayImaginaryFriendDialogue());
            Debug.Log("Player died. Restarting scene.");
            if (timePlayerDied == 0)
            {
                timePlayerDied = Time.time;
            }
            end = true;
        }
    }

    private void ResetScene()
    {
        Color initialColor = new Color(0, 0, 0, 0.0f);
        Color targetColor = new Color(initialColor.r, initialColor.g, initialColor.b, 0.0f);
        //root_ve.Q<VisualElement>("BlackCover").style.backgroundColor = new StyleColor(targetColor);
        root_ve.Q<VisualElement>("BlackCover").visible = false;
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            AudioManager.instance.PlayHitEvent();
            currentHealth--;
        }
        Debug.Log($"Health: {currentHealth}");
    }
}
