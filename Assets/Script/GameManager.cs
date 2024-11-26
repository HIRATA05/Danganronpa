using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

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

    [SerializeField] private PostProcessVolume postProcessVolume;
    private DepthOfField depthOfField;


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

    //��ʂ��ڂ₯��������ʂ̐؂�ւ�
    public void SwitchDepthOfField(bool _switch)
    {
        //depthOfField���擾
        postProcessVolume.profile.TryGetSettings<DepthOfField>(out depthOfField);

        if (_switch)
        {
            depthOfField.active = true;
        }
        else
        {
            depthOfField.active = false;
        }
    }

    //���N�G�X�g�ɉ����ăe�L�X�g�E�C���h�E���J��
    public void OpenTextWindow(DialogueText dialogueText)
    {
        Debug.Log("�e�L�X�g�E�B���h�E���J��");

        //�\���𓧖�������
        reticleAim.aimImage.color = Color.clear;
        reticleAim.ColorChangeClaer();

        //�e�L�X�g�E�B���h�E��\��
        textWindow.dialogueText = dialogueText;
        playerController = GameManager.PlayerController.TextWindowMode;

        //��ʌ��ʔ���
        SwitchDepthOfField(true);
    }

    
}
