using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuteToggle : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    public void ToggleMute()
    {
        audioSource.mute = !audioSource.mute;
    }
}
