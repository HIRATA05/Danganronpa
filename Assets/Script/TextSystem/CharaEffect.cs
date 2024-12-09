using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class CharaEffect : MonoBehaviour
{
    //会話中にキャラに発生させる演出等
    //UnityEventは引数を1つしか設定できないため注意

    [SerializeField, Header("イベントCG画像")] private Image eventcgImage;
    [SerializeField, Header("フェード用のイベントCGの裏側")] private Image eventcgBack;

    Color fadeClearColor = new Color(255, 255, 255, 0);
    Color fadeColor = Color.white;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    //引数は表情を変化させるキャラ、オブジェクトがキャラかどうか判別
    //スプライトを別の表情画像に変化
    public void FaceChange_Normal(GameObject chara)
    {
        //CharaFaceChangeがアタッチされているか確認
        if(chara != null && chara.GetComponent<CharaFaceChange>() != null)
        {
            Debug.Log("CharaEffectでCharaFaceChangeを確認");
            chara.GetComponent<CharaFaceChange>().ChangeFaceNormal();
        }
    }
    public void FaceChange_Talk(GameObject chara)
    {
        //CharaFaceChangeがアタッチされているか確認
        if (chara != null && chara.GetComponent<CharaFaceChange>() != null)
        {
            chara.GetComponent<CharaFaceChange>().ChangeFaceTalk();
        }
    }
    public void ChangeFace_Think(GameObject chara)
    {
        //CharaFaceChangeがアタッチされているか確認
        if (chara != null && chara.GetComponent<CharaFaceChange>() != null)
        {
            chara.GetComponent<CharaFaceChange>().ChangeFaceThink();
        }
    }
    public void ChangeFace_Joy(GameObject chara)
    {
        //CharaFaceChangeがアタッチされているか確認
        if (chara != null && chara.GetComponent<CharaFaceChange>() != null)
        {
            chara.GetComponent<CharaFaceChange>().ChangeFaceJoy();
        }
    }
    public void ChangeFace_Anger(GameObject chara)
    {
        //CharaFaceChangeがアタッチされているか確認
        if (chara != null && chara.GetComponent<CharaFaceChange>() != null)
        {
            chara.GetComponent<CharaFaceChange>().ChangeFaceAnger();
        }
    }
    public void ChangeFace_Surprise(GameObject chara)
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

    //印象的な発言の時に発生する白いフェード
    //SEを発生させ一瞬画像を白くして元に戻す
    public void ImageFedeWhite(Image charaImage)
    {
        //色を取得
        Color color = charaImage.color;

        //SE発生


        //画像のフェード処理

    }

    //SEを発生させる



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
}
