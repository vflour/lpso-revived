using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PDasherLogic : MonoBehaviour
{

    public GameObject[] prefab;
    public GameObject[,] level = new GameObject[6,10];
    public GameObject[] npcstart = new GameObject[10];
    public GameObject cursor;
    public GameObject[] npc;
    public GameObject exit;
    public GameObject spawn;
    public AudioSource clickSFX;
    public AudioSource popSFX;
    public AudioSource succesSFX;

    public TMP_Text scoreHearts;
    public TMP_Text scorePoints;
    public TMP_Text HighscoreText1;
    public TMP_Text HighscoreText2;

    private float gameTimer;
    public float gameTickTime = 20;

    public float tilewidth;
    public float tileheight;
    public float tilewidthp;
    public float tileheightp;
    public float width;
    public float height;
    public float xOrigin;
    public float yOrigin;
    public float ppu;
    public bool stable;

    public int hearts = 0;
    public int points = 0;

    public float screenScale;

    public List<GameObject> path;
    public List<GameObject> goalPath;
    public List<GameObject> toRemove;
    public bool foundPath;
    public int walkingNPC;
    public Vector2 selection;
    private bool sidewalk;
    private Vector2 goalPos;
    private Vector2 sidewalkTile;

    public TMP_Text KibbleWon;
    public TMP_Text KibbleTotal;
    public TMP_Text TotalScoreText;

    public GameObject startScreen;
    public GameObject scoreScreen;
    private bool gameRunning = false;
    public void toggleMute()
    {
        if (AudioListener.volume == 1)
        {
            AudioListener.volume = 0;
        }
        else
        {
            AudioListener.volume = 1;
        }
    }
    void Start()
    {
        HighscoreText1.SetText(GameDataManager.Instance.pdhighscore.ToString());
        HighscoreText2.SetText(GameDataManager.Instance.pdhighscore.ToString());
    }

    public void StartGame(){
        gameRunning = true;
        startScreen.SetActive(false);
        scoreScreen.SetActive(false);
        SpawnNPC();
    }

    void SpawnTile(int x){
        Transform tempTransform = this.transform;
        GameObject spawnedItem = Instantiate(prefab[(int)Mathf.Ceil(Random.Range(0,prefab.GetLength(0)))],tempTransform);
        spawnedItem.transform.position = screenPos(x,level.GetLength(1));
        level[x,level.GetLength(1)-1] = spawnedItem;
        level[x,level.GetLength(1)-1].GetComponent<LogLogic>().y = level.GetLength(1)-1;
        level[x,level.GetLength(1)-1].GetComponent<LogLogic>().x = x;
        level[x,level.GetLength(1)-1].GetComponent<LogLogic>().stable = false;
    }
    void SpawnNPC(){
        Transform tempTransform = this.transform;
        int currentCell = (int)Random.Range(0,npcstart.GetLength(0)-1);
        int foundAmount = 0;
        for(int i = 0; i < npcstart.Length; i++){
            if (npcstart[i]){
                foundAmount++;
            }
        }
        if(!npcstart[currentCell]){
            GameObject currentNPC = Instantiate(npc[(int)(Random.Range(0,npc.GetLength(0)))],tempTransform);
            currentNPC.transform.position = spawn.transform.position;
            npcstart[currentCell] = currentNPC;
        } else if(foundAmount < npcstart.Length-1) {
            SpawnNPC();
        }
    }
    
    void addHearts(int amount){
        for(int i=1;i<=amount;i++){
            hearts += 1;
            if(hearts <= 10){
                points += 100;
            } else {
                points += 150;
            }
        }
        scoreHearts.SetText(hearts.ToString());
        scorePoints.SetText(points.ToString());
    }

    void doTick(){
            SpawnNPC();
    }
    // Update is called once per frame

    void UpdateNPCs(){
        for (int i = 0; i < npcstart.GetLength(0); i++){
            if(foundPath || !stable){
                if (npcstart[i] != null){
                    npcstart[i].GetComponent<PDasherNPC>().waiting = true;
                }
            } else {
                if (npcstart[i] != null){
                    npcstart[i].GetComponent<PDasherNPC>().waiting = false;
                }
            }
        }
    }

    void SetScore(){
      gameRunning = false;
      TotalScoreText.SetText(points.ToString());

      int kibble = points/100;
      KibbleWon.SetText(kibble.ToString());
      GameDataManager.Instance.AddKibble(kibble);
      KibbleTotal.SetText(GameDataManager.Instance.kibble.ToString());

      if(points> GameDataManager.Instance.pdhighscore){
        GameDataManager.Instance.pdhighscore = points;  
      }
      HighscoreText1.SetText(GameDataManager.Instance.pdhighscore.ToString());
      HighscoreText2.SetText(GameDataManager.Instance.pdhighscore.ToString());
    }

    void Update()
    {
        if (gameRunning){
        UpdateNPCs();
        if(stable && !foundPath) {
            gameTimer = gameTimer + Time.deltaTime;
        }
        if (gameTimer > gameTickTime){
            gameTimer = 0;
            doTick();
        }
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        selection = tilePos(mousePos.x,mousePos.y);
        cursor.transform.position = screenPos(selection.x,selection.y);
        cursor.active = !(selection.x < 0 || selection.x >= level.GetLength(0) || selection.y < 0 || selection.y >= level.GetLength(1));
         if (Input.GetMouseButtonDown(0) && !foundPath && stable){
            if(selection.x >= 0 && selection.x <= level.GetLength(0)-1 && selection.y >= 0 && selection.y <= level.GetLength(1)-1){
                Transform tempTransform = level[(int)selection.x, (int)selection.y].transform;
                GameObject oldObject = level[(int)selection.x, (int)selection.y];
                level[(int)selection.x, (int)selection.y] = Instantiate(level[(int)selection.x, (int)selection.y].GetComponent<LogLogic>().nextState,tempTransform);
                level[(int)selection.x, (int)selection.y].gameObject.transform.SetParent(this.transform);
                level[(int)selection.x, (int)selection.y].gameObject.transform.position = screenPos(selection.x,selection.y);
                level[(int)selection.x, (int)selection.y].gameObject.GetComponent<LogLogic>().x = (int)selection.x;
                level[(int)selection.x, (int)selection.y].gameObject.GetComponent<LogLogic>().y = (int)selection.y;
                Destroy(oldObject);
                Refresh();
                clickSFX.Play();
            }
         }

         for(int i = 0; i < npcstart.GetLength(0); i++){
            if(npcstart[i] != null && !npcstart[i].GetComponent<PDasherNPC>().stable){
                npcstart[i].transform.position = Vector2.MoveTowards(npcstart[i].transform.position, screenPos(-2,i),1f * Time.deltaTime);
                if(Vector2.Distance(npcstart[i].transform.position, screenPos(-2,i)) < 0.1f){
                    npcstart[i].GetComponent<PDasherNPC>().stable = true;
                }
            }

            if(npcstart[i] != null && npcstart[i].GetComponent<PDasherNPC>().leaving){
                npcstart[i].transform.position = Vector2.MoveTowards(npcstart[i].transform.position, spawn.transform.position,1f * Time.deltaTime);
                if(Vector2.Distance(npcstart[i].transform.position, spawn.transform.position) < 0.1f){
                    Destroy(npcstart[i]);
                    scoreScreen.SetActive(true);
                    SetScore();
                }
            }

            if(npcstart[i] != null && npcstart[i].GetComponent<PDasherNPC>().foundexit){
                npcstart[i].transform.position = Vector2.MoveTowards(npcstart[i].transform.position, exit.transform.position,1f * Time.deltaTime);
                if(Vector2.Distance(npcstart[i].transform.position, exit.transform.position)  < 0.5f){
                    Destroy(npcstart[i]);
                }
            }
         }
         
        if (!foundPath) {
        stable = true;
        for (int i = 0; i < level.GetLength(0) ; i++)
        {
            for (int j = 0; j < level.GetLength(1) ; j++)
            {
                if (j == level.GetLength(1)-1 && level[i, j] == null && ((level[i, j-1] != null && level[i, j-1].GetComponent<LogLogic>().stable) || level[i, j-1] == null)){
                    SpawnTile(i);
                }
            if (j > 0 && level[i, j - 1] == null && level[i, j] != null){
                //level[i,j].transform.position = level[i, j - 1].transform.position; 
                level[i,j - 1] = level[i, j];
                level[i,j - 1].GetComponent<LogLogic>().y = j-1;
                level[i,j - 1].GetComponent<LogLogic>().x = i;
                level[i,j - 1].GetComponent<LogLogic>().stable = false;
                level[i,j] = null;
            }
            if (level[i, j] != null){
                level[i,j].transform.position = Vector2.MoveTowards(level[i,j].transform.position, screenPos(level[i,j].GetComponent<LogLogic>().x,level[i,j].GetComponent<LogLogic>().y),1f * Time.deltaTime);
                if(Vector2.Distance(level[i,j].transform.position, screenPos(level[i,j].GetComponent<LogLogic>().x,level[i,j].GetComponent<LogLogic>().y))  < 0.1f){
                    level[i,j].GetComponent<LogLogic>().stable = true;
                } else {
                    level[i,j].GetComponent<LogLogic>().stable = false;
                    stable = false;
                }
            } else {
                    stable = false;
                }
            }
        }
        }
        if (stable && !foundPath) {
            
            for (int i = 0; i < level.GetLength(0) ; i++)
            {
            for (int j = 0; j < level.GetLength(1) ; j++)
            {
                ResetTile(level[i, j],i,j);
                bool CanWalk = false;
                if (i==0){
                    if(level[i, j].GetComponent<LogLogic>().West && npcstart[j] != null && npcstart[j].GetComponent<PDasherNPC>().stable) {
                        CanWalk = true;
                        level[i, j].GetComponent<LogLogic>().isStart = true;
                    } else {
                        level[i, j].GetComponent<LogLogic>().isStart = false;
                    }
                }
                    if(i > 0 && level[i - 1, j].GetComponent<LogLogic>().East && level[i - 1, j].GetComponent<LogLogic>().Walkable && level[i,j].GetComponent<LogLogic>().West) {
                        CanWalk = true;
                        level[i, j].GetComponent<LogLogic>().breadcrumb.Add(level[i - 1, j]);
                    }
                    if(i < level.GetLength(0)-1 && level[i + 1, j].GetComponent<LogLogic>().West && level[i + 1, j].GetComponent<LogLogic>().Walkable && level[i,j].GetComponent<LogLogic>().East) {
                        CanWalk = true;
                        level[i, j].GetComponent<LogLogic>().breadcrumb.Add(level[i + 1, j]);
                    }
                    if(j > 0 && level[i, j - 1].GetComponent<LogLogic>().North && level[i, j - 1].GetComponent<LogLogic>().Walkable && level[i,j].GetComponent<LogLogic>().South) {
                        CanWalk = true;
                        level[i, j].GetComponent<LogLogic>().breadcrumb.Add(level[i, j - 1]);
                    }
                    if(j < level.GetLength(1)-1 && level[i, j + 1].GetComponent<LogLogic>().South && level[i, j + 1].GetComponent<LogLogic>().Walkable && level[i,j].GetComponent<LogLogic>().North) {
                        CanWalk = true;
                        level[i, j].GetComponent<LogLogic>().breadcrumb.Add(level[i, j + 1]);
                    }
                    level[i, j].GetComponent<LogLogic>().Walkable = CanWalk;

                    if(i==level.GetLength(0)-1 && level[i, j].GetComponent<LogLogic>().Walkable && level[i, j].GetComponent<LogLogic>().East){
                        path.Clear();
                        GameObject currentTile = level[i,j];
                        currentTile.GetComponent<LogLogic>().isEnd = true;
                        currentTile.GetComponent<LogLogic>().isPath = true;
                        WalkCrumb(currentTile, 100);
                    }
            }
            }

        }
        if(foundPath){
            if (goalPath.Count > 0){
                GameObject nextTile = goalPath[0];
                npcstart[walkingNPC].GetComponent<PDasherNPC>().clear = true;
                npcstart[walkingNPC].transform.position =  Vector2.MoveTowards(npcstart[walkingNPC].transform.position, screenPos(nextTile.GetComponent<LogLogic>().x,nextTile.GetComponent<LogLogic>().y),1f * Time.deltaTime);
                if(Vector2.Distance(npcstart[walkingNPC].transform.position, screenPos(nextTile.GetComponent<LogLogic>().x,nextTile.GetComponent<LogLogic>().y))  < 0.1f){
                 toRemove.Add(nextTile);
                 goalPath.Remove(nextTile);
                 sidewalkTile = new Vector2(nextTile.GetComponent<LogLogic>().x+1,nextTile.GetComponent<LogLogic>().y);
                }
            } else {
                if (!sidewalk){
                    goalPos = screenPos(sidewalkTile.x,sidewalkTile.y);
                    npcstart[walkingNPC].transform.position = Vector2.MoveTowards(npcstart[walkingNPC].transform.position,goalPos,1f * Time.deltaTime);
                    if(Vector2.Distance(npcstart[walkingNPC].transform.position, goalPos)  < 0.1f){
                        sidewalk = true;
                    }
                } else {
                        npcstart[walkingNPC].GetComponent<PDasherNPC>().foundexit = true;
                        foundPath = false;
                        popSFX.Play();
                        for(int i = toRemove.Count-1; i >= 0; i--)
                        {
                         Destroy(toRemove[i]);
                        }
                        addHearts(npcstart[walkingNPC].GetComponent<PDasherNPC>().hearts.Count);
                        toRemove.Clear();
                        Refresh();
                    //}
                }
            }
        }
        }
    }

    void Refresh(){
        for (int i = 0; i < level.GetLength(0) ; i++)
                    {
                        for (int j = 0; j < level.GetLength(1) ; j++)
                        {
                        level[i, j].GetComponent<LogLogic>().Walkable = false;
                        level[i, j].GetComponent<LogLogic>().breadcrumb.Clear();
                        }
                    }
                    path.Clear();
                    goalPath.Clear();
                    sidewalk = false;
    }
    void ResetTile(GameObject tile, int x, int y){
        if(tile != null && !foundPath){
            tile.GetComponent<LogLogic>().tempPath = false;
            tile.GetComponent<LogLogic>().isPath = false;
            tile.GetComponent<LogLogic>().level = 0;
            tile.GetComponent<LogLogic>().foundPath = false;
            tile.GetComponent<LogLogic>().breadcrumb.Clear();
            tile.GetComponent<LogLogic>().nextTiles.Clear();
            tile.GetComponent<LogLogic>().x = x;
            tile.GetComponent<LogLogic>().y = y;
        }
    }

    void WalkCrumb(GameObject Bread, int count){
        path.Add(Bread);
        Bread.GetComponent<LogLogic>().tempPath = true;
        Bread.GetComponent<LogLogic>().level = count;
        Bread.GetComponent<LogLogic>().foundPath = true;
        if (Bread.GetComponent<LogLogic>().isStart){
            goalPath.Clear();
            toRemove.Clear();
            WalkToEnd(Bread);
            succesSFX.Play();
            goalPath.Add(path[0]);
            foundPath = true;
            walkingNPC = Bread.GetComponent<LogLogic>().y;
        }
        if (Bread.GetComponent<LogLogic>().breadcrumb.Count > 0 ){
            for (int i = 0; i < Bread.GetComponent<LogLogic>().breadcrumb.Count; i++){
                if(!Bread.GetComponent<LogLogic>().breadcrumb[i].GetComponent<LogLogic>().foundPath){
                    Bread.GetComponent<LogLogic>().breadcrumb[i].GetComponent<LogLogic>().nextTiles.Add(Bread);
                    WalkCrumb(Bread.GetComponent<LogLogic>().breadcrumb[i], count - 1);
                }
            }
        }
    }

    void WalkToEnd(GameObject Tile){
        for (int i = 0; i < Tile.GetComponent<LogLogic>().nextTiles.Count; i++){
            if(Tile.GetComponent<LogLogic>().nextTiles[i].GetComponent<LogLogic>().level > Tile.GetComponent<LogLogic>().level){
                goalPath.Add(Tile);
                Tile.GetComponent<LogLogic>().isPath = true;
                WalkToEnd(Tile.GetComponent<LogLogic>().nextTiles[i]);
            }
        }
    }

    Vector2 screenPos(float x, float y){
        float xpos = (x+xOrigin) * (tilewidth/2) - (y+yOrigin) * (tilewidth/2);
        float ypos = (x+xOrigin) * (tileheight/2) + (y+yOrigin) * (tileheight/2);
        Vector2 position = new Vector2(xpos,ypos);
        return position;
    }

     Vector2 tilePos(float x, float y){
        Vector2 position;
        position.x = Mathf.Round((x / (tilewidth/2) + y / (tileheight/2))/2 -xOrigin);
        position.y = Mathf.Round((y / (tileheight/2) - x / (tilewidth/2))/2 -yOrigin);
        return position;
    }
}
