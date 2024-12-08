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

    //Trueの時にイベントが発生
    [Header("室内に入った時に発生するシナリオフラグ")]
    public bool GameStart_ClassRoom;
    public void GameStart_ClassRoom_True() { GameStart_ClassRoom = true; }
    public void GameStart_ClassRoom_False() { GameStart_ClassRoom = false; }

    public bool RoomIn;
    public void RoomIn_True() { RoomIn = true; }
    public void RoomIn_False() { RoomIn = false; }

}
