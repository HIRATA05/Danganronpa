using Cinemachine;
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

    private TextWindow textWindow;


    //�J������\��
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
    /// ���N�G�X�g�ɉ����ăe�L�X�g�E�C���h�E���J��
    /// </summary>
    public void OpenTextWindow(DialogueText dialogueText, CinemachineVirtualCamera[] virtualCameras)
    {
        Debug.Log("�e�L�X�g�E�B���h�E���J��");

        textWindow.dialogueText = dialogueText;
        playerController = GameManager.PlayerController.TextWindowMode;

        virtualCameras[0].Priority = 1;
        virtualCameras[1].Priority = 0;
        virtualCameras[2].Priority = 0;


        for (int i = 0; i < virtualCameras.Length; i++) { Debug.Log(virtualCameras[i].name); }
    }
}
