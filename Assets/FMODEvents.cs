using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FMODEvents : MonoBehaviour
{
    [field: Header("Player")]

    //[field: SerializeField] public EventReference Footsteps { get; private set; }

    [field: Header("UI")]
    //[field: SerializeField] public EventReference ChestOpen { get; private set; }

    [field: Header("Ambience")]
    //[field: SerializeField] public EventReference Campfire { get; private set; }

    [field: Header("Music")]
    [field: SerializeField] public EventReference mainMenuMusic { get; private set; }
    [field: SerializeField] public EventReference walkingMusic { get; private set; }
    [field: SerializeField] public EventReference endOfLifeMusic { get; private set; }
    [field: SerializeField] public EventReference miniGameMusic { get; private set; }
    [field: SerializeField] public EventReference lifeEventMusic { get; private set; }
    [field: SerializeField] public EventReference teacherMusic { get; private set; }
    public static FMODEvents instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one FMOD Events instance in the scene.");
        }
        instance = this;
    }
}
