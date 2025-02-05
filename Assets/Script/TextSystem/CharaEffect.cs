using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharaEffect : MonoBehaviour
{
    //会話中にキャラに発生させる演出等
    //UnityEventは引数を1つしか設定できないため注意

    private GameManager gameManager;

    [SerializeField, Header("イベントCG画像")] private Image eventcgImage;
    [SerializeField, Header("フェード用のイベントCGの裏側")] private Image eventcgBack;

    Color fadeClearColor = new Color(255, 255, 255, 0);
    Color fadeColor = Color.white;

    [SerializeField] private AudioSource audioSourceBgm;
    [SerializeField] private AudioSource audioSourceSe;

    [SerializeField] private AudioClip SelfIntroSE;

    void Start()
    {
        GameObject gm = GameObject.Find("GameManager");
        gameManager = gm.GetComponent<GameManager>();
    }

    void Update()
    {
        
    }

    //引数は表情を変化させるキャラ、オブジェクトがキャラかどうか判別
    //スプライトを別の表情画像に変化
    public void ChangeFace_Normal(GameObject chara)//通常
    {
        //CharaFaceChangeがアタッチされているか確認
        if(chara != null && chara.GetComponent<CharaFaceChange>() != null)
        {
            Debug.Log("CharaEffectでCharaFaceChangeを確認");
            chara.GetComponent<CharaFaceChange>().ChangeFaceNormal();
        }
    }
    public void ChangeFace_Talk(GameObject chara)//話す
    {
        //CharaFaceChangeがアタッチされているか確認
        if (chara != null && chara.GetComponent<CharaFaceChange>() != null)
        {
            chara.GetComponent<CharaFaceChange>().ChangeFaceTalk();
        }
    }
    public void ChangeFace_Think(GameObject chara)//思考
    {
        //CharaFaceChangeがアタッチされているか確認
        if (chara != null && chara.GetComponent<CharaFaceChange>() != null)
        {
            chara.GetComponent<CharaFaceChange>().ChangeFaceThink();
        }
    }
    public void ChangeFace_Joy(GameObject chara)//笑顔
    {
        //CharaFaceChangeがアタッチされているか確認
        if (chara != null && chara.GetComponent<CharaFaceChange>() != null)
        {
            chara.GetComponent<CharaFaceChange>().ChangeFaceJoy();
        }
    }
    public void ChangeFace_Anger(GameObject chara)//怒り
    {
        //CharaFaceChangeがアタッチされているか確認
        if (chara != null && chara.GetComponent<CharaFaceChange>() != null)
        {
            chara.GetComponent<CharaFaceChange>().ChangeFaceAnger();
        }
    }
    public void ChangeFace_Surprise(GameObject chara)//驚き
    {
        //CharaFaceChangeがアタッチされているか確認
        if (chara != null && chara.GetComponent<CharaFaceChange>() != null)
        {
            chara.GetComponent<CharaFaceChange>().ChangeFaceSurprise();
        }
    }
    public void ChangeFace_Frightened(GameObject chara)
    {
        //CharaFaceChangeがアタッチされているか確認
        if (chara != null && chara.GetComponent<CharaFaceChange>() != null)
        {
            chara.GetComponent<CharaFaceChange>().ChangeFaceFrightened();
        }
    }
    public void ChangeFace_Rage(GameObject chara)//激怒
    {
        //CharaFaceChangeがアタッチされているか確認
        if (chara != null && chara.GetComponent<CharaFaceChange>() != null)
        {
            chara.GetComponent<CharaFaceChange>().ChangeFaceRage();
        }
    }
    public void ChangeFace_Excitement(GameObject chara)
    {
        //CharaFaceChangeがアタッチされているか確認
        if (chara != null && chara.GetComponent<CharaFaceChange>() != null)
        {
            chara.GetComponent<CharaFaceChange>().ChangeFaceExcitement();
        }
    }
    public void ChangeFace_Upset(GameObject chara)
    {
        //CharaFaceChangeがアタッチされているか確認
        if (chara != null && chara.GetComponent<CharaFaceChange>() != null)
        {
            chara.GetComponent<CharaFaceChange>().ChangeFaceUpset();
        }
    }
    public void ChangeFace_Despair(GameObject chara)//絶望
    {
        //CharaFaceChangeがアタッチされているか確認
        if (chara != null && chara.GetComponent<CharaFaceChange>() != null)
        {
            chara.GetComponent<CharaFaceChange>().ChangeFaceDespair();
        }
    }

    //イベントCGを表示
    public void EventImageDisplay(Sprite sprite)
    {
        eventcgImage.sprite = sprite;
        eventcgImage.enabled = true;
    }
    //イベントCGをフェードして表示
    public void EventImageDisplayFade(Sprite sprite)
    {
        Color spriteColor;
        float duration = 0.5f;

        eventcgBack.enabled = true;

        eventcgImage.sprite = sprite;
        
        eventcgImage.color = fadeClearColor;
        eventcgImage.enabled = true;

        spriteColor = eventcgImage.color;
        //eventcgImage.sprite = BlackImage;
        StartCoroutine(Fade(1, spriteColor, duration, false));

    }
    //イベントCGを非表示
    public void EventImageDelete()
    {
        eventcgImage.sprite = null;
        eventcgImage.enabled = false;
    }
    //イベントCGをフェードして非表示
    public void EventImageDeleteFade()
    {
        Color spriteColor;
        float duration = 0.3f;

        eventcgImage.color = fadeColor;
        
        spriteColor = eventcgImage.color;
        StartCoroutine(Fade(0, spriteColor, duration, true));
        //eventcgImage.sprite = null;
        //eventcgImage.enabled = false;
    }
    //自己紹介イベントを表示
    public void SelfIntoroDisplay(Sprite sprite)
    {
        GameManager.isTalkPause = true;
        eventcgImage.sprite = sprite;
        eventcgImage.enabled = true;

        //操作不能にする
        gameManager.playerController = GameManager.PlayerController.EventScene;

        //SE
        SESound(SelfIntroSE);

        //一定時間待機
        StartCoroutine(EventCGDisplay());

        //操作可能
        //gameManager.playerController = GameManager.PlayerController.TextWindowMode;
    }

    private IEnumerator EventCGDisplay()
    {
        //イベント中で文字送りできない
        GameManager.isTalkEvent = false;

        //操作不能にする
        //gameManager.playerController = GameManager.PlayerController.EventScene;

        //指定時間待機
        yield return new WaitForSeconds(3f);

        //イベント終了で文字送りできるようになる
        GameManager.isTalkEvent = true;

        //時間で画像消去
        eventcgImage.sprite = null;
        eventcgImage.enabled = false;

        GameManager.isTalkPause = false;

        //操作可能
        gameManager.playerController = GameManager.PlayerController.TextWindowMode;
    }


    IEnumerator Fade(float targetAlpha, Color spriteColor, float duration, bool isFadeOut)
    {
        while (!Mathf.Approximately(spriteColor.a, targetAlpha))
        {
            float changePerFrame = Time.deltaTime / duration;
            spriteColor.a = Mathf.MoveTowards(spriteColor.a, targetAlpha, changePerFrame);
            eventcgImage.color = spriteColor;
            yield return null;
        }
        //画像をフェードアウトする時
        if (isFadeOut)
        {
            eventcgImage.enabled = false;
            eventcgImage.sprite = null;
            eventcgImage.color = fadeColor;
        }
        else
        {
            eventcgBack.enabled = false;
            //eventcgImage.color = fadeColor;
        }
       
    }

    //オブジェクトの状態の切り替え　時計の数字など
    public void ObjSwitch(ObjModeSwitch objModeSwitch)
    {
        //切り替え後に変化
        objModeSwitch.DisplayObjSwitch();
    }
    /*
    //オブジェクトの状態の切り替え　中庭の窓
    public void WindowObjSwitch(ObjModeSwitch objModeSwitch)
    {
        //切り替え後に変化
        objModeSwitch.DisplayObjSwitch();
    }*/

    //エンディングシーンに遷移
    public void CallEndingScene()
    {
        SceneManager.LoadScene(gameManager.EndingScene);
    }

    //BGM
    public void BGMSound(AudioClip audio)
    {
        audioSourceBgm.clip = audio;
    }

    //SE
    public void SESound(AudioClip audio)
    {
        audioSourceSe.clip = audio;
    }

}
