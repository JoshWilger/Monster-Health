using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using FMOD;
using System.Text.RegularExpressions;

public class AudioManager : MonoBehaviour
{
    public bool mainmenuMusic;
    public bool walkingMusic;
    public bool lifeeventMusic;
    public bool endoflifeMusic;
    public bool minigameMusic;
    public bool teacherMusic;

    private List<EventInstance> eventInstances;

    private List<StudioEventEmitter> eventEmitters;

    private EventInstance walkingMusicEventInstance;

    private EventInstance mainMenuMusicEventInstance;

    private EventInstance miniGameMusicEventInstance;

    private EventInstance endOfLifeMusicEventInstance;

    private EventInstance lifeEventMusicEventInstance;

    [SerializeField] Transform testTransform;

    public static AudioManager instance;


    private void Awake()
    {
        if (instance != null)
        {
            //Debug.Log("Found more than one Audio Manager in the scene.");
        }
        instance = this;

        eventInstances = new List<EventInstance>();
        eventEmitters = new List<StudioEventEmitter>();
    }

    private void Start()
    {
        if (mainmenuMusic)
        {
            InitializeMainMenuMusic(FMODEvents.instance.mainMenuMusic);
        }
        else if (walkingMusic)
        {
            InitializeWalkingMusic(FMODEvents.instance.walkingMusic);
        }
        else if (lifeeventMusic)
        {
            InitializeLifeEventMusic(FMODEvents.instance.lifeEventMusic);
        }
        else if (endoflifeMusic)
        {
            InitializeEndOfLifeMusic(FMODEvents.instance.endOfLifeMusic);
        }
        else if (minigameMusic)
        {
            InitializeMiniGameMusic(FMODEvents.instance.miniGameMusic);
        }
        else if (teacherMusic)
        {
            InitializeTeacherMusic(FMODEvents.instance.teacherMusic);
        }
    }

    private void InitializeMainMenuMusic(EventReference mainMenuMusicEventReference)
    {
        mainMenuMusicEventInstance = CreateInstance(mainMenuMusicEventReference);
        mainMenuMusicEventInstance.start();
    }
    private void InitializeWalkingMusic(EventReference walkingMusicEventReference)
    {
        walkingMusicEventInstance = CreateInstance(walkingMusicEventReference);
        walkingMusicEventInstance.start();
    }
    private void InitializeEndOfLifeMusic(EventReference endOfLifeMusicEventReference)
    {
        endOfLifeMusicEventInstance = CreateInstance(endOfLifeMusicEventReference);
        endOfLifeMusicEventInstance.start();
    }

    private void InitializeLifeEventMusic(EventReference lifeEventMusicEventReference)
    {
        lifeEventMusicEventInstance = CreateInstance(lifeEventMusicEventReference);
        lifeEventMusicEventInstance.start();
    }

    private void InitializeMiniGameMusic(EventReference miniGameMusicEventReference)
    {
        miniGameMusicEventInstance = CreateInstance(miniGameMusicEventReference);
        miniGameMusicEventInstance.start();
    }
    private void InitializeTeacherMusic(EventReference miniGameMusicEventReference)
    {
        miniGameMusicEventInstance = CreateInstance(miniGameMusicEventReference);
        miniGameMusicEventInstance.start();
    }

    [field: SerializeField] public EventReference CoinEvent { get; private set; }
    [field: SerializeField] public EventReference HoopEvent { get; private set; }
    [field: SerializeField] public EventReference JumpingEvent { get; private set; }
    [field: SerializeField] public EventReference LandingEvent { get; private set; }
    [field: SerializeField] public EventReference LightsOnEvent { get; private set; }
    [field: SerializeField] public EventReference LightsOffEvent { get; private set; }
    [field: SerializeField] public EventReference ButtonEvent { get; private set; }
    [field: SerializeField] public EventReference BurstEvent { get; private set; }
    [field: SerializeField] public EventReference HitEvent { get; private set; }
    [field: SerializeField] public EventReference MonsterEvent { get; private set; }
    [field: SerializeField] public EventReference TransitionInEvent { get; private set; }
    [field: SerializeField] public EventReference TransitionOutEvent { get; private set; }
    [field: SerializeField] public EventReference WalkingEvent { get; private set; }
    [field: SerializeField] public EventReference ClimbingEvent { get; private set; }
    [field: SerializeField] public EventReference DialogueEvent { get; private set; }
    public void PlayCoinEvent()
    {
        
            RuntimeManager.PlayOneShot(CoinEvent);
        
    }

    public void PlayHoopEvent()
    {
        
           RuntimeManager.PlayOneShot(HoopEvent);
        
    }

    public void PlayJumpingEvent()
    {
        
            RuntimeManager.PlayOneShot(JumpingEvent);
        
    }

    public void PlayLandingEvent()
    {
        
            RuntimeManager.PlayOneShot(LandingEvent);
        
    }

    public void PlayLightsOnEvent()
    {
        
            RuntimeManager.PlayOneShot(LightsOnEvent);
        
    }

    public void PlayLightsOffEvent()
    {
        
            RuntimeManager.PlayOneShot(LightsOffEvent);
        
    }

    public void PlayButtonEvent()
    {

        
            RuntimeManager.PlayOneShot(ButtonEvent);
        
    }

    public void PlayBurstEvent()
    {

        
            RuntimeManager.PlayOneShot(BurstEvent);
        
    }

    public void PlayHitEvent()
    {

        
            RuntimeManager.PlayOneShot(HitEvent);
        
    }

    public void PlayMonsterEvent()
    {

        
            RuntimeManager.PlayOneShot(MonsterEvent);
        
    }

    public void PlayTransitionInEvent()
    {

        
            RuntimeManager.PlayOneShot(TransitionInEvent);
        
    }

    public void PlayTransitionOutEvent()
    {

        
            RuntimeManager.PlayOneShot(TransitionOutEvent);
        
    }

    public void PlayWalkingEvent()
    {

        
            RuntimeManager.PlayOneShot(WalkingEvent);
        
    }
    public void PlayClimbingEvent()
    {

        
            RuntimeManager.PlayOneShot(ClimbingEvent);
        
    }

    public void PlayDialoguegEvent()
    {

        
            RuntimeManager.PlayOneShot(DialogueEvent);
        
    }
    public EventInstance CreateInstance(EventReference eventReference)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
        eventInstances.Add(eventInstance);
        return eventInstance;
    }
    public StudioEventEmitter InitializeEventEmitter(EventReference eventReference, GameObject emitterGameObject)
    {
        StudioEventEmitter emitter = emitterGameObject.GetComponent<StudioEventEmitter>();
        emitter.EventReference = eventReference;
        eventEmitters.Add(emitter);
        return emitter;
    }

    private void CleanUp()
    {
        // stop and release any created instances 
        foreach (EventInstance eventInstance in eventInstances)
        {
            eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            eventInstance.release();
        }
        // stop all of the event emitters, because if we don't they may hang around in other scenes
        foreach (StudioEventEmitter emitter in eventEmitters)
        {
            emitter.Stop();
        }
    }

    private void OnDestroy()
    {
        CleanUp();
    }
}
