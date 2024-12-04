using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySoundManager : MonoBehaviour
{
    

    private AudioSource audioSource;

    public AudioClip attackSound;

    public AudioClip damagedSound;

    public AudioClip DeathSound;

    public AudioClip potSound;

    public AudioClip axeSound;

    public AudioClip chestSound;

    public AudioClip lootSound;

    public AudioClip breakSound;

    private void Start() {
        audioSource=GetComponent<AudioSource>();
    }

    public void PlayAttackSoundDelay(){
        Invoke(nameof(PlayAttackSound),0.2f);
    }
    public void PlayAttackSound(){
        audioSource.PlayOneShot(attackSound);
    }
    public void PlayDamagedSoundDelay(){
        Invoke(nameof(PlayDamagedSound),0.2f);
    }

    public void PlayDamagedSound(){
        audioSource.PlayOneShot(damagedSound);
    }

    public void PlayDeathSound(){
        audioSource.PlayOneShot(DeathSound);
    }

    public void PlayPotSound(){
        audioSource.PlayOneShot(potSound);
    }

    public void PlayAxeSound(){
        audioSource.PlayOneShot(axeSound);
    }
    public void PlayChestSound(){
        audioSource.PlayOneShot(chestSound);
    }

    public void PlayLootSound(){
        //Debug.Log("Loot sound");
        audioSource.PlayOneShot(lootSound,0.5f);
    }

    public void PlayBreakSound(){
        audioSource.PlayOneShot(breakSound);
    }

}
