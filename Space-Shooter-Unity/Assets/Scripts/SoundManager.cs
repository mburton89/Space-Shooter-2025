using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public AudioSource pewSound;
    public AudioSource explosionSound;

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

    public void PlayPewSound()
    {
        float newPitch = Random.Range(0.9f, 1.1f);
        pewSound.pitch = newPitch;
        pewSound.Play();
    }

    public void PlayExplosionSound()
    {
        float newPitch = Random.Range(0.9f, 1.1f);
        explosionSound.pitch = newPitch;
        explosionSound.Play();
    }
}
