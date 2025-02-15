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
            All,//3つ全て
        }

        //カメラで表示するオブジェクト
        public GameObject camLookCenter;//中央
        public GameObject camLookRight;//右
        public GameObject camLookLeft;//左

        //カメラ分割の形
        public CameraDivision camDivision;

        //会話時のイベント
        public UnityEngine.Events.UnityEvent OnTalkEvent;
    }

    [System.Serializable] public class TalkSet
    {
        //会話番号　Scriptableテキストと同期するためのキー
        public string textinfo;

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
