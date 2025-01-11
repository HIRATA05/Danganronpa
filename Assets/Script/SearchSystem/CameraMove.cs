using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public class CameraMove : MonoBehaviour
{

    //ゲームマネージャー
    [SerializeField] private GameManager gameManager;

    //カメラ
    [SerializeField] private GameObject moveCamera;

    //回転する時の回転軸
    [SerializeField] private GameObject cameraShaft;

    //カメラ移動範囲
    [SerializeField] private float cameraRotLimit_Right = 30.0f;
    [SerializeField] private float cameraRotLimit_Left = -30.0f;
    /*
    [SerializeField] private float cameraRotLimit_Top = 0.0f;
    [SerializeField] private float cameraRotLimit_Bottom = 0.0f;
    */
    //回転させるスピード
    public float rotateSpeed = 1.0f;


    void Start()
    {
        
    }

    void Update()
    {
        if (gameManager.playerController == PlayerController.ReticleMode)
        {
            CameraRot();
        }
    }

    private void CameraRot()
    {
        //回転させる速度
        float rotH = Input.GetAxis("Horizontal") * rotateSpeed;
        //float rotV = Input.GetAxis("Vertical") * rotateSpeed;

        //回転させる
        var eularAngles = cameraShaft.transform.rotation.eulerAngles;
        var angle = eularAngles.y;
        if (angle > 180) angle -= 360;
        /*
        var eularAnglesV = cameraShaft.transform.rotation.eulerAngles;
        var angleV = eularAnglesV.x;
        if (angleV > 180) angleV -= 360;
        */
        //回転に制限をかける
        angle = Math.Clamp(angle + rotH, cameraRotLimit_Left, cameraRotLimit_Right);

        //angleV = Math.Clamp(angleV + rotV, cameraRotLimit_Bottom, cameraRotLimit_Top);
        
        //軸の角度を変える
        eularAngles.y = angle;

        //eularAnglesV.x = angleV;

        cameraShaft.transform.rotation = Quaternion.Euler(eularAngles);
        //moveCamera.transform.rotation = Quaternion.Euler(eularAnglesV);
    }
}
