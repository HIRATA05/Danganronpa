using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    float viewAngle;//エディター上で60を入力
    float _inputX, _inputY;

    void Update()
    {
        _inputX = Input.GetAxis("Horizontal");
        _inputY = Input.GetAxis("Vertical");

        Rotate(_inputX, -_inputY, viewAngle);
    }

    void Rotate(float _inputX, float _inputY, float limit)
    {
        //XYカメラ制限
        float maxLimit_X = limit, minLimit_X = 360 - maxLimit_X;
        float maxLimit_Y = limit, minLimit_Y = 360 - maxLimit_Y;

        //X軸回転
        var localAngle = transform.localEulerAngles;
        localAngle.x += _inputY;
        if (localAngle.x > maxLimit_X && localAngle.x < 180)
            localAngle.x = maxLimit_X;
        if (localAngle.x < minLimit_X && localAngle.x > 180)
            localAngle.x = minLimit_X;
        transform.localEulerAngles = localAngle;
        //Y軸回転
        var angle = transform.eulerAngles;
        angle.y += _inputX;
        if (localAngle.y > maxLimit_Y && localAngle.y < 180)
            localAngle.y = maxLimit_Y;
        if (localAngle.y < minLimit_Y && localAngle.y > 180)
            localAngle.y = minLimit_Y;
        transform.eulerAngles = angle;
    }
}
