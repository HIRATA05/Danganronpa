using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CharaEffect : MonoBehaviour
{
    //会話中にキャラに発生させる演出


    
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    //引数はゲームオブジェクトにして、オブジェクトがキャラかどうか判別
    //スプライトを別の表情画像に変化
    public void FaceChangeNormal(GameObject chara)
    {
        //CharaFaceChangeがアタッチされているか確認
        if(chara != null && chara.GetComponent<CharaFaceChange>() != null)
        {
            Debug.Log("CharaEffectでCharaFaceChangeを確認");
            chara.GetComponent<CharaFaceChange>().FaceChangeNormal();
        }
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

    //ただSEを発生させる


    public void OnClickEvent()
    {
        Debug.Log("Click");
    }

}
