using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.UIElements;
using Cinemachine;
using Cysharp.Threading.Tasks;
using static UnityEditor.PlayerSettings;

public class DiscussionManager : MonoBehaviour
{
    //ノンストップ議論の全体的な動作を管理

    //ゲームマネージャー
    [SerializeField] private GameManager gameManager;

    //議論時のテキストウィンドウ
    [SerializeField] private DiscussionTalkModeWindow discussionTalkModeWindow;

    //議論時のUI
    [SerializeField] private DiscussionUI discussionUI;

    //円形の並びを作るスクリプト
    [SerializeField] private CircleDeployer circleDeployer;

    public enum DiscussionMode
    {
        Talk,//会話
        BeforeDiscussion,//議論前演出
        Shooting//ノンストップ議論
    }
    [NonSerialized] public DiscussionMode discussion;

    //議論場所の中心点
    [SerializeField] private GameObject centerDiscussionPoint;

    //議論カメラ
    [SerializeField] private Camera DiscussionCamera;
    [SerializeField] private GameObject MainVirtualCamera;
    private CinemachineVirtualCamera MainCCVirtualCamera;

    //次の発言までの時間
    float nextSpeechTime = 5.0f;
    //現在時間
    float currentTime = 0.0f;

    //議論開始時会話
    [SerializeField, Header("開始の会話")] private DialogueText DiscussionStartText;
    //議論一巡時会話
    [SerializeField, Header("一巡時の会話")] private DialogueText DiscussionTakeAroundText;
    //議論終了時会話
    [SerializeField, Header("議論終了時の会話")] private DialogueText DiscussionFinishText;
    //論破ゲームオーバー時会話
    [SerializeField, Header("ゲームオーバー時の会話")] private DialogueText DiscussionGameOverText;
    //議論終了後の照準モードでの会話
    [Header("議論終了後の会話")] public DialogueText AfterText;

    //発言を表示するテキスト
    [SerializeField, Header("発言セリフ")] private GameObject speechText;
    
    //セリフ
    [System.Serializable]
    public class SpeechSet
    {
        //発言者の名前
        public string SpeechName;

        //発言
        public string NormalSpeechBefore;
        public string WeekPointSpeech;
        public string NormalSpeechAfter;

        /*
        //論破か同意か
        public enum SpeechType
        {
            refute,
            consent,
            None
        }
        */
        public DiscussionUI.SpeechType speechType = DiscussionUI.SpeechType.None;

        //発言の移動パターン
        public enum SpeechMovePattern
        {
            RightToLeft,//右から左
            LeftToRight,//左から右
        }
        public SpeechMovePattern speechMove;

        //コトダマを間違えた時の会話
        public DialogueText DiscussionMistakeText;

    }
    [Header("論破カラーコード#ffa500　同意カラーコード#41A2E1")]
    public SpeechSet[] speechSet;

    private SpeechSet CurrentSpeech;
    private string CurrentSpeechWeekPoint;
    private DiscussionUI.SpeechType CurrentSpeechType;

    private string CurrentWeekPoint;
    //[NonSerialized] public TextMeshProUGUI SpeechAll;

    //議論の進行番号
    private int DiscussionNum = 0;

    //議論開始の際の時間
    private float elapsedTime = 0.0f;
    private float startTime = 5.0f;

    //円形に並ぶ生徒
    [SerializeField, Header("議論で並ぶキャラ")] private GameObject[] DiscussionMenber;
    //生徒生成の親オブジェクト
    [SerializeField, Header("議論者発生の親オブジェクト")] private GameObject perentObj;
    //生成した生徒の配列
    private GameObject[] InstantiateMenber = new GameObject[0];

    private bool isDiscussionInitCalled = false;
    private bool isTextSetCalled = false;

    //議論中かどうか
    [NonSerialized] public bool discussionProgress = false;

    void Start()
    {
        MainCCVirtualCamera = MainVirtualCamera.GetComponent<CinemachineVirtualCamera>();
    }

