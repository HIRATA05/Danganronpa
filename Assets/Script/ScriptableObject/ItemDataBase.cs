using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDataBase", menuName = "CreateItemDataBase")]
public class ItemDataBase : ScriptableObject
{
    //�A�C�e��
    //[Header("�A�C�e�����X�g")] public List<Item> items = new List<Item>();

    //�R�g�_�}
    [Header("�R�g�_�}���X�g")] public List<TruthBullets> truthBullets = new List<TruthBullets>();

}