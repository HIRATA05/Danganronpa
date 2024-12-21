using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DiscussionUI : MonoBehaviour
{
    //議論のUI


    //テキスト参照
    //現在の発言番号
    [SerializeField] private TextMeshProUGUI SpeechNumCurrentText;
    //発言番号の最大
    [SerializeField] private TextMeshProUGUI SpeechNumMaxText;
    //発言者の名前
    [SerializeField] private TextMeshProUGUI SpeakerNameText;
    //発言力の画像
    [SerializeField] private Sprite LifeImageNormal;
    [SerializeField] private Sprite LifeImageDamage;

    [SerializeField] private Image[] LifePos;

    //コトダマクラス
    public class TruthBulletCylinder
    {
        //コトダマの名称
        string BulletName;

        //コトダマのタイプ
        public enum SpeechType
        {
            refute,
            consent
        }
        public SpeechType BulletType;

        //コトダマの放つ対象となる発言　BulletTypeとTargetSpeechが発言と一致した時論破出来る
        public string TargetSpeech;
    }
    //コトダマシリンダー　コトダマのクラスを後で作る
    [SerializeField] private TruthBulletCylinder[] Bullet;

    //主人公の発言力
    private int Life;
    private int MaxLife = 5;


    //発言力の設定
    public void SpeechPowerSet()
    {
        Life = MaxLife;
    }

    //現在の発言番号を設定
    public void SpeechNumSet(int num, string name)
    {
        SpeechNumCurrentText.text = num.ToString();
        SpeakerNameText.text = name;
    }

    //発言番号の最大を設定
    public void SpeechNumMaxSet(int num)
    {
        SpeechNumMaxText.text = num.ToString();
    }

    //呼び出すと発言力が1減り画像が切り替わる
    public void SpeechPoworDamage()
    {
        //発言力にダメージ
        Life--;

        if (Life > 0)
        {
            //現在の発言力によってライフ画像を変化
            LifeImageChange(Life);

        }
        else
        {
            //議論終了のテキストと処理を呼ぶ
            //論破失敗時会話 DiscussionFailureText

        }
    }

    //ライフ画像を変える
    public void LifeImageChange(int Life)
    {
        //forで順番にライフ画像を変える
        for (int i = 0; i < LifePos.Length - Life; i++)
        {
            //ダメージハートに差し替える
            LifePos[i].sprite = LifeImageDamage;
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
