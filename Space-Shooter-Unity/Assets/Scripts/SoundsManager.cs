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
    public AudioSource source6;
    public AudioSource source7;
    public AudioSource source8;
    public AudioSource source9;
    public AudioSource source10;    
    public AudioSource source11;
    public AudioSource source12;
    public AudioSource source13;
    public AudioSource source14;
    public AudioSource source15;
    public AudioSource source16;
    public AudioSource source17;
    public AudioSource source18;
    public AudioSource source19;
    public AudioSource source20;
    public AudioSource source21;
    public AudioSource source22;
    public AudioSource source23;
    public AudioSource source24;
    public AudioSource source25;
    public AudioSource source26;
    public AudioSource source27;
    public AudioSource source28;

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
        //PlaySFX(source11);
        PlayBGM(source18);
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
