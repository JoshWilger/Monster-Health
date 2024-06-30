using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public float currentHealth = 3;
    public bool hasPopped = false;
    public Animator bubbleAnimator;
    public float deathDelay = 1.5f;

    private float timePlayerDied = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void playImaginaryFriendDialogue()
    {
        yield return new WaitForSeconds(1.1f);

        textManager.AddPause(startingDelay);

        textManager.AddSpeakerChange("Imaginary Friend");
	textManager.AddCharacterChange("characters/imaginary");
        textManager.AddMessage("[0.06]It's okay to feel defeated sometimes.[0.2][0.06]");
	
	textManager.AddMessage("Remember, [0.15][0.06]everything you lose is a step you take,[0.15][0.06] and every step you take is progress.[0.2][0.06] Keep going,[0.15][0.06] and you will find your way.");

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

        if (currentHealth <= 0)
        {
            if (timePlayerDied != 0 && Time.time - timePlayerDied > deathDelay)
            {
                AudioManager.instance.PlayMonsterEvent();
                Debug.Log("Player died. Restarting scene.");

		playImaginaryFriendDialogue();

                string currentSceneName = SceneManager.GetActiveScene().name;
                SceneManager.LoadScene(currentSceneName);
            }
            if (timePlayerDied == 0)
            {
                timePlayerDied = Time.time;
            }

        }
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
