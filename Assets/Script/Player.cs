using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private RoomController roomController;

    bool isViewMap = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            // マップを開く
            isViewMap = true;
        }

        if (isViewMap)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                // enum 型で部屋名を指定
                roomController.MoveRoom(RoomNames.LivingRoom, transform);
            }
        }
    }
}
