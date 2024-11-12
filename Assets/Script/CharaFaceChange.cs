using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharaFaceChange : MonoBehaviour
{
    //キャラにアタッチする
    //キャラの表情を変化させる　イマージのスプライトを差し替える

    //キャラの表情の数列挙型を増やす
    enum Face
    {
        Normal = 0,
        
    }

    [Header("0通常/1")]
    [SerializeField] Sprite[] faceImage;


    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void FaceChangeNormal()
    {
        Debug.Log("表情を通常に変化");
        gameObject.GetComponent<Image>().sprite = faceImage[(int)Face.Normal];
    }
}
