using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUtilities : MonoBehaviour
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
    public void SavePetLocation(GameObject player){
        GameDataManager.Instance.OldLocation = player.transform.position;
    }
}
