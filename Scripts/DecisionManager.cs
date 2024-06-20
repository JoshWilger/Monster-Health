using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecisionList
{
    public delegate void decisionDelegate();
    public string text_1;
    public string text_2;
    public string text_3;
    public decisionDelegate decision1 = null;
    public decisionDelegate decision2 = null;
    public decisionDelegate decision3 = null;
}

public class CustomFunction
{
    public delegate void functionDelegate();
    public functionDelegate custom_function;
}

public class DecisionManager : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
