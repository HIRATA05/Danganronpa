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

    [Header("カメラ設定")]
    //会話カメラ
    [SerializeField] private Camera TaklCamera_1;//中央
    [SerializeField] private Camera TaklCamera_2;//右
    [SerializeField] private Camera TaklCamera_3;//左

    //カメラの表示範囲
    [Header("中央のみ")]//中央のみ　左右は画面外
    [SerializeField] private Rect rect_CenterOnly_Left;
    [SerializeField] private Rect rect_CenterOnly_Center;
    [SerializeField] private Rect rect_CenterOnly_Right;

    [Header("中央と右")]//中央と右　左は画面外
    [SerializeField] private Rect rect_CenteringRight_Left;
    [SerializeField] private Rect rect_CenteringRight_Center;
    [SerializeField] private Rect rect_CenteringRight_Right;

    [Header("中央と左")]//中央と左　右は画面外
    [SerializeField] private Rect rect_CenteringLeft_Left;
    [SerializeField] private Rect rect_CenteringLeft_Center;
    [SerializeField] private Rect rect_CenteringLeft_Right;

    [Header("中央と左右")]//中央と左右　3つとも画面内
    [SerializeField] private Rect rect_All_Left;
    [SerializeField] private Rect rect_All_Center;
    [SerializeField] private Rect rect_All_Right;

    //現在のカメラ表示範囲
    private Rect rect_current_Left;
    private Rect rect_current_Center;
    private Rect rect_current_Right;
    //表示範囲変化速度
    private float rectMoveSpeed = 0.01f;

    //現在のカメラ分割設定
    TalkCameraManager.CameraSet.CameraDivision currentCameraDivision = TalkCameraManager.CameraSet.CameraDivision.CenterOnly;

    //子オブジェクトを指定するための3つのヴァーチャルカメラ番号
    private const int vcamNumCenter = 0, vcamNumRight = 1, vcamNumLeft = 2;

    //会話カメラの辞書
    private Dictionary<int, TalkCameraManager.TalkSet> talkSetDictionary = new();


    [SerializeField] private GameObject instantieatedCard;


    private void Start()
    {
        StartCoroutine(MoveCard());

        rect_current_Center = TaklCamera_1.rect = rect_CenterOnly_Center;
        rect_current_Right = TaklCamera_2.rect = rect_CenterOnly_Right;
        rect_current_Left = TaklCamera_3.rect = rect_CenterOnly_Left;

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
            /*
            TaklCamera_1.rect = CameraRectMove(rect_current_Center, rect_CenterOnly_Center);
            TaklCamera_2.rect = CameraRectMove(rect_current_Right, rect_CenterOnly_Right);
            TaklCamera_3.rect =CameraRectMove(rect_current_Left, rect_CenterOnly_Left);
            */
            /*
            StartCoroutine(CameraRectMove(TaklCamera_1.rect, rect_CenterOnly_Center));
            StartCoroutine(CameraRectMove(TaklCamera_2.rect, rect_CenterOnly_Right));
            StartCoroutine(CameraRectMove(TaklCamera_3.rect, rect_CenterOnly_Left));
            */

            TaklCamera_1.rect = rect_CenterOnly_Center;
            TaklCamera_2.rect = rect_CenterOnly_Right;
            TaklCamera_3.rect = rect_CenterOnly_Left;
            //CameraRectMove(rect_current_Center, rect_current_Right, rect_CenterOnly_Left,
            //    rect_CenterOnly_Center, rect_CenterOnly_Right, rect_CenterOnly_Left);

        }
        else if (currentCameraDivision == TalkCameraManager.CameraSet.CameraDivision.CenterAndRight)
        {
            /*
            TaklCamera_1.rect = CameraRectMove(rect_current_Center, rect_CenteringRight_Center);
            TaklCamera_2.rect = CameraRectMove(rect_current_Right, rect_CenteringRight_Right);
            TaklCamera_3.rect = CameraRectMove(rect_current_Left, rect_CenteringRight_Left);
            */
            /*
            StartCoroutine(CameraRectMove(TaklCamera_1.rect, rect_CenteringRight_Center));
            StartCoroutine(CameraRectMove(TaklCamera_2.rect, rect_CenteringRight_Right));
            StartCoroutine(CameraRectMove(TaklCamera_3.rect, rect_CenteringRight_Left));
            */
            
            TaklCamera_1.rect = rect_CenteringRight_Center;
            TaklCamera_2.rect = rect_CenteringRight_Right;
            TaklCamera_3.rect = rect_CenteringRight_Left;
            

            //CameraRectMove(rect_current_Center, rect_current_Right, rect_CenterOnly_Left,
            //    rect_CenteringRight_Center, rect_CenteringRight_Right, rect_CenteringRight_Left);

        }
        else if (currentCameraDivision == TalkCameraManager.CameraSet.CameraDivision.CenterAndLeft)
        {
            TaklCamera_1.rect = rect_CenteringLeft_Center;
            TaklCamera_2.rect = rect_CenteringLeft_Right;
            TaklCamera_3.rect = rect_CenteringLeft_Left;
        }
        else if (currentCameraDivision == TalkCameraManager.CameraSet.CameraDivision.All)
        {
            TaklCamera_1.rect = rect_All_Center;
            TaklCamera_2.rect = rect_All_Right;
            TaklCamera_3.rect = rect_All_Left;
        }

    }

    
    // カードを指定された位置に移動させるコルーチン
    private IEnumerator MoveCard()
    {
        Debug.Log("カード移動コルーチン開始");
        Vector3 StartDeckPos = instantieatedCard.transform.localPosition;
        Vector3 EndHandPos = new Vector3(0f, 1f, 100f);
        float animDuration = 1f; // アニメーションの総時間
        float startTime = Time.time;

        while (Time.time - startTime < animDuration)
        {
            float journeyFraction = (Time.time - startTime) / animDuration;
            //滑らかに移動させるさせる場合は以下のコード追加する
            //journeyFraction = Mathf.SmoothStep(0f, 1f, journeyFraction);
            instantieatedCard.transform.localPosition = Vector3.Lerp(StartDeckPos, EndHandPos, journeyFraction);
            yield return null;
        }
        Debug.Log("カード移動終了");
    }
    

    //3つのカメラを同時に動作
    private IEnumerator CameraRectMove(Rect currentRect_Center, Rect currentRect_Right, Rect currentRect_Left,
                                Rect SetRect_Center,     Rect SetRect_Right,     Rect SetRect_Left)
    {
        //全てのRectの移動完了を判定するフラグ
        bool setCompletion = false;
        //XYWH設定完了のフラグ
        bool flgCenter_x = false, flgCenter_y = false, flgCenter_w = false, flgCenter_h = false;
        bool flgRight_x = false, flgRight_y = false, flgRight_w = false, flgRight_h = false;
        bool flgLeft_x = false, flgLeft_y = false, flgLeft_w = false, flgLeft_h = false;

        while (setCompletion)
        {
            Debug.Log("カメラ範囲セット中");
            //値を加算か減算か比較して決める
            //中央
            //X
            if (!flgCenter_x && currentRect_Center.x < SetRect_Center.x)
            {
                currentRect_Center.x += rectMoveSpeed;//加算
            }
            else if (!flgCenter_x && currentRect_Center.x > SetRect_Center.x)
            {
                currentRect_Center.x -= rectMoveSpeed;//減算
            }
            else flgCenter_x = true;
            //Y
            if (!flgCenter_y && currentRect_Center.y < SetRect_Center.y)
            {
                currentRect_Center.y += rectMoveSpeed;//加算
            }
            else if (!flgCenter_y && currentRect_Center.y > SetRect_Center.y)
            {
                currentRect_Center.y -= rectMoveSpeed;//減算
            }
            else flgCenter_y = true;
            //W
            if (!flgCenter_w && currentRect_Center.width < SetRect_Center.width)
            {
                currentRect_Center.width += rectMoveSpeed;//加算
            }
            else if (!flgCenter_w && currentRect_Center.width > SetRect_Center.width)
            {
                currentRect_Center.width -= rectMoveSpeed;//減算
            }
            else flgCenter_w = true;
            //H
            if (!flgCenter_h && currentRect_Center.height < SetRect_Center.height)
            {
                currentRect_Center.height += rectMoveSpeed;//加算
            }
            else if (!flgCenter_h && currentRect_Center.height > SetRect_Center.height)
            {
                currentRect_Center.height -= rectMoveSpeed;//減算
            }
            else flgCenter_h = true;
            //中央カメラに代入
            TaklCamera_1.rect = currentRect_Center;

            //右
            //X
            if (!flgRight_x && currentRect_Right.x < SetRect_Right.x)
            {
                currentRect_Right.x += rectMoveSpeed;//加算
            }
            else if (!flgRight_x && currentRect_Right.x > SetRect_Right.x)
            {
                currentRect_Right.x -= rectMoveSpeed;//減算
            }
            else flgRight_x = true;
            //Y
            if (!flgRight_y && currentRect_Right.y < SetRect_Right.y)
            {
                currentRect_Right.y += rectMoveSpeed;//加算
            }
            else if (!flgRight_y && currentRect_Right.y > SetRect_Right.y)
            {
                currentRect_Right.y -= rectMoveSpeed;//減算
            }
            else flgRight_y = true;
            //W
            if (!flgRight_w && currentRect_Right.width < SetRect_Right.width)
            {
                currentRect_Right.width += rectMoveSpeed;//加算
            }
            else if (!flgRight_w && currentRect_Right.width > SetRect_Right.width)
            {
                currentRect_Right.width -= rectMoveSpeed;//減算
            }
            else flgRight_w = true;
            //H
            if (!flgRight_h && currentRect_Right.height < SetRect_Right.height)
            {
                currentRect_Right.height += rectMoveSpeed;//加算
            }
            else if (!flgRight_h && currentRect_Right.height > SetRect_Right.height)
            {
                currentRect_Right.height -= rectMoveSpeed;//減算
            }
            else flgRight_h = true;
            //右カメラに代入
            TaklCamera_2.rect = currentRect_Right;

            //左
            //X
            if (!flgLeft_x && currentRect_Left.x < SetRect_Left.x)
            {
                currentRect_Left.x += rectMoveSpeed;//加算
            }
            else if (!flgLeft_x && currentRect_Left.x > SetRect_Left.x)
            {
                currentRect_Left.x -= rectMoveSpeed;//減算
            }
            else flgLeft_x = true;
            //Y
            if (!flgLeft_y && currentRect_Left.y < SetRect_Left.y)
            {
                currentRect_Left.y += rectMoveSpeed;//加算
            }
            else if (!flgLeft_y && currentRect_Left.y > SetRect_Left.y)
            {
                currentRect_Left.y -= rectMoveSpeed;//減算
            }
            else flgLeft_y = true;
            //W
            if (!flgLeft_w && currentRect_Left.width < SetRect_Left.width)
            {
                currentRect_Left.width += rectMoveSpeed;//加算
            }
            else if (!flgLeft_w && currentRect_Left.width > SetRect_Left.width)
            {
                currentRect_Left.width -= rectMoveSpeed;//減算
            }
            else flgLeft_w = true;
            //H
            if (!flgLeft_h && currentRect_Left.height < SetRect_Left.height)
            {
                currentRect_Left.height += rectMoveSpeed;//加算
            }
            else if (!flgLeft_h && currentRect_Left.height > SetRect_Left.height)
            {
                currentRect_Left.height -= rectMoveSpeed;//減算
            }
            else flgLeft_h = true;
            //左カメラに代入
            TaklCamera_3.rect = currentRect_Left;

            //全てのフラグがTRUEだとループから抜ける
            if (flgCenter_x && flgCenter_y && flgCenter_w && flgCenter_h &&
                flgRight_x && flgRight_y && flgRight_w && flgRight_h &&
                flgLeft_x && flgLeft_y && flgLeft_w && flgLeft_h)
            {
                Debug.Log("カメラ範囲セット完了");
                setCompletion = true;
                break;
            }

            yield return null;
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
            /*
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
            */
#else
            var talkSet = talkSetDictionary[dialogueText.number];
            var cameraSet = talkSet.cameraSet[index];

            //カメラの注視対象を設定
            //中央のカメラ
            roomObjectManager.RoomObjectPriorityChange(cameraSet.camLookCenter, vcamNumCenter);
            //右のカメラ
            roomObjectManager.RoomObjectPriorityChange(cameraSet.camLookRight, vcamNumRight);
            //左のカメラ
            roomObjectManager.RoomObjectPriorityChange(cameraSet.camLookLeft, vcamNumLeft);

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
                //会話イベントを発生
                cameraSet.OnTalkEvent.Invoke();

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
