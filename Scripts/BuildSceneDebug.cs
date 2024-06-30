using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildSceneDebug : MonoBehaviour
{
    private void OnEnable()
    {
        Debug.Log("Debug script got enabled!");
    }
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Debug script got started!");
        Debug.Log($"{name} position: {transform.position}");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
