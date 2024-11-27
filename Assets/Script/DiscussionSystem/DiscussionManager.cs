using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DiscussionManager : MonoBehaviour
{
    //ノンストップ議論の全体的な動作を管理

    //ゲームマネージャー
    [SerializeField] private GameManager gameManager;

    //円形の並びを作るスクリプト
    [SerializeField] private CircleDeployer circleDeployer;

    enum DiscussionMode
    {
        Talk,
        Shooting
    }
    DiscussionMode discussion;


    //照準
    [SerializeField] private Image aimImage;
    //照準の画像
    [SerializeField] private Sprite aimImageNormal;
    [SerializeField] private Sprite aimImageColText;

    //議論場所の中心点
    [SerializeField] private GameObject centerDiscussionPoint;

    //回転速度
    float rotSpeed = 1.0f;

    //次の発言までの時間
    float nextSpeechTime = 5.0f;
    //現在時間
    float currentTime = 0.0f;

    //コトダマシリンダー


    //表示するテキスト
    [SerializeField] private GameObject speechText;
    //論破カラーコード#ffa500　同意カラーコード#41A2E1
    //セリフ
    [System.Serializable]
    public class SpeechSet
    {
        //発言
        public string Speech;

        //発言者の名前
        public string SpeechName;

        //論破か同意か
        public enum SpeechType
        {
            refute,
            consent,
            None
        }
        public SpeechType speechType = SpeechType.None;

        //文字色変化の範囲
        public int WeekRangeStart = 0;
        public int WeekRangeEnd = 1;
    }
    [Header("論破カラーコード#ffa500　同意カラーコード#41A2E1")]
    public SpeechSet[] speechSet;

    private float textThickZ = 0.01f;

    //議論の進行番号
    private int DiscussionNum = 0;

    //円形に並ぶ生徒
    [SerializeField, Header("議論で並ぶキャラ")] private GameObject[] DiscussionMenber;
    //生徒生成の親オブジェクト
    [SerializeField, Header("議論者発生の親オブジェクト")] private GameObject perentObj;

    private bool isTextSetCalled = false;

    void Start()
    {
        //議論初期化処理
        DiscussionInit();


    }

    void Update()
    {
        if (!isTextSetCalled)
        {
            isTextSetCalled = true;
            //文字のデータをセットする
            speechText.GetComponent<TextMeshProUGUI>().text = speechSet[DiscussionNum].Speech;

            //文字数 speechText.GetComponent<TextMeshProUGUI>().text.Length

            //文字の当たり判定を設定する
            speechText.AddComponent<BoxCollider>();
            speechText.GetComponent<BoxCollider>().center = new Vector3(0, 0, 0);
            speechText.GetComponent<BoxCollider>().size = new Vector3(0, 0, textThickZ);
        }
        

        //時間経過で次の文字に進む
        currentTime += Time.deltaTime;
        if (currentTime > nextSpeechTime)
        {
            currentTime = 0;

            if (DiscussionNum < speechSet.Length)
            {
                DiscussionNum++;
                //文字の当たり判定を削除する
                Destroy(speechText.GetComponent<BoxCollider>());
                isTextSetCalled = false;
            }
            
        }
        
    }

    //発言の移動
    public void SpeechMove(SpeechSet speech)
    {
        
    }

    //議論フェイズの初期化
    public void DiscussionInit()
    {
        //議論番号初期化
        DiscussionNum = 0;

        //DiscussionMenberを順番に生成
        for (int i = 0; i < DiscussionMenber.Length; i++) {
            Instantiate(DiscussionMenber[i], perentObj.transform);
        }
        //生徒の並びを円形に並べる
        circleDeployer.Deploy();
    }
}
