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

    void Start()
    {
        playerController = PlayerController.ReticleMode;
    }

    void Update()
    {
        
    }
}
