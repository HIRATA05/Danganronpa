using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDataBase", menuName = "CreateItemDataBase")]
public class ItemDataBase : ScriptableObject
{
    //アイテム
    //[Header("アイテムリスト")] public List<Item> items = new List<Item>();

    //コトダマ
    [Header("コトダマリスト")] public List<TruthBullets> truthBullets = new List<TruthBullets>();

}