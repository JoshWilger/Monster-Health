using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class SpringObs : ObstacleScript
{
    private bool on = false;
    private float counter = -(3*Mathf.PI) / 2;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (on)
        {
            //transform.RotateAround(pivot.transform.position, Vector3.forward, Mathf.Sin(GetCounter()));
            float x_ax = (Mathf.Rad2Deg * ((Mathf.Sin(GetCounter())) / 5f)) + 90;
            transform.eulerAngles = new Vector3(x_ax, -90f, -90f);
            //print(Mathf.Sin(GetCounter()));
        }
    }

    public override void Begin()
    {
        on = true;
    }

    public override void End()
    {
        
    }

    private float GetCounter()
    {
        counter += (Mathf.PI / 2) * Time.deltaTime;
        if (counter >= Mathf.PI / 2)
        {
            counter = -(3*Mathf.PI) / 2;
        }
        return counter;
    }
}
