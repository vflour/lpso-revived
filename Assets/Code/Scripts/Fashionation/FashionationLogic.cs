using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FashionationLogic : MonoBehaviour
{
    // UI references
    [SerializeField] private TextMeshProUGUI Timer;
    [SerializeField] private int TimeCount;
    [SerializeField] private float SecondCount;

    [SerializeField] private TextMeshProUGUI ScoreTxt;
    [SerializeField] private int Score;

    [SerializeField] private TextMeshProUGUI HeartTxt;
    [SerializeField] private Image HeartIcon;
    [SerializeField] private Sprite PinkHeart;
    [SerializeField] private Sprite GoldHeart;
    [SerializeField] private int HeartScore;

    [SerializeField] private TextMeshProUGUI LevelTxt;
    [SerializeField] private int Level;

    [SerializeField] private GameObject buffer;
    [SerializeField] private GameObject HighscorePanel;
    [SerializeField] private GameObject StartPanel;
    [SerializeField] private GameObject EndPanel;
    [SerializeField] private GameObject ContinuePanel;
    [SerializeField] private TextMeshProUGUI HighscoreTxt;
    [SerializeField] private TextMeshProUGUI TotalScoreTxt;
    [SerializeField] private TextMeshProUGUI KibbleEarnedTxt;
    [SerializeField] private TextMeshProUGUI TotalKibbleTxt;
    [SerializeField] private TextMeshProUGUI LvlCompletedTxt;

    [SerializeField] private MinigameCountdown mgCd;
    [SerializeField] private TextPopup textPopup;


    // logic
    [SerializeField] private GameObject InputBlock;
    [SerializeField] private Sprite[] AllItemSprites;
    [SerializeField] private List<int> CurrentItems;
    [SerializeField] private int MaxCurrentItems;
    [SerializeField] private GameObject FashionItem;
    [SerializeField] private Transform FashionItemParent;
    [SerializeField] private Transform SpawnPosTop;
    [SerializeField] private Transform SpawnPosBottom;

    [SerializeField] private GameObject Pet;
    public Transform[] PetSpawns;
    public bool[] PetSpawnOccupied = new bool[] {false, false, false, false, false};
    public Transform[] PetIdlePoints;
    public bool[] PetIdleOccupied = new bool[] { false, false, false, false, false };
    [SerializeField] private Transform PetParent;
    [SerializeField] private int PetWantsMax;
    [SerializeField] private GameObject thinkItem;

    public List<int> GoalInts;
    public List<GameObject> ActivePets;
    [SerializeField] private List<GameObject> GoalPets;
    public int clickedItemNumber = -1;

    public int HeartGoal;
    public int HeartBonus;
    public ProgressBar RedHearts;
    public ProgressBar GoldHearts;
    public int MaxPets;
    public bool GoldPet;

    public bool playing;


    void Start()
    {
        mgCd.textSize = 90;
        mgCd.x = transform.position.x;
        mgCd.y = transform.position.y;
        mgCd.promptReady = true;
        mgCd.textSpeed = 60;

        buffer.SetActive(true);
        HighscorePanel.SetActive(true);
        StartPanel.SetActive(true);
        EndPanel.SetActive(false);
        ContinuePanel.SetActive(false);
        InputBlock.SetActive(true);
        Level = 1;
        Score = 0;
        playing = false;
        HighscoreTxt.text = $"{GameDataManager.Instance.fshnhighscore:n0}";
    }

    private void Update()
    {
        if (!InputBlock.activeSelf)
        {
            if (playing)
            {
                if (TimeCount > 0)
                {
                    if (SecondCount > 0)
                    {
                        SecondCount -= Time.deltaTime;
                    }
                    else
                    {
                        SecondCount = 1;
                        TimeCount -= 1;
                        if (TimeCount > 9)
                        {
                            Timer.text = "0:" + TimeCount;
                        }
                        else
                        {
                            Timer.text = "0:0" + TimeCount;
                        }
                    }
                }
                else
                {
                    EndLevel();
                }
            }
        }
    }

    public void PressedPlay()
    {
        buffer.SetActive(false);

        LevelTxt.text = "Day : " + Level;
        TimeCount = 60;
        Timer.text = "1:00";
        SecondCount = 1;
        MaxCurrentItems = 3;
        PetWantsMax = 1;
        if (Level > 1)
        {
            if ((Level - 1) % 3 == 0)
            {
                if (MaxCurrentItems < 5)
                {
                    MaxCurrentItems++;
                    PetWantsMax++;
                }
            }
        }
        HeartTxt.text = "0";
        HeartScore = 0;
        HeartGoal = (Level + 1) * 5;
        HeartBonus = HeartGoal * 2;
        MaxPets = HeartBonus / 5;
        int random = Random.Range(0, 10);
        if (random < 4)
        {
            GoldPet = true;
        }
        CurrentItems.Clear();
        ChooseSprites();
        StartCoroutine(StartingSequence());
    }

    public IEnumerator StartingSequence()
    {
        textPopup.SpawnText(mgCd.x, mgCd.y, "Level " + Level, textPopup.Bluestone, textPopup.BSPink, mgCd.textSize, 2.5f, mgCd.textSpeed, 2.5f, 3f);
        yield return new WaitForSeconds(3);
        playing = true;
        mgCd.StartCountdown();
        StartCoroutine(SpawnTimer());
        yield return new WaitForSeconds(6);
        InputBlock.SetActive(false);
        yield return new WaitForSeconds(2);
        SpawnPet();
        yield break;
    }



    void EndLevel()
    {
        playing = false;
        InputBlock.SetActive(true);
        bool won = false;
        if (HeartScore >= HeartGoal)
        {
            won = true;
        }
        mgCd.startEnding(won);
        StartCoroutine(EndSequence());
    }

    public IEnumerator EndSequence()
    {
        yield return new WaitForSeconds(6);
        HideUI();
        buffer.SetActive(true);
        ContinuePanel.SetActive(true);
        LvlCompletedTxt.text = "Day : " + Level + " Completed!";
        Level += 1;
        for (int i = 0; i < FashionItemParent.childCount; i++)
        {
            Destroy(FashionItemParent.GetChild(i).gameObject);
        }
        RedHearts.GetComponent<Image>().fillAmount = 0;
        GoldHearts.GetComponent<Image>().fillAmount = 0;
        yield break;
    }
    
    public void EndGame()
    {
        if (!buffer.activeSelf)
        {
            buffer.SetActive(true);
        }
        ContinuePanel.SetActive(false);
        HighscorePanel.SetActive(true);
        EndPanel.SetActive(true);

        TotalScoreTxt.text = $"{Score:n0} Pts";
        int kibble = Score / 100;
        KibbleEarnedTxt.text = $"{kibble}";
        GameDataManager.Instance.AddKibble(kibble);
        TotalKibbleTxt.text = $"{GameDataManager.Instance.kibble:n0}";
        if (Score > GameDataManager.Instance.fshnhighscore)
        {
            GameDataManager.Instance.fshnhighscore = Score * 100;
        }
        HighscoreTxt.text = $"{Score * 100:n0}";
    }

    // spawns item every second
    public IEnumerator SpawnTimer()
    {
        while (playing == true)
        {
            SpawnItem(true);
            SpawnItem(false);
            yield return new WaitForSeconds(1);
        }
    }

    public void CheckClicked()
    {
        if (clickedItemNumber != -1)
        {
            if (GoalInts.Contains(clickedItemNumber))
            {
                for (int i = 0; i < ActivePets.Count; i++)
                {
                    if (ActivePets[i].GetComponent<FashionationPet>().WantedItems.Contains(clickedItemNumber))
                    {
                        if (!GoalPets.Contains(ActivePets[i]))
                        {
                            GoalPets.Add(ActivePets[i]);
                        }
                    }
                }
                if (GoalPets.Count > 1)
                {
                    // check which has the lowest hearts
                    int lowestHeart = 5;
                    List<GameObject> LowestPets = new List<GameObject>();
                    for (int i = 0; i < GoalPets.Count; i++)
                    {
                        if (GoalPets[i].GetComponent<FashionationPet>().Hearts < lowestHeart)
                        {
                            LowestPets.Clear();
                            LowestPets.Add(GoalPets[i]);
                            lowestHeart = GoalPets[i].GetComponent<FashionationPet>().Hearts;
                        }
                        else if (GoalPets[i].GetComponent<FashionationPet>().Hearts == lowestHeart)
                        {
                            LowestPets.Add(GoalPets[i]);
                        }
                    }
                    // check which has the lowest timer if multiple have same heart amt
                    if (LowestPets.Count > 1)
                    {
                        float lowestTimer = 100f;
                        for (int x = 0; x < LowestPets.Count; x++)
                        {
                            if (LowestPets[x].GetComponent<FashionationPet>().HeartTimer < lowestTimer)
                            {
                                lowestTimer = LowestPets[x].GetComponent<FashionationPet>().HeartTimer;
                            }
                            if (x == LowestPets.Count - 1)
                            {
                                LowestPets.Clear();
                                for (int y = 0; y < GoalPets.Count; y++)
                                {
                                    if (GoalPets[y].GetComponent<FashionationPet>().Hearts == lowestHeart && GoalPets[y].GetComponent<FashionationPet>().HeartTimer == lowestTimer)
                                    {
                                        LowestPets.Add(GoalPets[y]);
                                        Debug.Log("Added pet to LowestPets");
                                    }
                                }
                            }
                        }
                    }
                    Debug.Log("Pet with " + LowestPets[0] + " Heart(s) chosen");
                    // remove goalint from wanteditems
                    LowestPets[0].GetComponent<FashionationPet>().WantedItems.Remove(clickedItemNumber);
                }
                else
                {
                    if (GoalPets[0].GetComponent<FashionationPet>().WantedItems.Count > 1)
                    {
                        GoalPets[0].GetComponent<FashionationPet>().WantedItems.Remove(clickedItemNumber);
                        GoalInts.Remove(clickedItemNumber);

                        for (int x = 0; x < GoalPets[0].GetComponent<FashionationPet>().thinkItems.Count; x++)
                        {
                            if (GoalPets[0].GetComponent<FashionationPet>().thinkItems[x].GetComponent<fshnThinkItem>().ItemNum == clickedItemNumber)
                            {
                                GoalPets[0].GetComponent<FashionationPet>().thinkItems[x].GetComponent<Image>().sprite = null;
                                GoalPets[0].GetComponent<FashionationPet>().thinkItems.Remove(GoalPets[0].GetComponent<FashionationPet>().thinkItems[x]);
                            }
                        }
                    }
                    else
                    {
                        UpdateScore(GoalPets[0].GetComponent<FashionationPet>().Hearts + 1);
                        Destroy(GoalPets[0]);
                        GoalPets.Clear();
                        ActivePets.Remove(ActivePets[0]);
                        GoalInts.Remove(clickedItemNumber);
                        SpawnPet();
                    }
                }
            }
            clickedItemNumber = -1;
        }
        else
        {
            Debug.Log("No item clicked");
        }
    }

    void UpdateScore(int score)
    {
        HeartScore += score;
        float progress;
        if (HeartScore <= HeartGoal)
        {
            progress = (float)HeartScore / (float)HeartGoal;
            RedHearts.SetProgress(progress);
        }
        else
        {
            int heartScore = HeartScore - HeartGoal;
            if (HeartScore - score < HeartGoal)
            {
                int remainingScore = HeartGoal - (HeartScore - score);
                RedHearts.SetProgress(1);
                heartScore -= remainingScore;
            }
            progress = (float)heartScore / (float)HeartGoal;
            GoldHearts.SetProgress(progress);
        }
        if (HeartScore >=  HeartBonus)
        {
            EndLevel();
        }
        Score += (100 * score);
        HeartTxt.text = HeartScore.ToString();
        ScoreTxt.text = $"{Score:n0}";
    }

    // chooses which items will be on that level
    void ChooseSprites()
    {
        int random = Random.Range(0, AllItemSprites.Length);
        if (CurrentItems.Count < MaxCurrentItems)
        {
            if (CurrentItems.Count == 0)
            {
                CurrentItems.Add(random);
            }
            else
            {
                if (!CurrentItems.Contains(random))
                {
                    CurrentItems.Add(random);
                }
            }
            ChooseSprites();
        }
    }
    // sets item properties
    void SpawnItem(bool travelLeft)
    {
        if (travelLeft)
        {
            GameObject fashionItem = Instantiate(FashionItem, new Vector3(SpawnPosTop.position.x, SpawnPosTop.position.y), Quaternion.identity, FashionItemParent); ;
            int random = Random.Range(0, CurrentItems.Count);
            fashionItem.GetComponent<FashionationItem>().ItemNumber = CurrentItems[random];
            fashionItem.GetComponent<Button>().image.sprite = AllItemSprites[CurrentItems[random]];
        }
        else
        {
            GameObject fashionItem = Instantiate(FashionItem, new Vector3(SpawnPosBottom.position.x, SpawnPosBottom.position.y), Quaternion.identity, FashionItemParent); ;
            int random = Random.Range(0, CurrentItems.Count);
            fashionItem.GetComponent<FashionationItem>().right = true;
            fashionItem.GetComponent<FashionationItem>().ItemNumber = CurrentItems[random];
            fashionItem.GetComponent<Button>().image.sprite = AllItemSprites[CurrentItems[random]];
        }
    }
    void SpawnPet()
    {
        if (playing)
        {
            int spawn = Random.Range(0, PetSpawns.Length);
            if (PetSpawnOccupied[spawn])
            {
                Debug.Log("Spawn " + spawn + " taken. Retrying");
                SpawnPet();
            }
            else
            {
                GameObject pet = Instantiate(Pet, new Vector3(PetSpawns[spawn].position.x, PetSpawns[spawn].position.y), Quaternion.identity, PetParent);
                pet.GetComponent<FashionationPet>().GeneratePet();
                pet.GetComponent<FashionationPet>().SpawnPointNumber = spawn;
                PetSpawnOccupied[spawn] = true;
                Transform ItemParent = pet.gameObject.transform.Find("bubble/hgroup");
                for (int i = 0; i < PetWantsMax; i++)
                {
                    int item = Random.Range(0, CurrentItems.Count);
                    if (pet.GetComponent<FashionationPet>().WantedItems.Count == 0)
                    {
                        pet.GetComponent<FashionationPet>().WantedItems.Add(CurrentItems[item]);
                        GameObject ThinkItem = Instantiate(thinkItem, new Vector3(ItemParent.position.x, ItemParent.position.y), Quaternion.identity, ItemParent);
                        ThinkItem.GetComponent<fshnThinkItem>().ItemNum = CurrentItems[item];
                        ThinkItem.GetComponent<fshnThinkItem>().ItemSprite = AllItemSprites[CurrentItems[item]];
                        pet.GetComponent<FashionationPet>().thinkItems.Add(ThinkItem);
                    }
                    else
                    {
                        if (pet.GetComponent<FashionationPet>().WantedItems.Contains(CurrentItems[item]))
                        {
                            ;
                        }
                        else
                        {
                            pet.GetComponent<FashionationPet>().WantedItems.Add(CurrentItems[item]);
                            GameObject ThinkItem = Instantiate(thinkItem, new Vector3(ItemParent.position.x, ItemParent.position.y), Quaternion.identity, ItemParent);
                            ThinkItem.GetComponent<fshnThinkItem>().ItemNum = CurrentItems[item];
                            ThinkItem.GetComponent<fshnThinkItem>().ItemSprite = AllItemSprites[CurrentItems[item]];
                            pet.GetComponent<FashionationPet>().thinkItems.Add(ThinkItem);
                        }
                    }
                }
            }
        }
    }

    void HideUI()
    {
        buffer.SetActive(false);
        HighscorePanel.SetActive(false);
        StartPanel.SetActive(false);
        EndPanel.SetActive(false);
        ContinuePanel.SetActive(false);
    }
}
