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
    //表示範囲変化許容値
    private double RectSetAllow = 0.0001;

    //現在のカメラ分割設定
    TalkCameraManager.CameraSet.CameraDivision currentCameraDivision = TalkCameraManager.CameraSet.CameraDivision.CenterOnly;

    //子オブジェクトを指定するための3つのヴァーチャルカメラ番号
    private const int vcamNumCenter = 0, vcamNumRight = 1, vcamNumLeft = 2;

    //会話カメラの辞書
    private Dictionary<string, TalkCameraManager.TalkSet> talkSetDictionary = new();



    private void Start()
    {
        rect_current_Center = TaklCamera_1.rect = rect_CenterOnly_Center;
        rect_current_Right = TaklCamera_2.rect = rect_CenterOnly_Right;
        rect_current_Left = TaklCamera_3.rect = rect_CenterOnly_Left;

        roomObjectManager = GetComponent<RoomObjectManager>();

        //辞書の初期化
        for (int loop = 0; loop < talkCameraManager.talkSet.Length; loop++)
        {
            var talkset = talkCameraManager.talkSet[loop];
            talkSetDictionary.Add(talkset.textinfo, talkset);
        }
        
    }

    void Update()
    {
        //テキストを表示
        if (gameManager.playerController == GameManager.PlayerController.TextWindowMode)
        {
            //会話文表示処理を実行する
            //DisplayDialogueText();
            ProgressText();
            /*
            if (Input.GetKeyDown(KeyCode.Space))
            {

            }
            */
        }   
    }

    //3つのカメラの表示範囲を移動する
    void TalkCameraRectMove()
    {
        if (currentCameraDivision == TalkCameraManager.CameraSet.CameraDivision.CenterOnly)
        {
            //会話カメラ表示範囲の変化　中央・右・左の順で指定
            StartCoroutine(CameraRectMove(rect_current_Center, rect_current_Right, rect_CenterOnly_Left,
                                rect_CenterOnly_Center, rect_CenterOnly_Right, rect_CenterOnly_Left));
        }
        else if (currentCameraDivision == TalkCameraManager.CameraSet.CameraDivision.CenterAndRight)
        {
            //会話カメラ表示範囲の変化　中央・右・左の順で指定
            StartCoroutine(CameraRectMove(rect_current_Center, rect_current_Right, rect_current_Left,
                                rect_CenteringRight_Center, rect_CenteringRight_Right, rect_CenteringRight_Left));
        }
        else if (currentCameraDivision == TalkCameraManager.CameraSet.CameraDivision.CenterAndLeft)
        {
            //会話カメラ表示範囲の変化　中央・右・左の順で指定
            StartCoroutine(CameraRectMove(rect_current_Center, rect_current_Right, rect_current_Left,
                                rect_CenteringLeft_Center, rect_CenteringLeft_Right, rect_CenteringLeft_Left));
        }
        else if (currentCameraDivision == TalkCameraManager.CameraSet.CameraDivision.All)
        {
            //会話カメラ表示範囲の変化　中央・右・左の順で指定
            StartCoroutine(CameraRectMove(rect_current_Center, rect_current_Right, rect_current_Left,
                                rect_All_Center, rect_All_Right, rect_All_Left));
        }

    }

    //会話の進行
    public void ProgressText()
    {
        //パネルが非表示なら表示する
        if (!panelObject.activeSelf)
        {
            panelObject.SetActive(true);

            //主人公の表示処理
            if (mainTalkChara.GetComponent<Image>().enabled == false) mainTalkChara.GetComponent<Image>().enabled = true;

            //会話用カメラの起動
            CameraEnabled();

            //最初の会話の表示
            DisplayDialogueText();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            //次の会話の表示
            DisplayDialogueText();
        }
    }

    /// <summary>
    /// 会話文表示処理
    /// </summary>
    public void DisplayDialogueText()
    {
        
        /*
        //パネルが非表示なら表示する
        if (!panelObject.activeSelf)
        {
            panelObject.SetActive(true);

            //主人公の表示処理
            if (mainTalkChara.GetComponent<Image>().enabled == false) mainTalkChara.GetComponent<Image>().enabled = true;

            //会話用カメラの起動
            CameraEnabled();
        }
        */
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
            //テキスト番号のキーから現在のカメラ設定を取得
            var talkSet = talkSetDictionary[dialogueText.textinfo];
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
                Debug.Log("カメラ切り替え");
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
                StopTyping();
            }
        }
        else
        {
            //会話が終了したためパネルを非表示にする
            speakerNameText.text = "";
            speakerDialogueText.text = "";
            panelObject.SetActive(false);

            //主人公の透過処理
            if (mainTalkChara.GetComponent<Image>().enabled != false) mainTalkChara.GetComponent<Image>().enabled = false;

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
    private void StopTyping()
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
        //現在のアクティブを反転させる
        TaklCamera_1.enabled = !TaklCamera_1.enabled;
        TaklCamera_2.enabled = !TaklCamera_2.enabled;
        TaklCamera_3.enabled = !TaklCamera_3.enabled;
    }

    //3つのカメラの表示範囲を同時に変化
    private IEnumerator CameraRectMove(Rect currentRect_Center, Rect currentRect_Right, Rect currentRect_Left,
                                       Rect SetRect_Center, Rect SetRect_Right, Rect SetRect_Left)
    {
        //全てのRectの移動完了を判定するフラグ
        bool setCompletion = false;
        //XYWH設定完了のフラグ
        bool flgCenter_x = false, flgCenter_y = false, flgCenter_w = false, flgCenter_h = false;
        bool flgRight_x = false, flgRight_y = false, flgRight_w = false, flgRight_h = false;
        bool flgLeft_x = false, flgLeft_y = false, flgLeft_w = false, flgLeft_h = false;
        //無限ループ阻止用の値
        int noEndlessLoop = 0, LoopStopNum = 10000;

        while (!setCompletion)
        {
            //設定された値に近いか調べ違う場合加算か減算する
            //中央カメラX
            if ((SetRect_Center.x - RectSetAllow) <= currentRect_Center.x && currentRect_Center.x <= (SetRect_Center.x + RectSetAllow))
                flgCenter_x = true;
            else
            {
                if (currentRect_Center.x < SetRect_Center.x) currentRect_Center.x += rectMoveSpeed;//加算
                else currentRect_Center.x -= rectMoveSpeed;//減算
            }
            //中央カメラY
            if ((SetRect_Center.y - RectSetAllow) <= currentRect_Center.y && currentRect_Center.y <= (SetRect_Center.y + RectSetAllow))
                flgCenter_y = true;
            else
            {
                if (currentRect_Center.y < SetRect_Center.y) currentRect_Center.y += rectMoveSpeed;//加算
                else currentRect_Center.y -= rectMoveSpeed;//減算
            }
            //中央カメラW
            if ((SetRect_Center.width - RectSetAllow) <= currentRect_Center.width && currentRect_Center.width <= (SetRect_Center.width + RectSetAllow))
                flgCenter_w = true;
            else
            {
                if (currentRect_Center.width < SetRect_Center.width) currentRect_Center.width += rectMoveSpeed;//加算
                else currentRect_Center.width -= rectMoveSpeed;//減算
            }
            //中央カメラH
            if ((SetRect_Center.height - RectSetAllow) <= currentRect_Center.height && currentRect_Center.height <= (SetRect_Center.height + RectSetAllow))
                flgCenter_h = true;
            else
            {
                if (currentRect_Center.height < SetRect_Center.height) currentRect_Center.height += rectMoveSpeed;//加算
                else currentRect_Center.height -= rectMoveSpeed;//減算
            }
            //中央カメラに代入
            TaklCamera_1.rect = currentRect_Center;

            //右カメラX
            if ((SetRect_Right.x - RectSetAllow) <= currentRect_Right.x && currentRect_Right.x <= (SetRect_Right.x + RectSetAllow))
                flgRight_x = true;
            else
            {
                if (currentRect_Right.x < SetRect_Right.x) currentRect_Right.x += rectMoveSpeed;//加算
                else currentRect_Right.x -= rectMoveSpeed;//減算
            }
            //右カメラY
            if ((SetRect_Right.y - RectSetAllow) <= currentRect_Right.y && currentRect_Right.y <= (SetRect_Right.y + RectSetAllow))
                flgRight_y = true;
            else
            {
                if (currentRect_Right.y < SetRect_Right.y) currentRect_Right.y += rectMoveSpeed;//加算
                else currentRect_Right.y -= rectMoveSpeed;//減算
            }
            //右カメラW
            if ((SetRect_Right.width - RectSetAllow) <= currentRect_Right.width && currentRect_Right.width <= (SetRect_Right.width + RectSetAllow))
                flgRight_w = true;
            else
            {
                if (currentRect_Right.width < SetRect_Right.width) currentRect_Right.width += rectMoveSpeed;//加算
                else currentRect_Right.width -= rectMoveSpeed;//減算
            }
            //右カメラH
            if ((SetRect_Right.height - RectSetAllow) <= currentRect_Right.height && currentRect_Right.height <= (SetRect_Right.height + RectSetAllow))
                flgRight_h = true;
            else
            {
                if (currentRect_Right.height < SetRect_Right.height) currentRect_Right.height += rectMoveSpeed;//加算
                else currentRect_Right.height -= rectMoveSpeed;//減算
            }
            //右カメラに代入
            TaklCamera_2.rect = currentRect_Right;

            //左カメラX
            if ((SetRect_Left.x - RectSetAllow) <= currentRect_Left.x && currentRect_Left.x <= (SetRect_Left.x + RectSetAllow))
                flgLeft_x = true;
            else
            {
                if (currentRect_Left.x < SetRect_Left.x) currentRect_Left.x += rectMoveSpeed;//加算
                else currentRect_Left.x -= rectMoveSpeed;//減算
            }
            //左カメラY
            if ((SetRect_Left.y - RectSetAllow) <= currentRect_Left.y && currentRect_Left.y <= (SetRect_Left.y + RectSetAllow))
                flgLeft_y = true;
            else
            {
                if (currentRect_Left.y < SetRect_Left.y) currentRect_Left.y += rectMoveSpeed;//加算
                else currentRect_Left.y -= rectMoveSpeed;//減算
            }
            //左カメラW
            if ((SetRect_Left.width - RectSetAllow) <= currentRect_Left.width && currentRect_Left.width <= (SetRect_Left.width + RectSetAllow))
                flgLeft_w = true;
            else
            {
                if (currentRect_Left.width < SetRect_Left.width) currentRect_Left.width += rectMoveSpeed;//加算
                else currentRect_Left.width -= rectMoveSpeed;//減算
            }
            //左カメラH
            if ((SetRect_Left.height - RectSetAllow) <= currentRect_Left.height && currentRect_Left.height <= (SetRect_Left.height + RectSetAllow))
                flgLeft_h = true;
            else
            {
                if (currentRect_Left.height < SetRect_Left.height) currentRect_Left.height += rectMoveSpeed;//加算
                else currentRect_Left.height -= rectMoveSpeed;//減算
            }
            //左カメラに代入
            TaklCamera_3.rect = currentRect_Left;

            noEndlessLoop++;
            //全ての値が指定値になった時ループ終了
            if (noEndlessLoop > LoopStopNum ||
                flgCenter_x && flgCenter_y && flgCenter_w && flgCenter_h &&
                flgRight_x && flgRight_y && flgRight_w && flgRight_h &&
                flgLeft_x && flgLeft_y && flgLeft_w && flgLeft_h)
            {
                //ループ終了のフラグ
                setCompletion = true;
            }

            yield return null;
        }

    }
}
