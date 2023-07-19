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
    public TMPro.TextMeshProUGUI userText;

    public void Start()
    { chatBar.onValueChanged.AddListener(delegate { Typing(); }); }

    void Update()
    {
        
        if (Input.GetKeyUp(KeyCode.Return)) {
            chatBar.Select();
            SendText();
        }
    }

    public void Typing()
    { chatSounds.PlayOneShot(typingSound); }

    public void ClickSound()
    { chatSounds.PlayOneShot(click); }

    public void SendText()
    {
        if (chatBar.text.Length > 0) {
            Debug.Log(userText.text.ToString());
            chatBar.text = "";
        }
    }
}
