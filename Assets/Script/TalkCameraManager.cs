using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkCameraManager : MonoBehaviour
{
    //RoomManagerからオブジェクトを取得
    //テキストの配列と同じ数だけカメラの注視先とカメラ分割設定を決める

    [System.Serializable] public class CameraSet
    {
        //カメラ分割の種類
        public enum CameraDivision
        {
            CenterOnly,//中央のみ
            CenterAndRight,//中央と右
            CenterAndLeft,//中央と左
        }

        //カメラで表示するオブジェクト
        public GameObject cameraLookObjectCenter;//中央
        public GameObject cameraLookObjectRight;//右
        public GameObject cameraLookObjectLeft;//左

        //カメラ分割の形
        public CameraDivision camDivision;
    }

    [System.Serializable] public class TalkSet
    {
        //会話番号　Scriptableテキストと同期するための番号
        public int number;

        //会話の数だけ作成する
        public CameraSet[] cameraSet;
    }

    [Header("その部屋の会話イベントの数だけ設定")]
    public TalkSet[] talkSet;



    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
