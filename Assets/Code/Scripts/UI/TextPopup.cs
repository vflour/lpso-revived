using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextPopup : MonoBehaviour
{
    [SerializeField] private Transform TextPrefab;

    public TMPro.TMP_FontAsset Bluestone;
    public TMPro.TMP_FontAsset ArialBlack;
    public Material ABPink;
    public Material ABGreen;
    public Material BSPink;

    // x: x position of spawned text
    // y: y position of spawned text
    // text: the text you want displayed with the popup
    // font: the TMP font asset you want to use the popup
    // material: the specific material the TMP font asset will use
    // HoldFor: how long the text stays before moving upwards, in seconds
    // MoveUp: the speed the text moves up at
    // DisappearAfter: how long until the text starts lowering its alpha
    // DisappearSpeed: how quickly the text lowers its alpha
    // vvvvvvv

    public void SpawnText(float x, float y, string text, TMP_FontAsset font, Material m, int textSize, float HoldFor, float MoveUp, float DisappearAfter, float DisappearSpeed)
    {
        Transform PopupTransform = Instantiate(TextPrefab, new Vector3(x,y,1), Quaternion.identity, gameObject.transform);
        PopupText popupText = PopupTransform.GetComponent<PopupText>();
        popupText.textSize = textSize;
        popupText.HoldFor = HoldFor;
        popupText.MoveUp = MoveUp;
        popupText.DisappearAfter = DisappearAfter;
        popupText.DisappearSpeed = DisappearSpeed;
        popupText.Setup(text, font, m);
    }
}
