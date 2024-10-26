using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    //ƒJƒƒ‰
    [SerializeField] private GameObject moveCamera;

    //‰ñ“]‚·‚é‚Ì‰ñ“]²
    [SerializeField] private GameObject cameraShaft;

    //ƒJƒƒ‰ˆÚ“®”ÍˆÍ
    [SerializeField] private float cameraRotLimit_Right = 30.0f;
    [SerializeField] private float cameraRotLimit_Left = -30.0f;

    //‰ñ“]‚³‚¹‚éƒXƒs[ƒh
    public float rotateSpeed = 1.0f;


    void Start()
    {
        
    }

    void Update()
    {
        //‰ñ“]‚³‚¹‚é‘¬“x
        float rot = Input.GetAxis("Horizontal") * rotateSpeed;

        //‰ñ“]‚³‚¹‚é
        var eularAngles = cameraShaft.transform.rotation.eulerAngles;
        var angle = eularAngles.y;
        if (angle > 180) angle -= 360;

        //‰ñ“]‚É§ŒÀ‚ğ‚©‚¯‚é
        angle = Math.Clamp(angle + rot, cameraRotLimit_Left, cameraRotLimit_Right);

        //²‚ÌŠp“x‚ğ•Ï‚¦‚é
        eularAngles.y = angle;
        cameraShaft.transform.rotation = Quaternion.Euler(eularAngles);


    }
}
