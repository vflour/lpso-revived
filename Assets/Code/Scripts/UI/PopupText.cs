using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopupText : MonoBehaviour
{
    // font attributes
    private TMPro.TextMeshProUGUI textMesh;
    public Color textColor;
    public int textSize;

    //movement attributes
    public float HoldFor;
    public float MoveUp;
    public float DisappearAfter;
    public float DisappearSpeed;

    public float ExpandTimer = 0.1f;
    public float ExpandTimerInitial;
    public float ShrinkTimer = 0.1f;
    public Vector3 initialsize;

    private void Awake()
    {
        textMesh = transform.GetComponent<TMPro.TextMeshProUGUI>();
        ExpandTimerInitial = ExpandTimer;
    }

    public void Setup(string text, TMP_FontAsset font, Material m)
    {
        textMesh.text = text;
        textMesh.font = font;
        textMesh.fontMaterial = m;
        textColor = textMesh.color;
        textMesh.fontSize = textSize;
        textMesh.transform.localScale = new Vector3(0.2f, 0.2f);
    }

    private void Update()
    {
        if (HoldFor > 0) // holds the text in the same place before moving up

        {
            HoldFor -= Time.deltaTime;
        }
        else // moves text up
        {
            transform.position += new Vector3(0, MoveUp) * Time.deltaTime;
        }

        DisappearAfter -= Time.deltaTime; // how long until object begins to fade
        if (DisappearAfter <= 0)
        {
            textColor.a -= DisappearSpeed * Time.deltaTime;
            textMesh.color = textColor;
            if (textColor.a < 0)
            {
                Destroy(gameObject);
            }
        }
        ExpandTimer -= Time.deltaTime;
        
        if (ExpandTimer > 0)
        {
            float increaseAmt = 3.5f;
            textMesh.transform.localScale += increaseAmt * Time.deltaTime * new Vector3(1f,1f,0f);
        }
        else
        {
            ShrinkTimer -= Time.deltaTime;
            if (ShrinkTimer > 0)
            {
                float decreaseAmt = 1f;
                textMesh.transform.localScale -= decreaseAmt * Time.deltaTime * new Vector3(1f,1f,0f);
            }
        }
    }
}
