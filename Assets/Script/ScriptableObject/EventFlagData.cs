using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EventFlag", menuName = "ScriptableObjects/CreateEventFlagData")]
public class EventFlagData : ScriptableObject
{
    //現在の部屋
    [Header("現在の部屋")]
    public string currentRoom;

    //アイテム
    [Header("アイテム")]
    public ItemDataBase itemDataBase;
    public bool EscepeSwitch;

    //シナリオ進行のためのフラグ設定
    [Header("シナリオフラグ")]
    public bool GameStart;

    //ゲーム開始時教室の探索
    [Header("プロローグ")]
    public bool GameStart_Window;
    public bool GameStart_Monitor;
    public bool GameStart_Lacky;
    public bool GameStart_All;//全て完了
    public bool GameStart_All_TalkStart;

    //自己紹介
    public bool SelfIntoro_Archer;
    public bool SelfIntoro_Hacker;
    public bool SelfIntoro_PhantomThief;
    public bool SelfIntoro_All;//全て完了
    public bool SelfIntoro_Call;//全て完了後モノクマの呼び出し

    //モノクマ登場
    public bool AppMonokuma;
    //モノクマ退場
    public bool AppMonokumaAfter;
    //モノクマ遭遇で探索開始後
    public bool AdventureStart;

    //食堂
    [Header("食堂")]
    //食堂での議論完了
    public bool DiningDiscStart;
    //倉庫開放のための弓道家への要請
    public bool WarehouseRequest;
    //デジタル時計
    public bool Digitalclock;

    //中庭
    [Header("中庭")]
    //ライトからバッテリー入手
    public bool BatteryInLight;
    //2階開放のための怪盗の提案
    public bool F2Request;
    //条件が揃って2階開放イベント発生可能
    public bool F2Open;
    //ロープ付き窓
    public bool RopeWindow;
    //2階侵入後
    public bool F2Intrusion;
    //2階開放イベントの間怪盗表示
    public bool F2OpenPhantomThief;
    //2階開放イベントの間弓道家表示
    public bool F2OpenArcher;

    //倉庫
    [Header("倉庫")]
    public bool IronBars;//鉄格子探索
    public bool WarehouseArcher;//倉庫にいる弓道家
    public bool IronBarsOpen;//鉄格子解除

    //2階教室
    [Header("2階教室")]
    public bool ClassRoomF2StartPhantomThief;//2階教室イベント最初の怪盗
    public bool ClassRoomF2_Vent;//2階教室の通気口探索
    public bool ClassRoomF2_Window;//2階教室の窓探索
    public bool ClassRoomF2_All;//2階教室の全探索
    public bool ClassRoomF2_All_TalkStart;//2階教室の全探索後の会話発生

    //図書室
    [Header("図書室")]
    public bool BookShelf;//本棚
    public bool LibraryBook;//本
    public bool DuraluminCaseInteract;//ケース
    public bool DuraluminCaseLogic;//ケース謎解き前
    public bool MemoMachineWork;//本棚探索後

    //機械工作室
    [Header("機械工作室")]
    public bool PressMachineLock;//プレス機ロック確認　ハッカーへ向かう
    public bool HackerPressMachineRequest;//ハッカーへ協力要請完了　ハッカー出現
    public bool PreesMoveDown;//プレス機の起動
    public bool MonokumaGreenPreesMove;//モノクマグリーンをプレス機まで移動
    public bool PressMachineShelfUnlock;//プレスでモノクマ破壊機械工作室棚解除

    

    //脱出イベント
    public bool EscepeEvent;

    //Trueの時にイベントが発生
    [Header("室内に入った時に発生するシナリオフラグ")]
    public bool GameStart_ClassRoom;
    public void GameStart_ClassRoom_True() { GameStart_ClassRoom = true; }
    public void GameStart_ClassRoom_False() { GameStart_ClassRoom = false; }

    //食堂に入った時
    public bool RoomIn_Dining;
    //中庭に入った時
    public bool RoomIn_Garden;
    //玄関ホールに入った時
    public bool RoomIn_EntranceHall;
    //体育館に入った時
    public bool RoomIn_Gym;
    //倉庫に入った時
    public bool RoomIn_Warehouse;
    //弓道場に入った時
    public bool RoomIn_KyudoHall;
    //2階教室
    public bool RoomIn_ClassRoom_F2;
    //図書室
    public bool RoomIn_Library;
    //機械工作室
    public bool RoomIn_MachineWorkRoom;
    //ハッカーの個室
    public bool RoomIn_HackerRoom;

    public bool RoomIn;
    public void RoomIn_True() { RoomIn = true; }
    public void RoomIn_False() { RoomIn = false; }

    //リザルトのスコア
    [Header("スコア")] public int Score = 0;

}
