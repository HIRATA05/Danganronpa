using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EventFlag", menuName = "ScriptableObjects/CreateEventFlagData")]
public class EventFlagData : ScriptableObject
{
    //アイテム
    [Header("アイテム")]
    public bool rope;

    //コトダマ
    [Header("コトダマ")]
    public bool evidence;

    //シナリオ進行のためのフラグ設定
    [Header("シナリオフラグ")]
    public bool GameStart;

}
