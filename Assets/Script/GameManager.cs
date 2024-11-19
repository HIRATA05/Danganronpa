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
    private ReticleAim reticleAim;

    public EventFlagData eventFlagData;



    void Start()
    {
        eventFlagData.RoomIn = true;

        GameObject tw = GameObject.Find("TextWindow");
        textWindow = tw.GetComponent<TextWindow>();
        GameObject ra = GameObject.Find("CorsorImage");
        reticleAim = ra.GetComponent<ReticleAim>();

        playerController = PlayerController.ReticleMode;
    }

    void Update()
    {
        
    }


    //リクエストに応えてテキストウインドウを開く
    public void OpenTextWindow(DialogueText dialogueText)
    {
        Debug.Log("テキストウィンドウを開く");

        //表示を透明化する
        reticleAim.aimImage.color = Color.clear;
        reticleAim.ColorChangeClaer();

        textWindow.dialogueText = dialogueText;
        playerController = GameManager.PlayerController.TextWindowMode;
        
    }

    
}
