using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagChangeEffect : MonoBehaviour
{
    //会話中にイベントフラグを変化させる
    //UnityEventは引数を1つしか設定できないため注意

    //ゲームマネージャーの取得
    private GameManager gameManager;
    private EventFlagData eventFlagData;

    void Start()
    {
        GameObject gm = GameObject.Find("GameManager");
        gameManager = gm.GetComponent<GameManager>();
        eventFlagData = gameManager.eventFlagData;
    }

    void Update()
    {
        
    }
    
    //この関数を設定すると議論に移動する
    public void DiscussionModeSwitch()
    {
        gameManager.isDiscussionStart = true;
    }

    //スクリプタブルのフラグを変化させてイベントを制御する
    public void GameStart_Change()
    {
        Debug.Log("GameStartをTRUE");
        eventFlagData.GameStart = true;
    }

    public void GameStart_WindowChange_True()
    {
        Debug.Log("GameStart_WindowをTRUE");
        eventFlagData.GameStart_Window = true;
    }
    public void GameStart_MonitorChange_True()
    {
        Debug.Log("GameStart_MonitorをTRUE");
        eventFlagData.GameStart_Monitor = true;
    }
    public void GameStart_LackyChange_True()
    {
        Debug.Log("GameStart_LackyをTRUE");
        eventFlagData.GameStart_Lacky = true;
    }
    public void GameStart_All_True()
    {
        if (eventFlagData.GameStart_Lacky && eventFlagData.GameStart_Monitor && eventFlagData.GameStart_Window)
        {
            eventFlagData.GameStart_All = true;
        }
    }

    public void GameStart_ClassRoom_Change_True()
    {
        Debug.Log("GameStart_ClassRoomをTRUE");
        eventFlagData.GameStart_ClassRoom_True();
    }
    public void GameStart_ClassRoom_Change_False()
    {
        Debug.Log("GameStart_ClassRoomをFALSE");
        eventFlagData.GameStart_ClassRoom_False();
    }

    public void RoomIn_Change_True()
    {
        Debug.Log("RoomInをTRUE");
        eventFlagData.RoomIn_True();
    }
    public void RoomIn_Change_False()
    {
        Debug.Log("RoomInをFALSE");
        eventFlagData.RoomIn_False();
    }

    //コトダマ入手
    public void TruesBulletGet_MonokumaInfo()
    {
        eventFlagData.itemDataBase.truthBullets[0].getFlag = true;
    }
    public void TruesBulletGet_Rope()
    {
        eventFlagData.itemDataBase.truthBullets[1].getFlag = true;
    }
    public void TruesBulletGet_ArcherySet()
    {
        eventFlagData.itemDataBase.truthBullets[2].getFlag = true;
    }
    public void TruesBulletGet_Battery()
    {
        eventFlagData.itemDataBase.truthBullets[3].getFlag = true;
    }
    public void TruesBulletGet_Claw()
    {
        eventFlagData.itemDataBase.truthBullets[5].getFlag = true;
    }
    public void TruesBulletGet_NotePc()
    {
        eventFlagData.itemDataBase.truthBullets[6].getFlag = true;
    }
    public void TruesBulletGet_Korosiai()
    {
        eventFlagData.itemDataBase.truthBullets[7].getFlag = true;
    }
    public void TruesBulletGet_Key()
    {
        eventFlagData.itemDataBase.truthBullets[8].getFlag = true;
    }

    //自己紹介
    public void SelfIntoro_Archer_True()
    {
        eventFlagData.SelfIntoro_Archer = true;
    }
    public void SelfIntoro_Hacker_True()
    {
        eventFlagData.SelfIntoro_Hacker = true;
    }
    public void SelfIntoro_PhantomThief_True()
    {
        eventFlagData.SelfIntoro_PhantomThief = true;
    }
    public void SelfIntoro_All_True()
    {
        if(eventFlagData.SelfIntoro_Archer && eventFlagData.SelfIntoro_Hacker && eventFlagData.SelfIntoro_PhantomThief)
        {
            eventFlagData.SelfIntoro_All = true;
        }
    }

    //食堂に入った時の自動発生会話フラグ
    public void RoomIn_Dining_True()
    {
        eventFlagData.RoomIn_Dining = true;
    }
    //鉄格子の解除開始
    public void IronBars_True()
    {
        eventFlagData.IronBars = true;
    }
    //倉庫の仕掛け解除の要請
    public void WarehouseRequest_True()
    {
        eventFlagData.WarehouseRequest = true;
    }
    //鉄格子の解除
    public void IronBarsOpen_True()
    {
        eventFlagData.IronBarsOpen = true;
    }
    //中庭の2階開放イベント発生
    public void F2Request_True()
    {
        eventFlagData.F2Request = true;
    }
    //2階開放イベント達成可能
    public void F2Open_True()
    {
        if(gameManager.eventFlagData.itemDataBase.truthBullets[1].getFlag && gameManager.eventFlagData.itemDataBase.truthBullets[2].getFlag &&
            gameManager.eventFlagData.itemDataBase.truthBullets[5].getFlag)
        {
            eventFlagData.F2Open = true;
        }
    }
    //2階教室通気口探索
    public void ClassRoomF2_Vent_True()
    {
        eventFlagData.ClassRoomF2_Vent = true;
    }
    //2階教室通気口探索
    public void ClassRoomF2_Window_True()
    {
        eventFlagData.ClassRoomF2_Window = true;
    }
    //図書室の本棚を探索、機械工作室へ
    public void MemoMachineWork_True()
    {
        eventFlagData.MemoMachineWork = true;
    }
    //ハッカーへ協力要請

    //プレスでモノクマ破壊

    
}
