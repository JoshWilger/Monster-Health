using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public float currentHealth = 3;
    public Animator bubbleAnimator;
    public float deathDelay = 1.5f;

    private float timePlayerDied = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (bubbleAnimator != null)
        {
            bubbleAnimator.SetFloat("health", currentHealth);
        }

        if (currentHealth <= 0)
        {
            if (timePlayerDied != 0 && Time.time - timePlayerDied > deathDelay)
            {
                Debug.Log("Player died. Restarting scene.");
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
            currentHealth--;
        }
        Debug.Log($"Health: {currentHealth}");
    }
}
