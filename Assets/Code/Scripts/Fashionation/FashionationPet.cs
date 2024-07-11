using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FashionationPet : MonoBehaviour
{
    public List<int> WantedItems;
    public GameObject ThinkBubble;
    public List<GameObject> thinkItems;
    public Sprite[] PetSprites;
    private FashionationLogic fashionation;
    
    public int SpawnPointNumber;
    public int EndPointNumber;

    public int Hearts = 4;
    public GameObject[] HeartDisplay;
    public float HeartTimer = 9.0f;
    public float HeartTimerInit = 9.0f;

    private bool Moving;
    private int point;
    [SerializeField] private Vector3 startPos;
    [SerializeField] private Vector3 endPos;
    [SerializeField] private float lerpTime = 1.0f;

    private void Start()
    {
        HeartTimer = 9.0f;
        lerpTime = 1.0f;
        fashionation = GameObject.Find("MinigameHandler").GetComponent<FashionationLogic>();
        ThinkBubble.SetActive(false);
    }

    public void GeneratePet()
    {
        int random = Random.Range(0, PetSprites.Length);
        GetComponent<Button>().image.sprite = PetSprites[random];
    }

    public void LoseHeart()
    {
        HeartDisplay[Hearts].SetActive(false);
        Hearts -= 1;
    }

    private void Update()
    {
        if (Hearts > 0)
        {
            if (HeartTimer > 0)
            {
                HeartTimer -= Time.deltaTime;
                if (HeartTimer < HeartTimerInit/2)
                {
                    HeartDisplay[Hearts].transform.localScale -= 0.1f * Time.deltaTime * new Vector3(1f, 1f);
                    if (HeartTimer < 1.1f)
                    {
                        Image image = HeartDisplay[Hearts].GetComponent<Image>();
                        image.CrossFadeAlpha(0f, 1, false);
                    }
                }
            }
            else
            {
                HeartTimer = HeartTimerInit;
                LoseHeart();
            }
        }
        else
        {
            Debug.Log("Angry customer!");
            Destroy(gameObject);
        }

        if (Moving)
        {
            if (lerpTime > 0)
            {
                float initialLerpTime = lerpTime;
                transform.position = Vector3.Lerp(endPos, startPos, initialLerpTime);
                lerpTime -= Time.deltaTime;
            }
            else
            {
                Moving = false;
            }
        }
    }

    public void Clicked()
    {
        fashionation.ActivePets.Add(gameObject);
        fashionation.PetSpawnOccupied[SpawnPointNumber] = false;
        Move();
        for (int i = 0; i < WantedItems.Count; i++)
        {
            fashionation.GoalInts.Add(WantedItems[i]);
        }
        GetComponent<Button>().interactable = false;
        StartCoroutine(ThinkBubbleTimer());
    }

    void Move()
    {
        point = Random.Range(0, fashionation.PetIdlePoints.Length);
        if (fashionation.PetIdleOccupied[point])
        {
            Debug.Log("End point " + point + " occupied. Retrying");
            Move();
        }
        else
        {
            startPos = new Vector3(transform.position.x, transform.position.y);
            endPos = fashionation.PetIdlePoints[point].position;

            Moving = true;
        }
    }

    public IEnumerator ThinkBubbleTimer()
    {
        yield return new WaitForSeconds(4);
        ThinkBubble.SetActive(true);
        yield break;
    }
}
