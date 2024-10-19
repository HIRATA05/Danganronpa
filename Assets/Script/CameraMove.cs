using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private GameObject moveCamera;

    
    void Start()
    {
        
    }

    void Update()
    {
        //入力でカメラ角度変更
        if (Input.GetKey(KeyCode.RightArrow))
        {
            Debug.Log("RightArrow");
            //moveCamera.
            //pos.x += 1;
        }
        //「←」入力の場合は変数「pos」のx軸における座標を毎フレーム毎に１だけ減少
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Debug.Log("RightArrow");
            // pos.x -= 1;
        }
    }
}
