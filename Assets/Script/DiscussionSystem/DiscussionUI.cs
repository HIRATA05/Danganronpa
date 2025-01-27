using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using Unity.VisualScripting;

public class DiscussionUI : MonoBehaviour
{
    //議論のUI

    //ゲームマネージャー
    [SerializeField] private GameManager gameManager;
    //議論全体の管理
    //[SerializeField] private DiscussionManager discussionManager;
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
    //発射するコトダマの表示名
    [SerializeField] private TextMeshProUGUI ShotBulletText;
    //発言力の画像
    [SerializeField] private Sprite LifeImageNormal;
    [SerializeField] private Sprite LifeImageDamage;

    [SerializeField] private GameObject SpeechPoworPanel;

    [SerializeField] private Image[] LifePos;

    //議論開始演出
    [SerializeField] private GameObject NonStopDiscussionEffect;
    [SerializeField] private GameObject StartEffect;
    

    //論破失敗時のバリア
    [SerializeField] private GameObject Barrier;
    //バリア演出終了フラグ
    [NonSerialized] public bool isBarrier = false;
    //論破演出画像
    [SerializeField] private GameObject RefuteEffect;
    [SerializeField] private GameObject ConsentEffect;
    //同意相手の画像
    [SerializeField] private Image ConsentChara;
    //論破演出終了フラグ
    [NonSerialized] public bool isRefuteFinish = false;
    //論破する時のバリアの色
    private Color32 refuteColor = new Color32(255, 200, 0, 255);
    private Color32 consentColor = new Color32(0, 200, 255, 255);
    //バリアの待機時間
    //private float barrierWaitTime = 0.1f;

    //コトダマのタイプ
    public enum SpeechType
    {
        refute,//論破
        consent,//同意
        None//無し
    }

    //コトダマクラス
    [System.Serializable]
    public class TruthBulletCylinder
    {
        //コトダマ情報
        public TruthBullets truthBullets;

        //コトダマのタイプ
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
    public int MaxLife = 5;

    //フェード用演出パネル
    [SerializeField] private Image FadeEffect;

    

    //議論UIの表示
    public void DiscussionDispUI(bool isActive)
    {
        if(DiscussionWindow.activeSelf != isActive)
        {
            DiscussionWindow.SetActive(isActive);
            SpeechPoworPanel.SetActive(isActive);
        }
    }

    //コトダマの設定
    public void BulletSelectSet()
    {
        BulletCount = 0;

        //コトダマの名前表示を設定
        BulletNameText.text = Bullet[BulletCount].truthBullets.bulletName;

        //発射するコトダマの名前表示を変える
        ShotBulletText.text = Bullet[BulletCount].truthBullets.bulletName;
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

        //発射するコトダマの名前表示を変える
        ShotBulletText.text = Bullet[BulletCount].truthBullets.bulletName;
    }

    //コトダマ発射時のシリンダーの文字消去
    public void CylinderTextErase()
    {
        //コトダマの名前表示を透明
        BulletNameText.color = Color.clear;
        
    }

    //コトダマ発射時のシリンダーの文字解除
    public void CylinderTextReturn()
    {
        //色を黒に変えて解除
        BulletNameText.color = Color.black;
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
        if(Life < 0)//0以下になった時0にする
        {
            Life = 0;
        }

        if (Life > 0)
        {
            //現在の発言力によってライフ画像を変化
            LifeImageChange(Life);

        }
        /*
        else
        {
            //議論終了のテキストと処理を呼ぶ
            //論破ゲームオーバー時会話 DiscussionGameOverText
            //discussionManager.DiscussionGameOver();
            Debug.Log("論破ゲームオーバー時会話");
        }
        */
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

    //ライフ数によってスコアを加算
    public void LifeCountScoreUp()
    {
        gameManager.eventFlagData.Score += (Life * 5);
    }

    //論破演出
    public IEnumerator RefuteImageEffect(DiscussionUI.SpeechType speechType)
    {
        //論破演出開始
        isRefuteFinish = false;
        //発言のタイプによって表示する画像を変える
        if (speechType == SpeechType.refute)
        {
            //矛盾論破演出画像表示
            RefuteEffect.SetActive(true);
        }
        else if (speechType == SpeechType.consent)
        {
            //同意論破演出画像表示
            ConsentEffect.SetActive(true);
        }

        //待機時間設定
        float waitTime = 3;
        //一定時間待機
        yield return new WaitForSeconds(waitTime);
        /*
        Color color = FadeEffect.color;
        float duration = 1.0f;
        int FadeInAlpha = 1;
        int FadeOutAlpha = 0;
        //フェードインさせる
        while (!Mathf.Approximately(color.a, FadeInAlpha))
        {
            float changePerFrame = Time.deltaTime / duration;
            color.a = Mathf.MoveTowards(color.a, FadeInAlpha, changePerFrame);
            FadeEffect.color = color;
            yield return null;
        }
        */
        

        //フェードインさせる
        StartCoroutine(gameManager.FadeScreen(true));

        //論破演出画像を非表示
        if (RefuteEffect.activeSelf)
        {
            RefuteEffect.SetActive(false);
        }
        if (ConsentEffect.activeSelf)
        {
            ConsentEffect.SetActive(false);
        }
        //論破演出終了
        isRefuteFinish = true;

        //フェードアウトさせる
        StartCoroutine(gameManager.FadeScreen(false));
        /*
        //フェードアウトさせる
        while (!Mathf.Approximately(color.a, FadeOutAlpha))
        {
            float changePerFrame = Time.deltaTime / duration;
            color.a = Mathf.MoveTowards(color.a, FadeOutAlpha, changePerFrame);
            FadeEffect.color = color;
            yield return null;
        }
        */
    }

    //同意相手の画像を設定
    public void ConsentCharaSetting(Sprite consentChara)
    {
        ConsentChara.sprite = consentChara;
    }

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
        else if (speechType == SpeechType.None)
        {
            Barrier.GetComponent<Image>().color = refuteColor;
        }
        
        //Barrier.GetComponent<Image>().color = Color.white;
        //バリア演出終了フラグをFalse
        isBarrier = false;
    }

    //バリアの透明度を段々と低下
    public IEnumerator BarrierAlpha()
    {
        //透明度0になるまで低下
        for (int i = 0; i < 255; i++)
        {
            Debug.Log("バリアの透明度低下中");
            //透明度を減らす
            Barrier.GetComponent<Image>().color -= new Color32(0, 0, 0, 1);
            //1フレーム待機
            yield return null;
        }
        Debug.Log("バリアの透明度低下終了");
        //バリア演出終了フラグをTrue
        isBarrier = true;
    }

    //議論開始演出
    public IEnumerator DiscussionBeforeStartEffect()
    {
        //ノンストップ議論表示

        NonStopDiscussionEffect.SetActive(true);
        //アニメーション

        yield return new WaitForSeconds(3);

        //ノンストップ議論表示消去
        NonStopDiscussionEffect.SetActive(false);
        //開始文字表示
        StartEffect.SetActive(true);
        //アニメーション

        yield return new WaitForSeconds(2);

        //開始文字表示消去
        StartEffect.SetActive(false);
    }

    
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