    void Update()
    {
        if (gameManager.playerController == GameManager.PlayerController.DiscussionMode)
        {
            //議論初期化処理
            DiscussionInit();
            //議論の進行
            DiscussionPlay();
        }
    }
    
    //議論フェイズの初期化
    public void DiscussionInit()
    {
        if (!isDiscussionInitCalled)
        {
            isDiscussionInitCalled = true;

            //議論番号初期化
            DiscussionNum = 0;

            InstantiateMenber = new GameObject[DiscussionMenber.Length];
            //DiscussionMenberを順番に生成
            for (int i = 0; i < DiscussionMenber.Length; i++)
            {
                InstantiateMenber[i] = Instantiate(DiscussionMenber[i], perentObj.transform);
            }

            //生徒の並びを円形に並べる
            circleDeployer.Deploy();

            //コトダマのセットアップ
            discussionUI.BulletSelectSet();

            //議論開始時の会話データを入れる
            gameManager.OpenDiscussionWindow(DiscussionStartText, DiscussionTalkModeWindow.TalkFinish.DiscussionMode);
            //ノンストップ議論の開始
            discussion = DiscussionMode.Talk;
        }

    }

    //議論中の処理
    private void DiscussionPlay()
    {
        //会話
        if (discussion == DiscussionMode.Talk)
        {
            //会話時のUI表示

        }
        //議論開始前の演出
        else if (discussion == DiscussionMode.BeforeDiscussion)
        {
            //Debug.Log("議論開始経過時間：" + elapsedTime);

            //議論開始の文字演出


            if (elapsedTime > startTime)
            {
                Debug.Log("=議論を開始=");
                elapsedTime = 0;

                //カメラを停止
                //DiscussionCamera.GetComponent<CameraRotation>().RotateOff();
                MainVirtualCamera.GetComponent<CameraRotation>().RotateOff();

                //議論開始処理
                DiscussionStart();
                /*
                //カメラを主人公に向ける
                CameraPriority(0);

                //最大発言番号を設定
                discussionUI.SpeechNumMaxSet(speechSet.Length);

                //上記終了後議論開始
                discussion = DiscussionMode.Shooting;
                discussionProgress = true;
                */
            }

            //時間経過
            elapsedTime += Time.deltaTime;

        }
        //議論
        else if (discussion == DiscussionMode.Shooting && discussionProgress)
        {
            //議論時のUI表示


            //右クリックでコトダマを切り替え
            if (Input.GetMouseButtonDown(1))
            {
                discussionUI.BulletSelectChange();
            }
            
            


            //一定時間ごとに文字を変化
            if (!isTextSetCalled)
            {
                isTextSetCalled = true;

                //現在の発言を取得
                CurrentSpeech = speechSet[DiscussionNum];
                CurrentSpeechWeekPoint = speechSet[DiscussionNum].WeekPointSpeech;
                Debug.Log("CurrentSpeechウィークポイント：" + CurrentSpeech.WeekPointSpeech + " CurrentSpeechWeekPoint:" + CurrentSpeechWeekPoint);
                CurrentSpeechType = speechSet[DiscussionNum].speechType;
                //CurrentWeekPoint = speechSet[DiscussionNum].WeekPointSpeech;
                //文字の大きさと色をセット
                speechSet[DiscussionNum].NormalSpeechBefore = "<size=60><color=white> " + speechSet[DiscussionNum].NormalSpeechBefore;

                if(speechSet[DiscussionNum].speechType == DiscussionUI.SpeechType.refute)
                    speechSet[DiscussionNum].WeekPointSpeech = "<size=65><color=#ffa500> " + speechSet[DiscussionNum].WeekPointSpeech;
                else if(speechSet[DiscussionNum].speechType == DiscussionUI.SpeechType.consent)
                    speechSet[DiscussionNum].WeekPointSpeech = "<size=65><color=#41A2E1> " + speechSet[DiscussionNum].WeekPointSpeech;
                else
                    speechSet[DiscussionNum].WeekPointSpeech = "<size=60><color=white> " + speechSet[DiscussionNum].WeekPointSpeech;

                speechSet[DiscussionNum].NormalSpeechAfter = "<size=60><color=white> " + speechSet[DiscussionNum].NormalSpeechAfter;
                //全ての文字を合わせて1つの文字列を作る
                string speech = speechSet[DiscussionNum].NormalSpeechBefore + " " + speechSet[DiscussionNum].WeekPointSpeech + " " + speechSet[DiscussionNum].NormalSpeechAfter;
                //Debug.Log(speech);

                //文字のデータをセットする
                speechText.GetComponent<TextMeshProUGUI>().text = speech;

                //SpeechAll = speechText.GetComponent<TextMeshProUGUI>();

                //speechText.GetComponent<TextMeshPro>().text = speechSet[DiscussionNum].Speech;

                //文字数取得 speechText.GetComponent<TextMeshProUGUI>().text.Length
                //SpeechType.Noneの場合ウィークポイントは作らない

                //現在の発言者を取得
                for(int i = 0; i < InstantiateMenber.Length; i++)
                {
                    if(speechSet[DiscussionNum].SpeechName == InstantiateMenber[i].GetComponent<SpeechName>().speechName)
                    {
                        //カメラを発言者に向ける
                        CameraPriority(i);
                    }
                }

                //発言番号と発言者を設定
                discussionUI.SpeechNumSet(DiscussionNum, speechSet[DiscussionNum].SpeechName);

                //発言表示後議論番号加算
                DiscussionNum++;
            }

            //時間経過で次の文字に進む
            currentTime += Time.deltaTime;
            if (currentTime > nextSpeechTime)
            {
                currentTime = 0;

                if (DiscussionNum < speechSet.Length)
                {
                    //発言文字の変化フラグ
                    isTextSetCalled = false;
                    Debug.Log(DiscussionNum + ":" + speechSet.Length);
                }
                else
                {
                    //一巡させる
                    TakeAround();
                }

            }
        }
    }

