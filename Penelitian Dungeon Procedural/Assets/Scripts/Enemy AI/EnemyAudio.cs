using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudio : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip walkingSound;

    private void Start()
    {
        // Get the AudioSource component attached to the same GameObject
        audioSource = GetComponent<AudioSource>();
        // Assign the walking sound clip to the AudioSource
        audioSource.clip = walkingSound;
    }

    private void Update()
    {
    }
    public void PlayFootStep()
    {
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

}
