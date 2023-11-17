using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudio : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip[] audioClips;

    private void Start()
    {
        // Get the AudioSource component attached to the same GameObject
        audioSource = GetComponent<AudioSource>();
        // Assign the walking sound clip to the AudioSource
    }

    private void Update()
    {
    }
    public void PlayFootStep()
    {
        audioSource.clip = audioClips[0];
        // If the enemy is walking and the walking sound is not already playing, play the sound
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
        // If the enemy is not walking and the walking sound is playing, stop the sound
        else if (!audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
    public void PlayRoarSound()
    {
        audioSource.clip = audioClips[1];
        // If the enemy is walking and the walking sound is not already playing, play the sound
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
        // If the enemy is not walking and the walking sound is playing, stop the sound
        else if (!audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    //TO BE IMPLEMENTED
    public void PlayPunchSound()
    {
        //play punching sounds. 
    }

}
