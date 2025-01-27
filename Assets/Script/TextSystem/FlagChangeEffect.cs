using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        if(!eventFlagData.ClassRoomF2_Vent) eventFlagData.ClassRoomF2_Vent = true;
    }
    //2階教室通気口探索
    public void ClassRoomF2_Window_True()
    {
        if(!eventFlagData.ClassRoomF2_Window) eventFlagData.ClassRoomF2_Window = true;
    }
    //図書室の本棚を探索、機械工作室へ
    public void MemoMachineWork_True()
    {
        eventFlagData.MemoMachineWork = true;
    }
    /*
    //機械工作室のプレス機のロックを確認
    public void PressMachineLock_True()
    {
        eventFlagData.PressMachineLock = true;
    }*/

    //ハッカーへ協力要請完了
    public void HackerPressMachineRequest_True()
    {
        eventFlagData.HackerPressMachineRequest = true;
    }

    //情報処理室モニター
    public void Moniter_DataProcessingRoom_True()
    {
        eventFlagData.Moniter_DataProcessingRoom = true;
    }
    //情報処理室死体
    public void Dead_True()
    {
        eventFlagData.Dead = true;
    }
    //情報処理室通気口の蓋
    public void Vent_Huta_True()
    {
        eventFlagData.Vent_Huta = true;
    }
    //情報処理室全探索会話発生
    public void DataProcessingRoom_All_True()
    {
        if (eventFlagData.Moniter_DataProcessingRoom && eventFlagData.Dead && eventFlagData.Vent_Huta)
        {
            eventFlagData.DataProcessingRoom_All = true;
        }
    }
    //情報処理室全探索後会話終了
    public void AllTalkEndFlag_True()
    {
        eventFlagData.AllTalkEndFlag = true;
    }
    //脱出ボタン入手
    public void EscepeSwitch_True()
    {
        eventFlagData.EscepeSwitch = true;
    }
    //モノクマ初登場終了
    public void AppMonokumaEnd_True()
    {
        eventFlagData.AppMonokumaEnd = true;
    }

    //部屋移動
    public void SceneChange(string scene)
    {
        SceneManager.LoadScene(scene);
    }

}
