using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using static TalkCameraManager;
using Cysharp.Threading.Tasks;

public class TextWindow : MonoBehaviour
{
    //探索時のテキストウィンドウ

    //ゲームマネージャー
    [SerializeField] private GameManager gameManager;

    //会話中のカメラの管理
    [SerializeField] private TalkCameraManager talkCameraManager;
    //部屋のオブジェクトを管理
    RoomObjectManager roomObjectManager;

    [NonSerialized] public DialogueText dialogueText;
    [SerializeField, Header("部屋表示ウィンドウのオブジェクト")] private GameObject roomObject;
    [SerializeField, Header("時間表示ウィンドウのオブジェクト")] private GameObject timeObject;
    [SerializeField, Header("テキストウィンドウのオブジェクト")] private GameObject panelObject;
    //表示するテキストによって変化するテキストウィンドウ
    [SerializeField, Header("テキストウィンドウの画像")] private Sprite textWindowNormal;
    [SerializeField] private Sprite textWindowDark;

    [SerializeField, Header("話者")] private TextMeshProUGUI speakerNameText;
    [SerializeField, Header("テキスト")] private TextMeshProUGUI speakerDialogueText;
    int index = 0;
    bool isTyping;

    private Coroutine dialogueCoroutine;

    //会話を行う主人公キャラのオブジェクトパーツ
    [SerializeField, Header("調べる事のない主人公のオブジェクト")] private GameObject mainTalkChara;

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
    private float rectMoveSpeed = 0.05f;
    //表示範囲変化許容値
    private double RectSetAllow = 0.0001;

    //現在のカメラ分割設定
    TalkCameraManager.CameraSet.CameraDivision currentCameraDivision = TalkCameraManager.CameraSet.CameraDivision.CenterOnly;

