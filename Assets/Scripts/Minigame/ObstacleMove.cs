using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMove : MonoBehaviour
{
    public float obstacle_speed = 10f;
    public float z_reset_trigger = -9.92f;
    public float z_reset = 61.08f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = transform.localPosition + (Vector3.back * obstacle_speed) * Time.deltaTime;
        if (transform.localPosition.z <= z_reset_trigger)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z + z_reset*2);
            //transform.localPosition = transform.localPosition + (Vector3.back * obstacle_speed) * Time.deltaTime;
        }
    }
}
