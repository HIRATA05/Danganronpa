using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    //��]���邩
    private bool isRotate = false;

    //��]���x
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
        //Debug.Log("=�J������]��=");
        //Y���ɉ�]
        transform.RotateAround(transform.position, Vector3.up, rotateSpeed);
    }

    //��]�t���O�̃I���I�t
    public void RotateOn()
    {
        isRotate = true;
    }
    public void RotateOff()
    {
        isRotate = false;
    }
}