    //発言の移動
    public void SpeechMove(SpeechSet speech)
    {
        //移動パターンによって発言の発生位置や動きが変化
        //カメラの動きも変化

    }

    //ヴァーチャルカメラの優先度を変化してカメラを変化
    private void CameraPriority(int charaNum)
    {
        //議論メンバー＋メインカメラ
        for (int i = 0; i < InstantiateMenber.Length + 1; i++)
        {
            if(i == InstantiateMenber.Length + 1)
            {
                //Debug.Log("i == InstantiateMenber.Length + 1");
                MainCCVirtualCamera.Priority = 1;
            }
            else if(i == charaNum)
            {
                //Debug.Log("i == charaNum");
                Debug.Log(InstantiateMenber[i].name);
                InstantiateMenber[i].transform.GetChild(0).gameObject.GetComponent<CinemachineVirtualCamera>().Priority = 1;
            }
            else
            {
                //Debug.Log("else");
                if(i < InstantiateMenber.Length)
                {
                    InstantiateMenber[i].transform.GetChild(0).gameObject.GetComponent<CinemachineVirtualCamera>().Priority = 0;
                }
                
                MainCCVirtualCamera.Priority = 0;
            }
        }
    }

    //議論を開始する処理
    public void ShootingInit()
    {
        //議論前の演出の発生に移行
        discussion = DiscussionMode.BeforeDiscussion;

        //一定時間カメラを回す
        //DiscussionCamera.GetComponent<CameraRotation>().RotateOn();
        MainVirtualCamera.GetComponent<CameraRotation>().RotateOn();
    }

    //会話の発生後議論を一巡させる
    public void TakeAround()
    {
        //議論一巡時の会話データを入れる
        gameManager.OpenDiscussionWindow(DiscussionTakeAroundText, DiscussionTalkModeWindow.TalkFinish.DiscussionMode);
        //会話開始
        discussion = DiscussionMode.Talk;

        //DiscussionEnd();
        //議論番号をリセット
        DiscussionNum = 0;
    }

    //議論開始
    public void DiscussionStart()
    {
        //カメラを主人公に向ける
        CameraPriority(0);

        //最大発言番号を設定
        discussionUI.SpeechNumMaxSet(speechSet.Length);

        //UIを表示
        discussionUI.DiscussionDispUI(true);

        //上記終了後議論開始
        discussion = DiscussionMode.Shooting;
        discussionProgress = true;
    }

