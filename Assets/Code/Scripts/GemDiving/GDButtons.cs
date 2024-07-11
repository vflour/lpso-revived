using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GDButtons : MonoBehaviour
{
    public GemDivingLogic GDLogic;
    public bool Active;
    public bool initialActive;
    private float SecondTimer;
    private float ScaleFactor;

    private void Start()
    {
        GDLogic = transform.parent.GetComponent<GemDivingLogic>();
        SecondTimer = 0.4f;
        ScaleFactor = 0.5f;
        initialActive = true;
    }
    void Update()
    {
        if (Active)
        {
            SetBlock(true);
            SecondTimer -= Time.deltaTime;
            if (SecondTimer > 0.2f)
            {
                transform.localScale += ScaleFactor * Time.deltaTime * new Vector3(1f, 1f, 0);
            }
            else
            {
                if (SecondTimer > 0.0f)
                {
                    transform.localScale -= ScaleFactor * Time.deltaTime * new Vector3(1f, 1f, 0);
                }
                else
                {
                    if (!initialActive)
                    {
                        SetBlock(false);
                    }
                    transform.localScale = new Vector3 (1f, 1f, 0);
                    if (SecondTimer < 0.0f)
                    {
                        SecondTimer = 0.4f;
                        Active = false;
                    }
                }
            }
        }
    }

    void SetBlock(bool set)
    {
        if (GDLogic.InputBlock.activeSelf != set)
        {
            GDLogic.InputBlock.SetActive(set);
        }
    }
}
