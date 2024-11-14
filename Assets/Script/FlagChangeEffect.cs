using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagChangeEffect : MonoBehaviour
{
    //会話中にイベントフラグを変化させる
    //UnityEventは引数を1つしか設定できないため注意

    //ゲームマネージャーの取得
    private GameManager gameManager;


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
    public void GameStart_Change()
    {
        Debug.Log("GameStartをTRUE");
        gameManager.eventFlagData.GameStart = true;
    }
}
