using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GemDivingLogic : MonoBehaviour
{
    [SerializeField] private GameObject[] Buttons;
    [SerializeField] private List<int> WinningSequence;
    [SerializeField] private List<int> PressedSequence;
    public GameObject InputBlock;

    [SerializeField] private GameObject WinScreen;
    [SerializeField] private Image WonItemImg;
    [SerializeField] private Sprite KibbleSprite;

    [SerializeField] private GameObject gemDiving;
    [SerializeField] private Skills skills;

    private void Start()
    {
        skills = GetComponent<Skills>();
        WinScreen.SetActive(false);
        gemDiving = transform.parent.gameObject;
        InputBlock.SetActive(true);
        for (int i = 0; i < 4; i++)
        {
            int random = Random.Range(0, Buttons.Length);
            WinningSequence.Add(random);
            StartCoroutine(ShowSequence());
        }
    }
    public IEnumerator ShowSequence()
    {
        yield return new WaitForSeconds(1);
        Buttons[WinningSequence[0]].GetComponent<GDButtons>().Active = true;
        yield return new WaitForSeconds(1.1f);
        Buttons[WinningSequence[1]].GetComponent<GDButtons>().Active = true;
        yield return new WaitForSeconds(1.1f);
        Buttons[WinningSequence[2]].GetComponent<GDButtons>().Active = true;
        yield return new WaitForSeconds(1.1f);
        Buttons[WinningSequence[3]].GetComponent<GDButtons>().Active = true;
        yield return new WaitForSeconds(1.1f);
        for (int i = 0; i < Buttons.Length; i++)
        {
            Buttons[i].GetComponent<GDButtons>().initialActive = false;
        }
        InputBlock.SetActive(false);
        yield break;
    }

    public void Pressed(int num)
    {
        PressedSequence.Add(num);
        Buttons[num].GetComponent<GDButtons>().Active = true;
        if (PressedSequence[PressedSequence.Count-1] != WinningSequence[PressedSequence.Count-1])
        {
            StartCoroutine(FailGame());
        }
        else
        {
            if (PressedSequence.Count == 4)
            {
                InputBlock.SetActive(true);
                Debug.Log("Won");
                WonGame();
            }
        }
    }

    void WonGame()
    {
        bool kibble = false;
        int a = Random.Range(0, 2);
        if (a == 0)
        {
            kibble = true;
        }
        StartCoroutine(EndScreen(kibble));
    }
    public IEnumerator EndScreen(bool wonKibble)
    {
        yield return new WaitForSeconds(1);
        WinScreen.SetActive(true);
        if (wonKibble)
        {
            WonItemImg.sprite = KibbleSprite;
            GameDataManager.Instance.AddKibble(10);
        }
        else
        {
            int b = Random.Range(57, 61);
            WonItemImg.sprite = GameDataManager.Instance.itemList[b].icon;
            GameDataManager.Instance.AddInventory(b);
        }
        skills.GemSkillUp();
        yield return new WaitForSeconds(5);
        GetComponentInParent<OverworldUI>().petUI.SetActive(true);
        GameDataManager.Instance.saveGame();
        Destroy(gemDiving);
        yield break;
    }
    public IEnumerator FailGame()
    {
        yield return new WaitForSeconds(1);
        GetComponentInParent<OverworldUI>().petUI.SetActive(true);
        Destroy(gemDiving);
        yield break;
    }
}
