using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChatTyping : MonoBehaviour
{
    public TMP_InputField chatBar;
    public AudioSource chatSounds;
    public AudioClip typingSound;
    public AudioClip click;

    public Button OKButton;
    public TMP_Text userText;

    public void Start()
    { chatBar.onValueChanged.AddListener(delegate { Typing(); }); }

    public void Typing()
    { chatSounds.PlayOneShot(typingSound); }

    public void ClickSound()
    { chatSounds.PlayOneShot(click); }


     //im fucking confused as to why this doesnt do what i want it to do, can someone help me
    public void SendText()
    {
        Debug.Log(userText.ToString());
        userText.SetText("");
    }
}
