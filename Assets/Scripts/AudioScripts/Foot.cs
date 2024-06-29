using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foot : MonoBehaviour
{
    public void PlayFootStep()
    {
        AudioManager.instance.PlayWalkingEvent();
    }
}
