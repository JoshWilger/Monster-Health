using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementMultiplier = 5f;
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
        float xInput = Input.GetAxisRaw("Horizontal");
        float zInput = Input.GetAxisRaw("Vertical");

        playerRb.AddForce(new Vector3(xInput, 0, zInput) * movementMultiplier);
    }
}
