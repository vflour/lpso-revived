using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FashionationItem : MonoBehaviour
{
    public int ItemNumber;
    [SerializeField] private float DestroyTimer = 7.0f;
    public bool right = false;
    public float Speed = 150f;
    private FashionationLogic fashionation;

    private void Start()
    {
        Speed = 150f;
        fashionation  = GameObject.Find("MinigameHandler").GetComponent<FashionationLogic>();
    }

    private void Update()
    {
        if (fashionation.playing)
        {
            if (DestroyTimer > 0)
            {
                DestroyTimer -= Time.deltaTime;
                if (!right)
                {
                    transform.position += new Vector3(Speed, 0f) * Time.deltaTime;
                }
                else
                {
                    transform.position += new Vector3(-Speed, 0f) * Time.deltaTime;
                }
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    public void Clicked()
    {
        fashionation.clickedItemNumber = ItemNumber;
        fashionation.CheckClicked();
        Destroy(gameObject);
    }
}
