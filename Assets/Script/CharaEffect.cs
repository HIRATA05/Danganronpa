using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CharaEffect : MonoBehaviour
{
    //会話中にキャラに発生させる演出等
    //UnityEventは引数を1つしか設定できないため注意

    [SerializeField] private Image eventcgImage;

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
    //イベントCGを非表示
    public void EventImageDelete() { eventcgImage.enabled = false; }

    //印象的な発言の時に発生する白いフェード
    //SEを発生させ一瞬画像を白くして元に戻す
    public void ImageFedeWhite(Image charaImage)
    {
        //色を取得
        Color color = charaImage.color;

        //SE発生


        //画像のフェード処理

    }

    //ただSEを発生させる


    public void OnClickEvent()
    {
        Debug.Log("Click");
    }

}
