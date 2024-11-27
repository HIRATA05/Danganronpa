using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BulletCol : MonoBehaviour
{

    TruthBulletShot truthBulletShot;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter(Collider other)
    {
        //当たったものに文字コンポーネントがあれば
        if (other.GetComponent<TextMeshProUGUI>())
        {
            Debug.Log("文字に当たった");
            truthBulletShot.BulletMoveEnd();

            //ウィークポイントか確認

            //ウィークポイントなら論破か同意か確認

        }

        else
        {
            Debug.Log("外れた");
            //truthBulletShot.BulletMoveEnd();
        }
        
        
    }

    //当たり判定スクリプトにコトダマ管理スクリプトを渡す
    public void SetBulletScript(TruthBulletShot truthBullet)
    {
        truthBulletShot = truthBullet;
    }
}
