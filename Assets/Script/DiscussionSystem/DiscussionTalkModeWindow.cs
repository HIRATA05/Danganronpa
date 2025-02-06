using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static DiscussionEventManager;
using static TalkCameraManager;

public class DiscussionTalkModeWindow : MonoBehaviour
{
    //議論時のテキストウィンドウ

    //ゲームマネージャー
    [SerializeField] private GameManager gameManager;
    [SerializeField] private DiscussionManager discussionManager;
    [SerializeField] private DiscussionEventManager discussionEventManager;

    /*[NonSerialized] */public DialogueText dialogueText;

    [SerializeField] private GameObject panelObject;
    [SerializeField] private TextMeshProUGUI speakerNameText;
    [SerializeField] private TextMeshProUGUI speakerDialogueText;
    int index = 0;
    bool isTyping;

    [SerializeField, Header("話者の名前画像")] private Image speakerName;
    [SerializeField] private Sprite speakerNameDetective;//探偵
    [SerializeField] private Sprite speakerNameLacky;//幸運
    [SerializeField] private Sprite speakerNameHacker;//ハッカー
    [SerializeField] private Sprite speakerNameArcher;//弓道家
    [SerializeField] private Sprite speakerNameThief;//怪盗

    private Coroutine dialogueCoroutine;

    //議論参加メンバーの情報配列
    [NonSerialized] public GameObject[] DiscussionMenber;

    //会話終了後の移行先
    public enum TalkFinish
    {
        DiscussionMode,//議論の最初に移行
        AdventureMode,//探索に移行
        TakeAroundDiscussionMode,//一巡して議論の最初に移行
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

        if (Input.GetMouseButtonDown(0) /*Input.GetKeyDown(KeyCode.Space) gameManager.KeyInputSpace()*/)
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

            //表示する文字の色を設定によって変える
            if(dialogueText.textInfomations[index].colorType == TextInfomation.TextColorType.Blue)
                speakerDialogueText.color = Color.cyan;
            else speakerDialogueText.color = Color.white;

            
            //話者の名前を表示
            speakerNameText.text = dialogueText.textInfomations[index].speakerName;
            if (dialogueText.textInfomations[index].speakerName == "シノノメ トオル")
            {
                speakerName.color = Color.white;
                speakerName.sprite = speakerNameDetective;
            }
            else if (dialogueText.textInfomations[index].speakerName == "コトギ マナ")
            {
                speakerName.color = Color.white;
                speakerName.sprite = speakerNameLacky;
            }
            else if (dialogueText.textInfomations[index].speakerName == "ミナセ ツミカ")
            {
                speakerName.color = Color.white;
                speakerName.sprite = speakerNameHacker;
            }
            else if (dialogueText.textInfomations[index].speakerName == "レイゼイ リン")
            {
                speakerName.color = Color.white;
                speakerName.sprite = speakerNameArcher;
            }
            else if (dialogueText.textInfomations[index].speakerName == "ニカイドウ ハルノ")
            {
                speakerName.color = Color.white;
                speakerName.sprite = speakerNameThief;
            }
            else
            {
                speakerName.color = Color.clear;
                speakerName.sprite = null;
            }

            //会話中のキャラにカメラを向ける
            SpeechCameraSet(speakerNameText);

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
                    //議論に移行
                    discussionManager.ShootingInit();
                    Debug.Log("議論に移行");
                    break;
                    
                case TalkFinish.TakeAroundDiscussionMode:
                    //議論を再開
                    discussionManager.DiscussionStart();
                    Debug.Log("一巡して議論を再開");
                    break;
                    
                case TalkFinish.AdventureMode:
                    //探索に移行
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
            //#006AB6 #0095d9
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

    //発言者のカメラを設定
    public void SpeechCameraSet(TextMeshProUGUI speakerName)
    {
        for (int i = 0; i < DiscussionMenber.Length; i++)
        {
            Debug.Log("speakerName.text:"+ speakerName.text + "  <SpeechName>().speechName"+ DiscussionMenber[i].GetComponent<SpeechName>().speechName);
            if (speakerName.text == DiscussionMenber[i].GetComponent<SpeechName>().speechName)
            {
                Debug.Log("CameraPriority(i)呼び出し");
                //カメラを発言者に向ける
                discussionManager.CameraPriority(i);
            }
        }
    }
}
