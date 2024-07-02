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
            AudioManager.instance.StopLightsOnEvent();
            AudioManager.instance.PlayNoLightsEvent();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Lightbulb"))
        {
            AudioManager.instance.LightsOffSoundReset();
            AudioManager.instance.StopLightsOnEvent();
            UpdateLight();
            UpdateCollection(other);
            AudioManager.instance.PlayLightsOnEvent();
        }
    }

    private void UpdateLight()
    {
        timeLightbulbCollected = Time.time;
        fullBrightLightObject.SetActive(true);
    }
}
