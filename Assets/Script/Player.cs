using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private RoomController roomController;

    bool isViewMap = false;
    int mapLayer;
    int defaultLayer;

    private void Start()
    {
        defaultLayer = 1 << LayerMask.NameToLayer("Default");
        mapLayer = 1 << LayerMask.NameToLayer("UI");
        Camera.main.cullingMask = defaultLayer;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            // マップを開く
            isViewMap = (isViewMap == true) ? false : true;
        }

        if (isViewMap)
        {
            Camera.main.cullingMask = mapLayer;

            float DebugDrawRayDistance = 15.0f;
            if (Input.GetMouseButtonDown(0))
            {
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out var hit))
                {
                    string objectName = hit.collider.gameObject.name;
                    Debug.Log("左クリック:" + objectName);

                    if (hit.collider.transform.name == RoomNames.Kitchen.ToString())
                    {
                        roomController.MoveRoom(RoomNames.Kitchen, transform);
                        isViewMap = false;
                    }
                }
                Debug.DrawRay(ray.origin, ray.direction * DebugDrawRayDistance, Color.green, 5, false);
            }
        }
        else
        {
            Camera.main.cullingMask = defaultLayer;
            isViewMap = false;
        }
    }
}
