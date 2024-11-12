using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscussionManager : MonoBehaviour
{
    //ノンストップ議論の全体的な動作を管理


    //議論場所の中心点
    [SerializeField] private GameObject centerDiscussionPoint;
    //回転速度
    float rotSpeed = 1.0f;

    //照準


    //コトダマシリンダー


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

    //円形に並ぶ生徒
    [SerializeField] private GameObject[] DiscussionMenber;



    //議論フェイズの初期化
    public void DiscussionInit()
    {
        
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
