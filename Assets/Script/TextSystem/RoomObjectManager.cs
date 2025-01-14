using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class RoomObjectManager : MonoBehaviour
{
    [Header("部屋の中の調べられるキャラや物を設定")]
    //調べられる対象物
    [SerializeField] private GameObject[] roomObject;

    private const int priorityOn = 1;
    private const int priorityOff = 0;


    void Start()
    {
        
    }

    void Update()
    {
        
    }

    //オブジェクトを比較してプライオリティを変化させる
    public void RoomObjectPriorityChange(GameObject CameraObject, int CameraNum)
    {
        for (int i = 0; i < roomObject.Length; i++)
        {
            //nullである場合はプライオリティをゼロ
            if (CameraObject == null || !CameraObject.activeSelf)
            {
                CameraNumberCheck(roomObject[i], CameraNum, priorityOff);

            }
            //nullでない場合
            else
            {
                //オブジェクトが一致した場合プライオリティを1
                if (roomObject[i] == CameraObject)
                {
                    CameraNumberCheck(roomObject[i], CameraNum, priorityOn);
                }
                //オブジェクトが一致しなかった場合プライオリティをゼロ
                else
                {
                    CameraNumberCheck(roomObject[i], CameraNum, priorityOff);
                }
            }   
        }
    }

    //CameraNum番目の子オブジェクトからヴァーチャルカメラを取得
    private void CameraNumberCheck(GameObject CameraObject, int CameraNum, int prioritySet)
    {
        PriorityChange(CameraObject.transform.GetChild(CameraNum).gameObject.GetComponent<CinemachineVirtualCamera>(), prioritySet);   
    }
    //プライオリティを変化させる
    private void PriorityChange(CinemachineVirtualCamera Camera, int prioritySet)
    {
        Camera.Priority = prioritySet;
    }
}
