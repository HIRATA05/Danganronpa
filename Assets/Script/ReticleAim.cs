using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.PlayerSettings;

public class ReticleAim : MonoBehaviour
{
    //エイムの操作

    //照準
    [SerializeField] private Image aimImage;

    //照準の画像
    [SerializeField] private Sprite OnCollisionAimImage;
    [SerializeField] private Sprite DisCollisionAimImage;
    //照準の大きさ
    [SerializeField] private float OnCollisionAimSize;
    [SerializeField] private float DisCollisionAimSize;

    //回転速度
    private float RotSpeed = 0.1f;


    void Start()
    {
        
    }

    void Update()
    {
        //マウスの位置と照準器の位置を同期させる。
        transform.position = Input.mousePosition;

        RaycastHit hit;

        //MainCameraからマウスの位置にRayを飛ばす
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);


        if (Physics.Raycast(ray, out hit))
        {
            //探索可能オブジェクトに当たった時
            if (hit.transform.CompareTag("SearchObj"))
            {
                //照準を変化させる
                aimImage.sprite = OnCollisionAimImage;
                //照準サイズ変更
                transform.localScale = new Vector3(OnCollisionAimSize, OnCollisionAimSize, OnCollisionAimSize);

            }
            else
            {
                //オブジェクトに当たっていない時
                DisColliderAim();
            }
        }
        else
        {
            //オブジェクトに当たっていない時
            DisColliderAim();
        }


    }

    //探索可能オブジェクトに当たっていない時
    private void DisColliderAim()
    {
        //照準の回転
        //transformを取得
        Transform myTransform = this.transform;

        //ワールド座標を基準に回転を取得
        Vector3 worldAngle = myTransform.eulerAngles;
        worldAngle.x = 0.0f;
        worldAngle.y = 0.0f;
        worldAngle.z += RotSpeed;//回転を加算
        myTransform.eulerAngles = worldAngle;//回転角度を設定

        //照準を変化させる
        aimImage.sprite = DisCollisionAimImage;
        //照準サイズ変更
        transform.localScale = new Vector3(DisCollisionAimSize, DisCollisionAimSize, DisCollisionAimSize);

    }

}
