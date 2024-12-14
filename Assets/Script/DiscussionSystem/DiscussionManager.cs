using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class DiscussionManager : MonoBehaviour
{
    //ノンストップ議論の全体的な動作を管理

    //ゲームマネージャー
    [SerializeField] private GameManager gameManager;

    //議論時のテキストウィンドウ
    [SerializeField] private DiscussionTalkModeWindow discussionTalkModeWindow;

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

    //次の発言までの時間
    float nextSpeechTime = 5.0f;
    //現在時間
    float currentTime = 0.0f;

    //コトダマシリンダー　コトダマのクラスを後で作る
    //[SerializeField] private コトダマクラス[] cylinder;

    //議論開始時会話
    [SerializeField] private DialogueText DiscussionStartText;
    //議論一巡時会話
    [SerializeField] private DialogueText DiscussionTakeAroundText;
    //議論終了時会話
    [SerializeField] private DialogueText DiscussionFinishText;
    //論破失敗時会話
    [SerializeField] private DialogueText DiscussionFailureText;

    //表示するテキスト
    [SerializeField] private GameObject speechText;

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

        //論破か同意か
        public enum SpeechType
        {
            refute,
            consent,
            None
        }
        public SpeechType speechType = SpeechType.None;

        //発言の移動パターン
        public enum SpeechMovePattern
        {
            RightToLeft,//右から左
            LeftToRight,//左から右
        }
        public SpeechMovePattern speechMove;
        /*
        //文字色変化の範囲
        public int WeekRangeStart = 0;
        public int WeekRangeEnd = 1;
        */
    }
    [Header("論破カラーコード#ffa500　同意カラーコード#41A2E1")]
    public SpeechSet[] speechSet;

    //発言の当たり判定の厚さ
    private float textThickX = 0.0f;
    private float textThickY = 0.0f;
    private float textThickZ = 0.01f;

    //議論の進行番号
    private int DiscussionNum = 0;

    //議論開始の際の時間
    private float elapsedTime = 0.0f;
    private float startTime = 5.0f;

    //円形に並ぶ生徒
    [SerializeField, Header("議論で並ぶキャラ")] private GameObject[] DiscussionMenber;
    //生徒生成の親オブジェクト
    [SerializeField, Header("議論者発生の親オブジェクト")] private GameObject perentObj;

    private bool isDiscussionInitCalled = false;
    private bool isTextSetCalled = false;

    //議論中かどうか
    [NonSerialized] public bool discussionProgress = false;

    void Start()
    {
        
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

            //DiscussionMenberを順番に生成
            for (int i = 0; i < DiscussionMenber.Length; i++)
            {
                Instantiate(DiscussionMenber[i], perentObj.transform);
            }
            //生徒の並びを円形に並べる
            circleDeployer.Deploy();

            //議論開始時の会話データを入れる
            gameManager.OpenDiscussionWindow(DiscussionStartText);
            //ノンストップ議論の開始
            discussion = DiscussionMode.Talk;
        }

    }

    private void DiscussionPlay()
    {
        //会話
        if (discussion == DiscussionMode.Talk)
        {
            //会話時のUI表示

        }
        //議論開始前の演出
        else if(discussion == DiscussionMode.BeforeDiscussion)
        {
            //Debug.Log("議論開始経過時間：" + elapsedTime);

            //議論開始の文字演出


            if (elapsedTime > startTime)
            {
                Debug.Log("=議論を開始=");
                elapsedTime = 0;

                //カメラを停止
                DiscussionCamera.GetComponent<CameraRotation>().RotateOff();

                //カメラを主人公に向ける

                //上記終了後議論開始
                discussion = DiscussionMode.Shooting;
                discussionProgress = true;
            }

            //時間経過
            elapsedTime += Time.deltaTime;

        }
        //議論
        else if (discussion == DiscussionMode.Shooting && discussionProgress)
        {
            //議論時のUI表示

            //一定時間ごとに文字を変化
            if (!isTextSetCalled)
            {
                isTextSetCalled = true;

                //文字の大きさと色をセット
                speechSet[DiscussionNum].NormalSpeechBefore = "<size=10><color=white>" + speechSet[DiscussionNum].NormalSpeechBefore;

                if(speechSet[DiscussionNum].speechType == SpeechSet.SpeechType.refute)
                    speechSet[DiscussionNum].WeekPointSpeech = "<size=15><color=#ffa500>" + speechSet[DiscussionNum].WeekPointSpeech;
                else if(speechSet[DiscussionNum].speechType == SpeechSet.SpeechType.consent)
                    speechSet[DiscussionNum].WeekPointSpeech = "<size=15><color=#41A2E1>" + speechSet[DiscussionNum].WeekPointSpeech;
                else
                    speechSet[DiscussionNum].WeekPointSpeech = "<size=10><color=white>" + speechSet[DiscussionNum].WeekPointSpeech;

                speechSet[DiscussionNum].NormalSpeechAfter = "<size=10><color=white>" + speechSet[DiscussionNum].NormalSpeechAfter;
                //全ての文字を合わせて1つの文字列を作る
                string speech = speechSet[DiscussionNum].NormalSpeechBefore + speechSet[DiscussionNum].WeekPointSpeech + speechSet[DiscussionNum].NormalSpeechAfter;
                Debug.Log(speech);
                //文字のデータをセットする
                speechText.GetComponent<TextMeshProUGUI>().text = speech;
                //speechText.GetComponent<TextMeshPro>().text = speechSet[DiscussionNum].Speech;

                //文字数取得 speechText.GetComponent<TextMeshProUGUI>().text.Length
                //文字の大きさ　ウィーク15　通常10
                //SpeechType.Noneの場合ウィークポイントは作らない
                if (speechSet[DiscussionNum].speechType != SpeechSet.SpeechType.None)
                {
                    //発言の文字数から文字の当たり判定を設定する
                    /*
                    speechText.AddComponent<BoxCollider>();
                    speechText.GetComponent<BoxCollider>().center = new Vector3(0, 0, 0);
                    speechText.GetComponent<BoxCollider>().size = new Vector3(0, 0, textThickZ);
                    */
                }

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

                    //文字の当たり判定を削除する
                    //Destroy(speechText.GetComponent<BoxCollider>());

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

    

    //議論を開始する処理
    public void ShootingInit()
    {
        //議論前の演出の発生に移行
        discussion = DiscussionMode.BeforeDiscussion;

        //一定時間カメラを回す
        DiscussionCamera.GetComponent<CameraRotation>().RotateOn();

    }

    //会話の発生後議論を一巡させる
    public void TakeAround()
    {
        //議論一巡時の会話データを入れる
        gameManager.OpenDiscussionWindow(DiscussionTakeAroundText);
        //会話開始
        discussion = DiscussionMode.Talk;
        //議論番号をリセット
        DiscussionNum = 0;
    }

    //議論を停止して論破画像を表示する処理
    public void ShootingFinish()
    {
        Debug.Log("議論終了で会話移行");
        //初期化処理発生のフラグを戻す
        //isDiscussionInitCalled = false;

        //プレイヤーの議論操作を終了
        discussionProgress = false;

        //論破演出画像表示

        //画面の割れる演出

        //議論終了後の会話データを入れる
        gameManager.OpenDiscussionWindow(DiscussionFinishText);
        //会話開始
        discussion = DiscussionMode.Talk;

    }

    //議論フェイズの初期化を発生させるフラグ
    public void DiscInitCallOn()
    {
        isDiscussionInitCalled = false;
    }

}