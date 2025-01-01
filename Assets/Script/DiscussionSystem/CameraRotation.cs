using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    //回転するか
    private bool isRotate = false;

    //回転速度
    public float rotateSpeed = 0.5f;

    void Start()
    {
        
    }

    void Update()
    {
        if(isRotate) CameraRotate();
    }

    private void CameraRotate()
    {
        //Debug.Log("=カメラ回転中=");
        //Y軸に回転
        transform.RotateAround(transform.position, Vector3.up, rotateSpeed);
    }

    //回転フラグのオンオフ
    public void RotateOn()
    {
        isRotate = true;
    }
    public void RotateOff()
    {
        isRotate = false;
    }
}
