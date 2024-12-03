using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BulletCol : MonoBehaviour
{

    TruthBulletShot truthBulletShot;


    void Start()
    {
        
    }

    void Update()
    {

    }

    public void OnTriggerEnter(Collider other)
    {
        //当たったものに文字コンポーネントがあれば
        if (other.GetComponent<TextMeshProUGUI>())
        {
            Debug.Log("文字に当たった");


            truthBulletShot.BulletCol();

        }

    }

    //当たり判定スクリプトにコトダマ管理スクリプトを渡す
    public void SetBulletScript(TruthBulletShot truthBullet)
    {
        truthBulletShot = truthBullet;
    }
}
