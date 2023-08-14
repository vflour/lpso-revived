using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PDasherNPC : MonoBehaviour
{
    public float timer;
    public float gameTick;

    public bool stable;
    public bool leaving;
    public bool clear;
    public GameObject heartSprite;
    public List<GameObject> hearts;
    public Transform heartContainer;

    public int maxHearts;
    public bool waiting;
    public bool foundexit = false;
    
    // Start is called before the first frame update
    void Start()
    {
        hearts = new List<GameObject>();

        for(int i=0;i < maxHearts;i++){ 
            GameObject tempHeart = Instantiate(heartSprite,heartContainer);
            hearts.Add(tempHeart);
            float offset = (float)i; 
            hearts[i].transform.localPosition = new Vector2(offset/8, 0);
        }
    }
    void doTick(){
        if(hearts.Count>0){
            GameObject heart = hearts[hearts.Count-1];
            Destroy(heart);
            hearts.Remove(heart);
            if (hearts.Count==0)
            {
                leaving = true;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(this.transform.position.x,this.transform.position.y,this.transform.position.y);
        if(stable && !clear && !waiting){
        timer = timer + Time.deltaTime;
        }
        if (timer > gameTick){
            timer = 1;
            doTick();
        }
    }
}
