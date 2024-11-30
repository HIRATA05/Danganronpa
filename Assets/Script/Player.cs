using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TECHC.Kamiyashiki
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private RoomController roomController;

        bool isViewMap = false;

        private void Start()
        {

        }

        void Update()
        {
            // マップを開く
            if (Input.GetKeyDown(KeyCode.M))
            {
                isViewMap = (isViewMap == true) ? false : true;
                roomController.OpenMapCanvas(isViewMap);
            }
        }
    }
}