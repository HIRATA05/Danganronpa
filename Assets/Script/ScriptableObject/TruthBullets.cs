using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "TruthBullets", menuName = "CreateTruthBullets")]
public class TruthBullets : ScriptableObject
{
    [Header("コトダマのID番号　ID順に並びを決める")] public int id;
    [Header("名前")] public string bulletName;
    [Header("入手しているか")] public bool getFlag;
    [TextArea(2, 10), Header("説明文")] public string infomation;

    public TruthBullets(TruthBullets bullet)
    {
        this.id = bullet.id;
        this.bulletName = bullet.bulletName;
        this.getFlag = bullet.getFlag;
        this.infomation = bullet.infomation;
    }
}