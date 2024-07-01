using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeIn : MonoBehaviour
{
    private Animator fadeAnimator;

    // Start is called before the first frame update
    void Start()
    {
        if (TryGetComponent(out fadeAnimator))
        {
            fadeAnimator.SetTrigger("FadeIn");
        }
        else
        {
            Debug.Log($"No {fadeAnimator.GetType()} found for {name}");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
