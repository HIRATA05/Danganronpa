using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "TruthBullets", menuName = "CreateTruthBullets")]
public class TruthBullets : ScriptableObject
{
    [Header("�R�g�_�}��ID�ԍ��@ID���ɕ��т����߂�")] public int id;
    [Header("���O")] public string bulletName;
    [Header("���肵�Ă��邩")] public bool getFlag;
    [TextArea(2, 10), Header("������")] public string infomation;

    public TruthBullets(TruthBullets bullet)
    {
        this.id = bullet.id;
        this.bulletName = bullet.bulletName;
        this.getFlag = bullet.getFlag;
        this.infomation = bullet.infomation;
    }
}