using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;

public class DiscussionUI : MonoBehaviour
{
    //議論のUI


    //UIのウィンドウ
    [SerializeField] private GameObject DiscussionWindow;
    //現在の発言番号
    [SerializeField] private TextMeshProUGUI SpeechNumCurrentText;
    //発言番号の最大
    [SerializeField] private TextMeshProUGUI SpeechNumMaxText;
    //発言者の名前
    [SerializeField] private TextMeshProUGUI SpeakerNameText;
    //現在のコトダマの名前
    [SerializeField] private TextMeshProUGUI BulletNameText;
    //発言力の画像
    [SerializeField] private Sprite LifeImageNormal;
    [SerializeField] private Sprite LifeImageDamage;

    [SerializeField] private GameObject SpeechPoworPanel;

    [SerializeField] private Image[] LifePos;

    //論破失敗時のバリア
    [SerializeField] private GameObject Barrier;
    //バリア演出終了フラグ
    [NonSerialized] public bool isBarrier = false;
    //論破する時のバリアの色
    private Color refuteColor = new Color(255, 200, 0, 255);
    private Color consentColor = new Color(255, 200, 0, 255);
    //バリアの待機時間
    private float barrierWaitTime = 1.0f;

    //コトダマのタイプ
    public enum SpeechType
    {
        refute,
        consent,
        None
    }

    //コトダマクラス
    [System.Serializable]
    public class TruthBulletCylinder
    {
        //コトダマ情報
        public TruthBullets truthBullets;
        
        public SpeechType BulletType;

        //コトダマの放つ対象となる発言　BulletTypeとTargetSpeechが発言と一致した時論破出来る
        public string TargetSpeech;

        //論破失敗時会話
        //public DialogueText DiscussionFailureText;
    }
    //コトダマシリンダー　コトダマのクラスを後で作る
    public TruthBulletCylinder[] Bullet;

    public int BulletCount;//コトダマの番号を変える関数を作ること

    //主人公の発言力
    private int Life;
    private int MaxLife = 5;


    //議論UIの表示
    public void DiscussionDispUI(bool isActive)
    {
        if(DiscussionWindow.activeSelf != isActive)
        {
            DiscussionWindow.SetActive(isActive);
        }
        
    }

    //コトダマの設定
    public void BulletSelectSet()
    {
        BulletCount = 0;

        //コトダマの名前表示を設定
        BulletNameText.text = Bullet[BulletCount].truthBullets.bulletName;
    }

    //コトダマの選択を切り替える
    public void BulletSelectChange()
    {
        //番号を加算
        BulletCount++;
        Debug.Log("番号:" + BulletCount + " Length"+ Bullet.Length);
        //最大数超過で0にする
        if (BulletCount >= Bullet.Length)
        {
            BulletCount = 0;
        }
        Debug.Log("コトダマの番号:"+ BulletCount);
        //コトダマの名前表示を更新
        BulletNameText.text = Bullet[BulletCount].truthBullets.bulletName;
    }

    //発言力の設定
    public void SpeechPowerSet()
    {
        Life = MaxLife;
    }

    //現在の発言番号を設定
    public void SpeechNumSet(int num, string name)
    {
        num++;
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
        //発言力UIを表示
        if (!SpeechPoworPanel.activeSelf)
        {
            SpeechPoworPanel.SetActive(true);
        }

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
            //論破ゲームオーバー時会話 DiscussionGameOverText
            Debug.Log("論破ゲームオーバー時会話");
        }
    }

    //ライフ画像を変える
    public void LifeImageChange(int Life)
    {
        //現在ライフの一番上を一瞬伸縮させる


        //forで順番にライフ画像を変える
        for (int i = 0; i < LifePos.Length - Life; i++)
        {
            //ダメージハートに差し替える
            LifePos[i].sprite = LifeImageDamage;
        }
    }

    /*
    //論破失敗演出
    public async void DiscussionFailureBarrier(Vector3 BulletPos)
    {
        //失敗演出
        //着弾位置にバリアの位置を変化
        BarrierPosSet(BulletPos);

        //透明解除


        //透明度0になるまで低下
        BarrierAlpha();

        //非同期で条件が満たされるまで待機
        await UniTask.WaitUntil(() => isBarrier);

    }*/

    //バリアの位置をセット
    public void BarrierPosSet(Vector3 Pos, DiscussionUI.SpeechType speechType)
    {
        //位置を変化
        Barrier.transform.position = Pos;

        //発言のタイプによってバリアの色を変える
        if(speechType == SpeechType.refute)
        {
            Barrier.GetComponent<Image>().color = refuteColor;
        }
        else if (speechType == SpeechType.consent)
        {
            Barrier.GetComponent<Image>().color = consentColor;
        }

        //バリア演出終了フラグをFalse
        isBarrier = false;
    }

    //バリアの透明度を段々と低下
    public /*async*/ void BarrierAlpha()
    {
        //透明度0になるまで低下
        for (int i = 0; i < 255; i++)
        {
            //透明度を減らす
            Barrier.GetComponent<Image>().color = Barrier.GetComponent<Image>().color - new Color32(0, 0, 0, 1);
            //時間待機
            //await UniTask.Delay(TimeSpan.FromSeconds(barrierWaitTime));
        }
        //バリア演出終了フラグをTrue
        isBarrier = true;
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
