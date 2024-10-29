using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //ゲーム全体を管理する

    //プレイヤー操作の状態
    public enum PlayerController
    {
        ReticleMode,
        TextWindowMode,
        Logic,
    }
    [NonSerialized] public PlayerController playerController;

    private TextWindow textWindow;


    //カメラを表示
    public int cam1;

    void Start()
    {
        GameObject tw = GameObject.Find("TextWindow");
        textWindow = tw.GetComponent<TextWindow>();

        playerController = PlayerController.ReticleMode;
    }

    void Update()
    {
        
    }

    /// <summary>
    /// リクエストに応えてテキストウインドウを開く
    /// </summary>
    public void OpenTextWindow(DialogueText dialogueText, CinemachineVirtualCamera[] virtualCameras)
    {
        Debug.Log("テキストウィンドウを開く");

        textWindow.dialogueText = dialogueText;
        playerController = GameManager.PlayerController.TextWindowMode;

        virtualCameras[0].Priority = 1;
        virtualCameras[1].Priority = 0;
        virtualCameras[2].Priority = 0;


        for (int i = 0; i < virtualCameras.Length; i++) { Debug.Log(virtualCameras[i].name); }
    }
}
