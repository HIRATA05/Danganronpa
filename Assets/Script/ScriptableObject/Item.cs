using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "Item", menuName = "CreateItem")]
public class Item : ScriptableObject
{
    [Header("アイテムのID番号　ID順に並びを決める")] public int id;
    [Header("名前")] public string itemName;
    [Header("入手しているか")] public bool getFlag;
    [TextArea(2, 10), Header("説明文")] public string infomation;

    public Item(Item item)
    {
        //this.type = item.type;
        this.id = item.id;
        this.itemName = item.itemName;
        this.getFlag = item.getFlag;
        this.infomation = item.infomation;
    }
}