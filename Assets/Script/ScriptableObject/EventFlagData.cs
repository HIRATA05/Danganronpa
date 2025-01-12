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
    //

    //2階開放のための弓道家への要請
    public bool F2Request;
    //条件が揃って2階開放イベント発生可能
    public bool F2Open;
    //ロープ付き窓
    public bool RopeWindow;
    //2階侵入後
    public bool F2Intrusion;
    //2階開放イベントの間弓道家表示
    public bool F2OpenArcher;

    //倉庫
    [Header("倉庫")]
    public bool IronBars;//鉄格子探索
    public bool WarehouseArcher;//倉庫にいる弓道家
    public bool IronBarsOpen;//鉄格子解除

    //Trueの時にイベントが発生
    [Header("室内に入った時に発生するシナリオフラグ")]
    public bool GameStart_ClassRoom;
    public void GameStart_ClassRoom_True() { GameStart_ClassRoom = true; }
    public void GameStart_ClassRoom_False() { GameStart_ClassRoom = false; }

    //食堂に入った時
    public bool RoomIn_Dining;
    //中庭に入った時
    public bool RoomIn_Garden;
    //倉庫に入った時
    public bool RoomIn_Warehouse;

    public bool RoomIn;
    public void RoomIn_True() { RoomIn = true; }
    public void RoomIn_False() { RoomIn = false; }

    //キャラの配置フラグ
    

}
