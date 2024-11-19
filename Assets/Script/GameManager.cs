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


    //���N�G�X�g�ɉ����ăe�L�X�g�E�C���h�E���J��
    public void OpenTextWindow(DialogueText dialogueText)
    {
        Debug.Log("�e�L�X�g�E�B���h�E���J��");

        //�\���𓧖�������
        reticleAim.aimImage.color = Color.clear;
        reticleAim.ColorChangeClaer();

        textWindow.dialogueText = dialogueText;
        playerController = GameManager.PlayerController.TextWindowMode;
        
    }

    
}
