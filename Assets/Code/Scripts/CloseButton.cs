using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CloseButton : MonoBehaviour
{
    public void Close(){
        SceneManager.LoadScene("Vertical Slice Menu", LoadSceneMode.Single);
    }
    public void Reset(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
