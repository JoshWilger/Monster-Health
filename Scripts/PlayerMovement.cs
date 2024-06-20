using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody playerRb;

    // Start is called before the first frame update
    void Start()
    {
        if (!TryGetComponent<Rigidbody>(out playerRb))
        {
            Debug.Log($"No {playerRb.GetType()} found for {playerRb.name}");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
