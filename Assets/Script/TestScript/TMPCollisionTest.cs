using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TMPCollisionTest : MonoBehaviour
{

    //TMP_TextUtilities ModifyMesh リッチテキストタグ
    [SerializeField] private TMP_Text text;

    private void Start()
    {
        text.GetComponent<TMP_Text>().text = "<size=45><color=#41A2E1>" + text.GetComponent<TMP_Text>().text + " <size=35><color=#ffa500>" + "hope";
    }

    private void Update()
    {
        var index = TMP_TextUtilities.FindIntersectingWord(text, Input.mousePosition, Camera.main);

        if (index < 0)
            return;

        var wordInfo = text.textInfo.wordInfo[index];

        Debug.Log(wordInfo.GetWord());
    }
}
