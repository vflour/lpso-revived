using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VerticalSliceMenu : MonoBehaviour
{
    public void Awake(){
        Cursor.visible = false;
        //SceneManager.LoadScene("Overworld", LoadSceneMode.Additive);
        GameDataManager.Instance.loadGame();
    }

    public void GoToScene(string scene){
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }

    public void AddKibble(int amount){
        GameDataManager.Instance.kibble += amount;
    }
}
