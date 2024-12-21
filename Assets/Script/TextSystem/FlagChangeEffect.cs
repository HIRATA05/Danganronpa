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
    
    //議論に移動
    public void DiscussionModeSwitch()
    {
        gameManager.isDiscussionStart = true;
    }

    //スクリプタブルのフラグを変化させてイベントを制御する
    public void GameStart_Change()
    {
        Debug.Log("GameStartをTRUE");
        gameManager.eventFlagData.GameStart = true;
    }

    public void RoomIn_Change_True()
    {
        Debug.Log("RoomInをTRUE");
        gameManager.eventFlagData.RoomIn_True();
    }
    public void RoomIn_Change_False()
    {
        Debug.Log("RoomInをFALSE");
        gameManager.eventFlagData.RoomIn_False();
    }
}
