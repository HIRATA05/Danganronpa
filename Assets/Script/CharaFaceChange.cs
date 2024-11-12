using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharaFaceChange : MonoBehaviour
{
    //キャラにアタッチする
    //キャラの表情を変化させる　イマージのスプライトを差し替える

    //キャラの表情の数列挙型を増やす
    public enum Face
    {
        Normal = 0,
        Talk,
        Think,
        Joy,
        Anger,
        Surprise,
        Frightened,
        Rage,
        Excitement,
        Upset,
        Despair
    }

    [Header("0通常/1話す/2思案/3喜び/4怒り/5驚き/6怯え/7激怒/8興奮/9焦り/10絶望")]
    [SerializeField] Sprite[] faceImage;

    //室内で表示されるキャラのデフォルトスプライト
    [SerializeField] Sprite defaultFace;

    void Start()
    {

    }

    void Update()
    {
        
    }

    // 表情を変える
    public void ChangeFace(Face face)
    {
        gameObject.GetComponent<Image>().sprite = faceImage[(int)face];
    }

    public void ChangeFaceNormal() => ChangeFace(Face.Normal);

    public void ChangeFaceTalk() => ChangeFace(Face.Talk);

    public void ChangeFaceThink() => ChangeFace(Face.Think);

    public void ChangeFaceJoy() => ChangeFace(Face.Joy);

    public void ChangeFaceAnger() => ChangeFace(Face.Anger);

    public void ChangeFaceSurprise() => ChangeFace(Face.Surprise);

    public void ChangeFaceFrightened() => ChangeFace(Face.Frightened);

    public void ChangeFaceRage() => ChangeFace(Face.Rage);

    public void ChangeFaceExcitement() => ChangeFace(Face.Excitement);

    public void ChangeFaceUpset() => ChangeFace(Face.Upset);

    public void ChangeFaceDespair() => ChangeFace(Face.Despair);


    //デフォルトスプライトを変化
    public void DefaultFaceChange(Face face)
    {
        defaultFace = faceImage[(int)face];
    }

}
