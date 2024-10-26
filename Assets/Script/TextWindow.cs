using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class TextWindow : MonoBehaviour
{
    //テキストウィンドウ

    //ゲームマネージャー
    [SerializeField] private GameManager gameManager;

    [NonSerialized] public DialogueText dialogueText;
    [SerializeField] private GameObject panelObject;
    [SerializeField] private TextMeshProUGUI speakerNameText;
    [SerializeField] private TextMeshProUGUI speakerDialogueText;
    int index = 0;
    bool isTyping;

    private Coroutine dialogueCoroutine;


    void Update()
    {
        //テキストを表示
        if(gameManager.playerController == GameManager.PlayerController.TextWindowMode)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // 会話文表示処理を実行する
                displayDialogueText();

            }
        }   
    }

    /// <summary>
    /// 会話文表示処理
    /// </summary>
    public void displayDialogueText()
    {
        //パネルが非表示なら表示する
        if (!panelObject.activeSelf)
        {
            panelObject.SetActive(true);
        }
        
        //scriptableObjectの情報をパネルに表示する
        if (dialogueText.textInfomations.Length > index)
        {
            //話者の名前を表示
            speakerNameText.text = dialogueText.textInfomations[index].speakerName;

            if (!isTyping)
            {
                dialogueCoroutine = StartCoroutine(TypeDialogueText(dialogueText.textInfomations[index].paragraphs));
            }
            else
            {
                stopTyping();
            }
        }
        else
        {
            //会話が終了したためパネルを非表示にする
            speakerNameText.text = "";
            speakerDialogueText.text = "";
            panelObject.SetActive(false);

            //状態を初期化して照準モードにする
            gameManager.playerController = GameManager.PlayerController.ReticleMode;

            index = 0;
        }
        
    }

    /// <summary>
    /// ダイアログのテキストを一文字づつ表示していきます
    /// </summary>
    /// <param name="paragraph">会話の1文</param>
    /// <returns>IEnumerator</returns>
    private IEnumerator TypeDialogueText(string paragraph)
    {
        string displayText = "";
        isTyping = true;
        int colorIndex = 0;
        foreach (char c in paragraph)
        {
            colorIndex++;
            speakerDialogueText.text = paragraph;
            displayText = speakerDialogueText.text.Insert(colorIndex, "<color=#00000000>");
            speakerDialogueText.text = displayText;
            yield return new WaitForSeconds(0.05f);
        }
        isTyping = false;
        //次の会話文インデックスを進める
        index++;
    }

    /// <summary>
    /// 1文字づつ表示を終了しテキスト全文を表示する
    /// </summary>
    private void stopTyping()
    {
        StopCoroutine(dialogueCoroutine);
        //未表示の会話文を全文表示する
        speakerDialogueText.text = dialogueText.textInfomations[index].paragraphs;
        isTyping = false;
        //次の会話文インデックスを進める
        index++;
    }
}
