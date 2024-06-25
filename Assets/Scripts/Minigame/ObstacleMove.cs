using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMove : MonoBehaviour
{
    public float obstacle_speed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + (Vector3.back * obstacle_speed) * Time.deltaTime;
        if (transform.position.z <= -9.92)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 62.08f);
            transform.position = transform.position + (Vector3.back * obstacle_speed) * Time.deltaTime;
        }
    }
}
