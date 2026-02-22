using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using TMPro;

public class SoundManager : MonoBehaviour
{
    // NOTE that we use a separate audioSource (in EventSystem instead of Level1Manager) so that 
    // we can have a lower sfx volume than music

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip clickSound;
    public AudioClip clickDoubleSound;
    public AudioClip attackSound;
    public AudioClip hitSound;
    public AudioClip victorySound;
    public AudioClip lossSound;

    public void playClick()
    {
        if (audioSource && clickSound)
        {
            audioSource.PlayOneShot(clickSound);
        }
    }

    public void playDoubClick()
    {
        if (audioSource && clickDoubleSound) { audioSource.PlayOneShot(clickDoubleSound); }
    }

    public void playVictory() { if (audioSource && victorySound) { audioSource.PlayOneShot(victorySound); }  }
    public void playLoss() { if (audioSource && lossSound) { audioSource.PlayOneShot(lossSound); } }
    public void playattack() { if (audioSource && attackSound) { audioSource.PlayOneShot(attackSound); } }
    public void playhit() { if (audioSource && hitSound) { audioSource.PlayOneShot(hitSound); } }


}
