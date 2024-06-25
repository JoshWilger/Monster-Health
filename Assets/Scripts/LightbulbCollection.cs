using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightbulbCollection : ItemCollection
{
    public float secondsOfLightEffect;
    public GameObject fullBrightLightObject;
    private float timeLightbulbCollected;

    // Update is called once per frame
    void Update()
    {
        if (Time.time - timeLightbulbCollected >= secondsOfLightEffect)
        {
            fullBrightLightObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Lightbulb"))
        {
            UpdateLight();
            UpdateCollection(other);
        }
    }

    private void UpdateLight()
    {
        timeLightbulbCollected = Time.time;
        fullBrightLightObject.SetActive(true);
    }
}
