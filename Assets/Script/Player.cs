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
            // �}�b�v���J��
            isViewMap = true;
        }

        if (isViewMap)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                // enum �^�ŕ��������w��
                roomController.MoveRoom(RoomNames.LivingRoom, transform);
            }
        }
    }
}
