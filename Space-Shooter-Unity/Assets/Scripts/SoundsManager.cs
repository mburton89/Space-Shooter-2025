using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsManager : MonoBehaviour
{
    //Singleton
    public static SoundsManager Instance;

    //Audio Sources
    public AudioSource source1;
    public AudioSource source2;
    public AudioSource source3;
    public AudioSource source4;
    public AudioSource source5;

    //Volume Values
    public float gameVolume;
    public float maxVolume;

    //Pitch Values
    public float maxPitch;
    public float minPitch;

    //Currently Playing
    public AudioSource currentSFX;
    public AudioSource currentBGM;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayBGM(source1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Plays BGM
    public void PlayBGM(AudioSource music)
    {
        AudioSource tempMusic = Instantiate(music);
        currentBGM = tempMusic;
        currentBGM.volume = gameVolume;
        currentBGM.loop = true;
        currentBGM.Play();

        //AudioSource tempMusic = Instantiate(music);
        //tempMusic.volume = gameVolume;
        //tempMusic.loop = true;
        //tempMusic.Play();

        Debug.Log("Playing " + currentBGM.ToString());
    }

    // Pauses BGM
    public void PauseBGM()
    {
        currentBGM.Pause();
    }

    // Plays SFX at set pitch
    public void PlaySFX(AudioSource sfx)
    {
        AudioSource tempSFX = Instantiate(sfx);
        currentSFX = tempSFX;
        currentSFX.volume = gameVolume;
        currentSFX.Play();
    }

    // Plays SFX with varied pitch
    public void PlayVariedSFX(AudioSource sfx)
    {
        AudioSource tempSFX = Instantiate(sfx);
        currentSFX = tempSFX;
        currentSFX.volume = gameVolume;
        float tempPitch = Random.Range(minPitch, maxPitch);
        currentSFX.pitch = tempPitch;
        currentSFX.Play();
    }
}