    //子オブジェクトを指定するための3つのヴァーチャルカメラ番号
    private const int vcamNumCenter = 2, vcamNumRight = 3, vcamNumLeft = 4;

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
            ProgressText();
            
        }
    }

    //3つのカメラの表示範囲を移動する
    void TalkCameraRectMove()
    {
        if (currentCameraDivision == TalkCameraManager.CameraSet.CameraDivision.CenterOnly)
        {
            CameraEnabledOn();
            //会話カメラ表示範囲の変化　中央・右・左の順で指定
            StartCoroutine(CameraRectMove(rect_current_Center, rect_current_Right, rect_CenterOnly_Left,
                                rect_CenterOnly_Center, rect_CenterOnly_Right, rect_CenterOnly_Left));
        }
        else if (currentCameraDivision == TalkCameraManager.CameraSet.CameraDivision.CenterAndRight)
        {
            CameraEnabledOn();
            //会話カメラ表示範囲の変化　中央・右・左の順で指定
            StartCoroutine(CameraRectMove(rect_current_Center, rect_current_Right, rect_current_Left,
                                rect_CenteringRight_Center, rect_CenteringRight_Right, rect_CenteringRight_Left));
        }
        else if (currentCameraDivision == TalkCameraManager.CameraSet.CameraDivision.CenterAndLeft)
        {
            CameraEnabledOn();
            //会話カメラ表示範囲の変化　中央・右・左の順で指定
            StartCoroutine(CameraRectMove(rect_current_Center, rect_current_Right, rect_current_Left,
                                rect_CenteringLeft_Center, rect_CenteringLeft_Right, rect_CenteringLeft_Left));
        }
        else if (currentCameraDivision == TalkCameraManager.CameraSet.CameraDivision.All)
        {
            CameraEnabledOn();
            //会話カメラ表示範囲の変化　中央・右・左の順で指定
            StartCoroutine(CameraRectMove(rect_current_Center, rect_current_Right, rect_current_Left,
                                rect_All_Center, rect_All_Right, rect_All_Left));
        }
        else if(currentCameraDivision == TalkCameraManager.CameraSet.CameraDivision.None)
        {
            //会話カメラ無し
            CameraEnabledOff();
            Debug.Log("カメラenabled false");
            /*
            TaklCamera_1.enabled = false;
            TaklCamera_2.enabled = false;
            TaklCamera_3.enabled = false;
            */
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
            var mainChara = mainTalkChara.transform.GetChild(0).gameObject.transform.GetChild(0);
            var mainCharaShadow = mainTalkChara.transform.GetChild(1).gameObject.transform.GetChild(0);
            if (mainChara.GetComponent<Image>().enabled == false)
            { mainChara.GetComponent<Image>().enabled = true; mainCharaShadow.GetComponent<Image>().enabled = true; }

            //会話用カメラの起動
            //CameraEnabledOn();
            //CameraEnabled();

            //最初の会話の表示
            DisplayDialogueText();
        }

        //カメラが非表示なら表示する
        if (!TaklCamera_1.enabled)
        {
            /*
            Debug.Log("カメラenabled true");
            TaklCamera_1.enabled = true;
            TaklCamera_2.enabled = true;
            TaklCamera_3.enabled = true;
            */
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            //次の会話の表示
            DisplayDialogueText();
        }
    }

    //会話文表示処理
    public async void DisplayDialogueText()
    {

        //scriptableObjectの情報をパネルに表示する
        if (dialogueText.textInfomations.Length > index)
        {
            //テキスト番号のキーから現在のカメラ設定を取得
            var talkSet = talkSetDictionary[dialogueText.textinfo];
            var cameraSet = talkSet.cameraSet[index];

            //表示する文字の色を設定によって変える
            if (dialogueText.textInfomations[index].colorType == TextInfomation.TextColorType.Blue)
                speakerDialogueText.color = Color.cyan;
            else speakerDialogueText.color = Color.white;

            //テキストウィンドウの設定によって表示を変化
            WindowSpriteSetiing();

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
                //それぞれのカメラの表示範囲を指定の位置まで移動する
                currentCameraDivision = cameraSet.camDivision;

                TalkCameraRectMove();
            }

            //話者の名前を表示
            speakerNameText.text = dialogueText.textInfomations[index].speakerName;

            //会話イベントを発生
            cameraSet.OnTalkEvent.Invoke();
            //StartCoroutine(NextTalkEvent(cameraSet));

            //自己紹介関数で自己紹介発生フラグをTrue
            //Trueの時はUniTaskで待機
            if (GameManager.isTalkPause)
            {
                //設定したイベントが完了するまで待機
                await UniTask.WaitUntil(() => GameManager.isTalkEvent);
            }

            if (!isTyping)
            {
                //会話イベントを発生
                //cameraSet.OnTalkEvent.Invoke();

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
            var mainChara = mainTalkChara.transform.GetChild(0).gameObject.transform.GetChild(0);
            var mainCharaShadow = mainTalkChara.transform.GetChild(1).gameObject.transform.GetChild(0);
            if (mainChara.GetComponent<Image>().enabled != false) 
            { 
                mainChara.GetComponent<Image>().enabled = false; mainCharaShadow.GetComponent<Image>().enabled = false; 
            }

            //画面効果の切り替え
            gameManager.SwitchDepthOfField(false);

            //会話用カメラの非表示
            CameraEnabledOff();
            //CameraEnabled();

            //部屋UIを表示
            UIWindowActive(true);

            //状態を初期化して照準モードにする
            gameManager.playerController = GameManager.PlayerController.ReticleMode;

            index = 0;
            
            //フラグがある場合その部屋の議論が発生
            if (gameManager.isDiscussionStart)
            {
                gameManager.isDiscussionStart = false;
                gameManager.DiscussionModeChange();
            }
        }
    }

    private IEnumerator NextTalkEvent(CameraSet cameraSet)
    {
        GameManager.isTalkEvent = false;
        //会話イベントを発生
        cameraSet.OnTalkEvent.Invoke();
        //文字送りの前に一定時間画像を表示してから消去するイベントを追加したい

        //if(cameraSet.OnTalkEvent != null)

        yield return new WaitUntil(() => GameManager.isTalkEvent);

        if (!isTyping)
        {
            //会話イベントを発生
            //cameraSet.OnTalkEvent.Invoke();

            dialogueCoroutine = StartCoroutine(TypeDialogueText(dialogueText.textInfomations[index].paragraphs));
        }
        else
        {
            StopTyping();
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

    //会話カメラの起動
    public void CameraEnabledOn()
    {
        //画面効果の切り替え
        gameManager.SwitchDepthOfField(true);
        //現在のアクティブをTRUE
        /*
        TaklCamera_1.enabled = !TaklCamera_1.enabled;
        TaklCamera_2.enabled = !TaklCamera_2.enabled;
        TaklCamera_3.enabled = !TaklCamera_3.enabled;
        */
        TaklCamera_1.enabled = true;
        TaklCamera_2.enabled = true;
        TaklCamera_3.enabled = true;
    }
    //会話カメラの停止
    public void CameraEnabledOff()
    {
        //画面効果の切り替え
        gameManager.SwitchDepthOfField(false);
        //現在のアクティブをFALSE
        TaklCamera_1.enabled = false;
        TaklCamera_2.enabled = false;
        TaklCamera_3.enabled = false;
    }

    //テキストウィンドウの設定によって表示を変化
    private void WindowSpriteSetiing()
    {
        if (dialogueText.textInfomations[index].windowType == TextInfomation.TextWindowType.Normal)
        {
            panelObject.GetComponent<Image>().sprite = textWindowNormal;
            UIWindowActive(true);
        }
        else if (dialogueText.textInfomations[index].windowType == TextInfomation.TextWindowType.Dark)
        {
            panelObject.GetComponent<Image>().sprite = textWindowDark;
            UIWindowActive(true);
        }
        else if (dialogueText.textInfomations[index].windowType == TextInfomation.TextWindowType.Normal_NonUI)
        {
            panelObject.GetComponent<Image>().sprite = textWindowNormal;
            UIWindowActive(false);
        }
        else if (dialogueText.textInfomations[index].windowType == TextInfomation.TextWindowType.Dark_NonUI)
        {
            panelObject.GetComponent<Image>().sprite = textWindowDark;
            UIWindowActive(false);
        }
    }

    //部屋と時間のUIを表示・非表示
    private void UIWindowActive(bool isChange)
    {
        if (isChange == true)
        {
            roomObject.SetActive(true);
            timeObject.SetActive(true);
        }
        else if (isChange == false)
        {
            roomObject.SetActive(false);
            timeObject.SetActive(false);
        }
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
