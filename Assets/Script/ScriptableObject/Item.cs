using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "Item", menuName = "CreateItem")]
public class Item : ScriptableObject
{
    [Header("�A�C�e����ID�ԍ��@ID���ɕ��т����߂�")] public int id;
    [Header("���O")] public string itemName;
    [Header("���肵�Ă��邩")] public bool getFlag;
    [TextArea(2, 10), Header("������")] public string infomation;

    public Item(Item item)
    {
        //this.type = item.type;
        this.id = item.id;
        this.itemName = item.itemName;
        this.getFlag = item.getFlag;
        this.infomation = item.infomation;
    }
}