using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //�Q�[���S�̂��Ǘ�����

    //�v���C���[����̏��
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