    //議論で発生したものをリセット
    public void DiscussionEnd()
    {
        //発言を初期化
        speechText.GetComponent<TextMeshProUGUI>().text = "";
        isTextSetCalled = false;
        //議論の生徒を消去
        for (int i = 0; i < InstantiateMenber.Length; i++)
        {
            Destroy(InstantiateMenber[i]);
        }
        //カメラの向きをリセット
        MainVirtualCamera.transform.rotation = Quaternion.identity;
    }

    //議論を停止して論破画像を表示する処理
    public void ShootingFinish()
    {
        Debug.Log("議論終了で会話移行");

        DiscussionEnd();//後で会話終了時に発生するように変える
        //初期化処理発生のフラグを戻す
        //isDiscussionInitCalled = false;

        //プレイヤーの議論操作を終了
        discussionProgress = false;

        //UIを非表示
        discussionUI.DiscussionDispUI(false);

        //論破演出画像表示

        //画面の割れる演出

        //議論終了後の会話データを入れる
        gameManager.OpenDiscussionWindow(DiscussionFinishText, DiscussionTalkModeWindow.TalkFinish.AdventureMode);
        //会話開始
        discussion = DiscussionMode.Talk;
    }

    //議論フェイズの初期化を発生させるフラグ
    public void DiscInitCallOn()
    {
        isDiscussionInitCalled = false;
    }

    //文字に当たったか判定して正しいウィークポイントか確認
    public bool TextColWeek()
    {
        var index = TMP_TextUtilities.FindIntersectingWord(speechText.GetComponent<TextMeshProUGUI>(), Input.mousePosition, Camera.main);
        if (index < 0)
            return false;

        var wordInfo = speechText.GetComponent<TextMeshProUGUI>().textInfo.wordInfo[index];

        Debug.Log("取得した発言:"+wordInfo.GetWord() + " "+ CurrentSpeechWeekPoint /*CurrentSpeech.WeekPointSpeech CurrentSpeech.WeekPointSpeech*/);

        //取得した文字がウィークポイントと同じか判定
        if(wordInfo.GetWord() == CurrentSpeechWeekPoint /*CurrentSpeech.WeekPointSpeech CurrentSpeech.WeekPointSpeech*/)
        {
            //条件が正しい時Trueを返す
            return true;
        }
        return false;
    }

    //現在のコトダマでウィークポイントを判定
    public bool TruthBulletCompare()
    {
        //コトダマのタイプとウィークポイントが論破か同意か判定
        if (discussionUI.Bullet[discussionUI.BulletCount].BulletType == CurrentSpeechType/*CurrentSpeech.speechType*/)
        {
            Debug.Log("コトダマの標的:" + discussionUI.Bullet[discussionUI.BulletCount].TargetSpeech + " 取得したウィークポイント：" + CurrentSpeechWeekPoint);
            //コトダマと発言が正しいか判定
            if (discussionUI.Bullet[discussionUI.BulletCount].TargetSpeech == CurrentSpeechWeekPoint /*CurrentSpeech.WeekPointSpeech*/)
            {
                return true;
            }
        }

        return false;
    }

    //ウィークポイントを論破出来なかった時会話に移行
    public async void DiscussionFailureChange(Vector3 BulletPos)
    {
        //失敗演出
        //着弾位置にバリアの位置を変化
        discussionUI.BarrierPosSet(BulletPos, CurrentSpeechType/*CurrentSpeech.speechType*/);

        //透明度0になるまで低下
        discussionUI.BarrierAlpha();

        //非同期で条件が満たされるまで待機
        await UniTask.WaitUntil(() => discussionUI.isBarrier);

        //議論失敗時の会話データを入れる
        gameManager.OpenDiscussionWindow(CurrentSpeech.DiscussionMistakeText, DiscussionTalkModeWindow.TalkFinish.DiscussionMode);
        //会話の開始
        discussion = DiscussionMode.Talk;
    }
    
}