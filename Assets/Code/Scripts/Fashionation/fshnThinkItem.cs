using UnityEngine;
using UnityEngine.UI;

public class fshnThinkItem : MonoBehaviour
{
    public int ItemNum;
    public Sprite ItemSprite;

    private void Start()
    {
        GetComponent<Image>().sprite = ItemSprite;
    }
}
