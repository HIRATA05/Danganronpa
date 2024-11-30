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
        int defaultLayer;
        int mapLayer;

        private void Start()
        {
            defaultLayer = 1 << LayerMask.NameToLayer("Default");
            mapLayer = 1 << LayerMask.NameToLayer("UI");
            Camera.main.cullingMask = defaultLayer;
        }

        void Update()
        {
            // マップを開く
            if (Input.GetKeyDown(KeyCode.M))
            {
                isViewMap = (isViewMap == true) ? false : true;
            }

            // マップ切り替え
            if (isViewMap)
            {
                Camera.main.cullingMask = mapLayer;
            }
            else
            {
                Camera.main.cullingMask = defaultLayer;
                isViewMap = false;
            }
        }
    }
}