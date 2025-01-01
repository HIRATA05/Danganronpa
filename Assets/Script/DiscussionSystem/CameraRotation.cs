using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    //‰ñ“]‚·‚é‚©
    private bool isRotate = false;

    //‰ñ“]‘¬“x
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
        //Debug.Log("=ƒJƒƒ‰‰ñ“]’†=");
        //Y²‚É‰ñ“]
        transform.RotateAround(transform.position, Vector3.up, rotateSpeed);
    }

    //‰ñ“]ƒtƒ‰ƒO‚ÌƒIƒ“ƒIƒt
    public void RotateOn()
    {
        isRotate = true;
    }
    public void RotateOff()
    {
        isRotate = false;
    }
}
