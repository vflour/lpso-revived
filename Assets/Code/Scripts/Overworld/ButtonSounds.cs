using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSounds : MonoBehaviour
{
    public AudioSource ButtonSound;
    public AudioClip hoverSound;
    public AudioClip pressedSound;

    public void playHover()
    { ButtonSound.PlayOneShot(hoverSound); }

    public void playClick()
    { ButtonSound.PlayOneShot(pressedSound); }
}
