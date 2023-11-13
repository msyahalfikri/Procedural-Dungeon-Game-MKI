using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip[] audioClips;
    public DamageDealer playerDamageDealer;

    private void Start()
    {
        // Get the AudioSource component attached to the same GameObject
        audioSource = GetComponent<AudioSource>();
        playerDamageDealer = GetComponentInChildren<DamageDealer>();
    }

    private void Update()
    {
    }
    public void PlaySlashSound()
    {
        int random;
        if (playerDamageDealer.hitRegistered)
        {
            random = UnityEngine.Random.Range(0, 3);
        }
        else
        {
            random = UnityEngine.Random.Range(3, 10);
        }
        audioSource.clip = audioClips[random];
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
