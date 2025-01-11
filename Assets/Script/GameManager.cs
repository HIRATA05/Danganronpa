using Cinemachine;
using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //�Q�[���S�̂��Ǘ�����

    //�v���C���[����̏��
    public enum PlayerController
    {
        ReticleMode,//�T��
        TextWindowMode,//��b
        DiscussionMode,//�c�_
        EventScene//�C�x���g�V�[��
    }
    [NonSerialized] public PlayerController playerController;

    private TextWindow textWindow;
    private ReticleAim reticleAim;
    private DiscussionManager disussionManager;
    private DiscussionTalkModeWindow discussionTalkModeWindow;

    public EventFlagData eventFlagData;


    [SerializeField] private PostProcessVolume postProcessVolume;
    private DepthOfField depthOfField;

    //��b�I�����c�_�J�n�̃t���O
    public bool isDiscussionStart = false;

    //�t�F�[�h�p���o�p�l��
    [SerializeField, Header("�t�F�[�h���o�p�摜")] private Image FadeEffect;
    //�t�F�[�h�����̃t���O
    bool fadeComp = false;

    //��ʐ؂�ւ��p
    [SerializeField] private GameObject AdventureScene;
    [SerializeField] private GameObject DiscussionScene;

    //��b�ɐݒ肵���C�x���g�̔����t���O
    [NonSerialized] public static bool isTalkEvent = false;
    //��b�Ƃ̊Ԃɑҋ@���Ԃ���������C�x���g�t���O
    [NonSerialized] public static bool isTalkPause = false;

    //�L�[�C���v�b�g
    private float keyElapsedTime = 0;
    private float keyWaitTime = 0.5f;
    //�X�y�[�X�L�[�̗L��
    private bool isSpaceKey = true;
    //�L�[�̌v�����
    private bool keyElapsed = false;


    void Start()
    {
        /*
        eventFlagData.GameStart = true;
        eventFlagData.GameStart_Window = false;
        eventFlagData.GameStart_Monitor = false;
        eventFlagData.GameStart_Lacky = false;
        eventFlagData.GameStart_All = false;
        eventFlagData.GameStart_All_TalkStart = false;
        eventFlagData.RoomIn = true;//�e�X�g�p
        eventFlagData.GameStart_ClassRoom_True();

        GameObject tw = GameObject.Find("TextWindow");
        textWindow = tw.GetComponent<TextWindow>();
        GameObject ra = GameObject.Find("CorsorImage");
        reticleAim = ra.GetComponent<ReticleAim>();
        */
        GameObject dm = GameObject.Find("DiscussionManager");
        disussionManager = dm.GetComponent<DiscussionManager>();
        GameObject dw = GameObject.Find("DiscussionTalkModeWindow");
        discussionTalkModeWindow = dw.GetComponent<DiscussionTalkModeWindow>();

        playerController = PlayerController.ReticleMode;
        DiscussionScene.SetActive(!DiscussionScene.activeSelf);
    }

    void Update()
    {
        //�L�[�C���v�b�g�̌v���J�n
        if (keyElapsed)
        {
            keyElapsedTime += Time.deltaTime;
            
            if (keyElapsedTime > keyWaitTime)
            {
                keyElapsedTime = 0;
                isSpaceKey = true;//�L�[��L��
                keyElapsed = false;
            }
        }
        
    }


    //ReticleMode�ɕύX
    public void ReticleModeChange()
    {
        playerController = PlayerController.ReticleMode;
        Debug.Log("�Ə�����T���Ɉڍs");
        SwitchScene();

        //�c�_�I����̉�b����
        OpenTextWindow(disussionManager.AfterText);
    }

    //TextWindowMode�ɕύX
    public void TextWindowModeChange()
    {
        playerController = PlayerController.TextWindowMode;
        Debug.Log("�e�L�X�g�E�B���h�E�Ɉڍs");
    }

    //DiscussionMode�ɕύX
    public async void DiscussionModeChange()
    {
        fadeComp = false;
        //�t�F�[�h�C��������
        StartCoroutine(FadeScreen(true));
        
        //�t�F�[�h�C�������܂őҋ@
        await UniTask.WaitUntil(() => fadeComp);

        playerController = PlayerController.DiscussionMode;
        //�V�[���؂�ւ�
        SwitchScene();
        //�t�F�[�h�A�E�g������
        StartCoroutine(FadeScreen(false));
        //�c�_�J�n���Ăяo��
        disussionManager.DiscInitCallOn();
    }

    //�T���V�[���Ƌc�_�V�[���̐؂�ւ�
    public void SwitchScene()
    {
        Debug.Log("�V�[���̐؂�ւ�");
        AdventureScene.SetActive(!AdventureScene.activeSelf);
        DiscussionScene.SetActive(!DiscussionScene.activeSelf);
    }

    //��ʂ��ڂ₯��������ʂ̐؂�ւ�
    public void SwitchDepthOfField(bool switchBleary)
    {
        //depthOfField���擾
        postProcessVolume.profile.TryGetSettings<DepthOfField>(out depthOfField);

        if (switchBleary)
        {
            depthOfField.active = true;
        }
        else
        {
            depthOfField.active = false;
        }
    }

    //�����ړ����ɌĂяo���A�T�����̃e�L�X�g�E�B���h�E�����̕����̃e�L�X�g�ɕς���
    public void RoomChange(TextWindow textData, DiscussionTalkModeWindow discussionTalkData)
    {
        textWindow = textData;
        discussionTalkModeWindow = discussionTalkData;
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
        playerController = PlayerController.TextWindowMode;

        //��ʌ��ʔ���
        SwitchDepthOfField(true);
    }

    //���N�G�X�g�ɉ����ċc�_�p�e�L�X�g�E�C���h�E���J��
    public void OpenDiscussionWindow(DialogueText dialogueText, DiscussionTalkModeWindow.TalkFinish talkFinishSet)
    {
        Debug.Log("�c�_�p�e�L�X�g�E�B���h�E���J��");

        //��b�I�����̈ڍs���ݒ�
        discussionTalkModeWindow.talkFinish = talkFinishSet;

        //�e�L�X�g�E�B���h�E��\��
        discussionTalkModeWindow.TextSet(dialogueText);
    }
    /*
    //�X�y�[�X�L�[�̓��͑ҋ@
    public bool KeyInputSpace()
    {
        //input = Input.GetKey(KeyCode.Space);

        //keyElapsedTime += Time.deltaTime;

        //�X�y�[�X�L�[�̓���
        if (Input.GetKey(KeyCode.Space) && isSpaceKey)
        {
            isSpaceKey = false;//�L�[�𖳌�
            keyElapsed = true;

            return true;
        }
        

        return false;
    }*/

    //��ʂɃt�F�[�h���o�𔭐�
    public IEnumerator FadeScreen(bool isFade)
    {
        Color color = FadeEffect.color;
        float duration = 1.0f;
        int FadeAlpha;
        //true�Ńt�F�[�h�C��
        if (isFade)
        {
            FadeAlpha = 1;
        }
        //false�Ńt�F�[�h�A�E�g
        else
        {
            FadeAlpha = 0;
        }
         
        //�t�F�[�h�C��������
        while (!Mathf.Approximately(color.a, FadeAlpha))
        {
            float changePerFrame = Time.deltaTime / duration;
            color.a = Mathf.MoveTowards(color.a, FadeAlpha, changePerFrame);
            FadeEffect.color = color;
            yield return null;
        }

        //�t�F�[�h�C���̎��t�F�[�h�����t���O�𗧂Ă�
        if (isFade)
        {
            fadeComp = true;
        }
        
    }
}
