using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSounds : MonoBehaviour
{
    public AudioSource ButtonSound;
    public AudioClip hoverSound;
    public AudioClip pressedSound;
    public AudioClip openPDA;

    public GameObject childImage;

    public void playHover()
    { ButtonSound.PlayOneShot(hoverSound); }

    public void playClick()
    { ButtonSound.PlayOneShot(pressedSound); }

    public void playPDA()
    { ButtonSound.PlayOneShot(openPDA); }

    public void EnlargeImage()
    { childImage.transform.localScale = new Vector3(1.1f, 1.1f, 0); }

    public void ShrinkImage()
    { childImage.transform.localScale = new Vector3(1, 1, 0); }
}
