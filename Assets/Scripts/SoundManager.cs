using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource ShootSound;
    public AudioSource FreezeSound;
    public AudioSource FireCometStartSound;
    public AudioSource FireCometEndSound;
    public AudioSource ShockwaveSound;
    public AudioSource BrewingSound;
    public AudioSource PickupSound;

    public void PlayShootSound()
    {
        ShootSound.Play();
    }
    public void PlayFreezeSound()
    {
        FreezeSound.Play();
    }
    public void PlayFireCometStartSound()
    {
        FireCometStartSound.Play();
    }
    public void PlayFireCometEndSound()
    {
        FireCometEndSound.Play();
    }
    public void PlayShockwaveSound()
    {
        ShockwaveSound.Play();
    }

    public void PlayBrewingSound()
    {
        BrewingSound.Play();
    }

    public void PlayPickupSound()
    {
        PickupSound.Play();
    }


}
