using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSoundHandler : MonoBehaviour
{
    // Start is called before the first frame update
    private AudioSource audioSource;
    public AudioClip ActivateSound;

    private void Start() {
        audioSource=GetComponent<AudioSource>();
    }

    public void PlayAttackSoundDelay(){
        Invoke(nameof(PlayActivateSound),0.2f);
    }
    public void PlayActivateSound(){
        audioSource.PlayOneShot(ActivateSound);
    }
}
