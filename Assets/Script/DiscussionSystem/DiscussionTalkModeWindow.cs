using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static DiscussionEventManager;
using static TalkCameraManager;

public class DiscussionTalkModeWindow : MonoBehaviour
{
    //議論時のテキストウィンドウ

    //ゲームマネージャー
    [SerializeField] private GameManager gameManager;
    [SerializeField] private DiscussionManager discussionManager;
    [SerializeField] private DiscussionEventManager discussionEventManager;

    public DialogueText dialogueText;

    [SerializeField] private GameObject panelObject;
    [SerializeField] private TextMeshProUGUI speakerNameText;
    [SerializeField] private TextMeshProUGUI speakerDialogueText;
    int index = 0;
    bool isTyping;

    private Coroutine dialogueCoroutine;

    //会話終了後の移行先
    public enum TalkFinish
    {
        DiscussionMode,
        AdventureMode,
    }
    [NonSerialized] public TalkFinish talkFinish;

    //会話カメラの辞書
    private Dictionary<string, DiscussionEventSet> talkSetDictionary = new();


    void Start()
    {
        //辞書の初期化
        for (int loop = 0; loop < discussionEventManager.discussionEventSet.Length; loop++)
        {
            var talkset = discussionEventManager.discussionEventSet[loop];
            talkSetDictionary.Add(talkset.textinfo, talkset);
        }
    }

    void Update()
    {
        //テキストを表示
        if (gameManager.playerController == GameManager.PlayerController.DiscussionMode && discussionManager.discussion == DiscussionManager.DiscussionMode.Talk)
        {
            //会話文表示処理を実行する
            ProgressText();
        }
    }

    //会話文をセット
    public void TextSet(DialogueText talkText)
    {
        dialogueText = talkText;
    }

    //会話の進行
    public void ProgressText()
    {
        //パネルが非表示なら表示する
        if (!panelObject.activeSelf)
        {
            panelObject.SetActive(true);

            //最初の会話の表示
            DisplayDialogueText();
        }

        if (/*Input.GetKeyDown(KeyCode.Space)*/gameManager.KeyInputSpace())
        {
            //次の会話の表示
            DisplayDialogueText();
        }
    }

    //会話文表示処理
    public void DisplayDialogueText()
    {
        //scriptableObjectの情報をパネルに表示する
        if (dialogueText.textInfomations.Length > index)
        {
            //テキスト番号のキーから現在のカメラ設定を取得
            var talkSet = talkSetDictionary[dialogueText.textinfo];
            var talkEvent = talkSet.discussionEvent.OnTalkEvent[index];

            //話者の名前を表示
            speakerNameText.text = dialogueText.textInfomations[index].speakerName;

            if (!isTyping)
            {
                //会話イベントを発生
                talkEvent.Invoke();

                dialogueCoroutine = StartCoroutine(TypeDialogueText(dialogueText.textInfomations[index].paragraphs));
            }
            else
            {
                StopTyping();
            }
        }
        else
        {
            //会話が終了したためパネルを非表示にする
            speakerNameText.text = "";
            speakerDialogueText.text = "";
            panelObject.SetActive(false);

            //状態を初期化してノンストップ議論か探索に移行する
            switch (talkFinish)
            {
                case TalkFinish.DiscussionMode:
                    discussionManager.ShootingInit();
                    Debug.Log("議論に移行");
                    break;
                case TalkFinish.AdventureMode:
                    gameManager.ReticleModeChange();
                    Debug.Log("照準操作探索に移行");
                    break;
                default:
                    gameManager.ReticleModeChange();
                    Debug.Log("照準操作探索に移行");
                    break;
            }

            index = 0;
        }
    }

    //ダイアログのテキストを一文字づつ表示
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

    //1文字づつ表示を終了しテキスト全文を表示する
    private void StopTyping()
    {
        StopCoroutine(dialogueCoroutine);
        //未表示の会話文を全文表示する
        speakerDialogueText.text = dialogueText.textInfomations[index].paragraphs;
        isTyping = false;
        //次の会話文インデックスを進める
        index++;
    }
}
