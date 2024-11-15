using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.Experimental.GraphView;

public class DiscussionManager : MonoBehaviour
{
    //ノンストップ議論の全体的な動作を管理

    //円形の並びを作るスクリプト
    [SerializeField] private CircleDeployer circleDeployer;

    //議論場所の中心点
    [SerializeField] private GameObject centerDiscussionPoint;

    //回転速度
    float rotSpeed = 1.0f;

    //照準


    //コトダマシリンダー


    //表示するテキスト
    [SerializeField] private TextMeshProUGUI Text;

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
    public SpeechSet[] speechSet;

    //議論の進行番号
    private int DiscussionNum = 0;

    //円形に並ぶ生徒
    [SerializeField, Header("このオブジェクトが議論で発生する")] private GameObject[] DiscussionMenber;
    //生徒生成の親オブジェクト
    [SerializeField, Header("議論者発生の親オブジェクト")] private GameObject perentObj;


    void Start()
    {
        //議論初期化処理
        DiscussionInit();

        Text.text = speechSet[0].Speech;

        string part = speechSet[0].Speech.Substring(1, 5);

        Debug.Log(part);

    }

    void Update()
    {
        
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
