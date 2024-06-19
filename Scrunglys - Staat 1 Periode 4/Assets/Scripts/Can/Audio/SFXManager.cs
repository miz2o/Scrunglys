using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager instance;
    [SerializeField] private AudioSource soundFXObject;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void PlaySFXClip(AudioClip audioClip, Transform spawnTransform, float volume, float pitch)
    {
        AudioSource audioSource = Instantiate(soundFXObject, spawnTransform.position, quaternion.identity);

        audioSource.clip = audioClip;

        audioSource.volume = volume;
        audioSource.pitch = pitch;

        audioSource.Play();
        
        float clipLength = audioSource.clip.length;

        Destroy(audioSource.gameObject, clipLength);
    }

    public void PlayMulitpleSFXClip(AudioClip[] audioClip, Transform spawnTransform, float volume, float pitch)
    {   
        int rand = UnityEngine.Random.Range(0, audioClip.Length);
        AudioSource audioSource = Instantiate(soundFXObject, spawnTransform.position, quaternion.identity);

        audioSource.clip = audioClip[rand];

        audioSource.volume = volume;
        audioSource.pitch = pitch;

        audioSource.Play();
        
        float clipLength = audioSource.clip.length;

        Destroy(audioSource.gameObject, clipLength);
    }
}
