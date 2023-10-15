using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CloseButton : MonoBehaviour
{
    public void Close(){
        GameDataManager.Instance.saveGame();
        SceneManager.LoadScene("Waggington", LoadSceneMode.Single);
    }
    public void Reset(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
