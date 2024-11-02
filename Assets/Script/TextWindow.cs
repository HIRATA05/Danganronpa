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
    //会話中のカメラの管理
    [SerializeField] private TalkCameraManager talkCameraManager;
    //部屋のオブジェクトを管理
    RoomObjectManager roomObjectManager;

    [NonSerialized] public DialogueText dialogueText;
    [SerializeField] private GameObject panelObject;
    [SerializeField] private TextMeshProUGUI speakerNameText;
    [SerializeField] private TextMeshProUGUI speakerDialogueText;
    int index = 0;
    bool isTyping;

    private Coroutine dialogueCoroutine;

    //会話を行う主人公キャラのオブジェクトパーツ
    [SerializeField] private GameObject mainTalkChara;
    

    //カメラ
    [SerializeField] private Camera TaklCamera_1;
    [SerializeField] private Camera TaklCamera_2;
    [SerializeField] private Camera TaklCamera_3;

    [Header("カメラの表示範囲")]//カメラの表示範囲
    //中央のみ　左右は画面外
    [SerializeField] private Rect rect_Left = new Rect(0.0f, 0.0f, 0.25f, 0.9f);
    [SerializeField] private Rect rect_Center = new Rect(0.25f, 0.0f, 0.5f, 1f);
    [SerializeField] private Rect rect_Right = new Rect(0.5f, 0.0f, 0.25f, 0.9f);

    //中央と右　左は画面外
    [SerializeField] private Rect rect_CenteringRight_Left = new Rect(-0.25f, 0.0f, 0.25f, 0.9f);
    [SerializeField] private Rect rect_CenteringRight_Center = new Rect(0.1f, 0.0f, 0.5f, 1f);
    [SerializeField] private Rect rect_CenteringRight_Right = new Rect(0.5f, 0.0f, 0.5f, 0.9f);

    //中央と左　右は画面外
    [SerializeField] private Rect rect_CenteringLeft = new Rect(0.25f, 0.0f, 0.5f, 1f);
    //中央と左右　3つとも画面内


    //現在のカメラ分割設定
    TalkCameraManager.CameraSet.CameraDivision currentCameraDivision = TalkCameraManager.CameraSet.CameraDivision.CenterOnly;

    //子オブジェクトを指定するための3つのヴァーチャルカメラ番号
    private const int vcamNumCenter = 0, vcamNumRight = 1, vcamNumLeft = 2;

    //会話カメラの辞書
    private Dictionary<int, TalkCameraManager.TalkSet> talkSetDictionary = new();

    private void Start()
    {
        TaklCamera_1.rect = rect_Center;
        TaklCamera_2.rect = rect_Right;
        TaklCamera_3.rect = rect_Left;

        roomObjectManager = GetComponent<RoomObjectManager>();

        // 辞書の初期化
        for (int loop = 0; loop < talkCameraManager.talkSet.Length; loop++)
        {
            var talkset = talkCameraManager.talkSet[loop];
            talkSetDictionary.Add(talkset.number, talkset);
        }

    }

    void Update()
    {

        //テキストを表示
        if (gameManager.playerController == GameManager.PlayerController.TextWindowMode)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //会話文表示処理を実行する
                displayDialogueText();

            }
        }   
    }

    //3つのカメラの表示範囲を移動する
    void TalkCameraRectMove()
    {
        if (currentCameraDivision == TalkCameraManager.CameraSet.CameraDivision.CenterOnly)
        {
            TaklCamera_1.rect = rect_Center;
            TaklCamera_2.rect = rect_Right;
            TaklCamera_3.rect = rect_Left;
        }
        else if (currentCameraDivision == TalkCameraManager.CameraSet.CameraDivision.CenterAndRight)
        {
            TaklCamera_1.rect = rect_CenteringRight_Center;
            TaklCamera_2.rect = rect_CenteringRight_Right;
            TaklCamera_3.rect = rect_CenteringRight_Left;
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

            //主人公のマップオブジェクト削除
            if (mainTalkChara.GetComponent<Image>().enabled == false) mainTalkChara.GetComponent<Image>().enabled = true;

            //会話用カメラの起動
            CameraEnabled();

        }

        //scriptableObjectの情報をパネルに表示する
        if (dialogueText.textInfomations.Length > index)
        {
#if false
            //会話中のカメラを設定
            for(int loop = 0; loop < talkCameraManager.talkSet.Length; loop++)
            {
                //テキストの番号とカメラ設定の番号が一致している時
                if (dialogueText.number == talkCameraManager.talkSet[loop].number)
                {
                    //カメラの注視対象を設定
                    //中央のカメラ
                    roomObjectManager.RoomObjectPriorityChange(talkCameraManager.talkSet[loop].cameraSet[index].cameraLookObjectCenter, vcamNumCenter);
                    //右のカメラ
                    roomObjectManager.RoomObjectPriorityChange(talkCameraManager.talkSet[loop].cameraSet[index].cameraLookObjectRight, vcamNumRight);
                    //左のカメラ
                    roomObjectManager.RoomObjectPriorityChange(talkCameraManager.talkSet[loop].cameraSet[index].cameraLookObjectLeft, vcamNumLeft);

                    //カメラ分割を設定
                    if(currentCameraDivision != talkCameraManager.talkSet[loop].cameraSet[index].camDivision)
                    {
                        Debug.Log("主人公のオブジェクト配置");
                        //それぞれのカメラの表示範囲を指定の位置まで移動する
                        currentCameraDivision = talkCameraManager.talkSet[loop].cameraSet[index].camDivision;

                        TalkCameraRectMove();

                    }

                }
            }
#else
            var talkSet = talkSetDictionary[dialogueText.number];
            var cameraSet = talkSet.cameraSet[index];

            //カメラの注視対象を設定
            //中央のカメラ
            roomObjectManager.RoomObjectPriorityChange(cameraSet.cameraLookObjectCenter, vcamNumCenter);
            //右のカメラ
            roomObjectManager.RoomObjectPriorityChange(cameraSet.cameraLookObjectRight, vcamNumRight);
            //左のカメラ
            roomObjectManager.RoomObjectPriorityChange(cameraSet.cameraLookObjectLeft, vcamNumLeft);

            //カメラ分割を設定
            if (currentCameraDivision != cameraSet.camDivision)
            {
                Debug.Log("主人公のオブジェクト配置");
                //それぞれのカメラの表示範囲を指定の位置まで移動する
                currentCameraDivision = cameraSet.camDivision;

                TalkCameraRectMove();

            }
#endif

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

            //主人公のマップオブジェクト削除
            if(mainTalkChara.GetComponent<Image>().enabled != false) mainTalkChara.GetComponent<Image>().enabled = false;

            //会話用カメラの非表示
            CameraEnabled();

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

    //会話カメラの起動
    public void CameraEnabled()
    {
        //FALSEの時TRUE
        //    if (!TaklCamera_1.enabled) TaklCamera_1.enabled = true;
        //    if (!TaklCamera_2.enabled) TaklCamera_2.enabled = true;
        //    if (!TaklCamera_3.enabled) TaklCamera_3.enabled = true;

        //    //TRUEの時FALSE
        //    if (TaklCamera_1.enabled) TaklCamera_1.enabled = false;
        //    if (TaklCamera_2.enabled) TaklCamera_2.enabled = false;
        //    if (TaklCamera_3.enabled) TaklCamera_3.enabled = false;

        TaklCamera_1.enabled = !TaklCamera_1.enabled;
        TaklCamera_2.enabled = !TaklCamera_2.enabled;
        TaklCamera_3.enabled = !TaklCamera_3.enabled;

    }
}
