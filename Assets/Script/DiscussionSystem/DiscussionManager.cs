using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.UIElements;
using Cinemachine;
using Unity.VisualScripting;

public class DiscussionManager : MonoBehaviour
{
    //�m���X�g�b�v�c�_�̑S�̓I�ȓ�����Ǘ�

    //�Q�[���}�l�[�W���[
    [SerializeField] private GameManager gameManager;

    //�c�_���̃e�L�X�g�E�B���h�E
    [SerializeField] private DiscussionTalkModeWindow discussionTalkModeWindow;

    //�c�_����UI
    [SerializeField] private DiscussionUI discussionUI;

    //�~�`�̕��т����X�N���v�g
    [SerializeField] private CircleDeployer circleDeployer;

    public enum DiscussionMode
    {
        Talk,//��b
        BeforeDiscussion,//�c�_�O���o
        Shooting//�m���X�g�b�v�c�_
    }
    [NonSerialized] public DiscussionMode discussion;

    //�c�_�ꏊ�̒��S�_
    [SerializeField] private GameObject centerDiscussionPoint;

    //�c�_�J����
    [SerializeField] private Camera DiscussionCamera;
    [SerializeField] private GameObject MainVirtualCamera;
    private CinemachineVirtualCamera MainCCVirtualCamera;

    //���̔����܂ł̎���
    float nextSpeechTime = 5.0f;
    //���ݎ���
    float currentTime = 0.0f;

    //�R�g�_�}�V�����_�[�@�R�g�_�}�̃N���X����ō��
    //[SerializeField] private �R�g�_�}�N���X[] cylinder;

    //�c�_�J�n����b
    [SerializeField] private DialogueText DiscussionStartText;
    //�c�_�ꏄ����b
    [SerializeField] private DialogueText DiscussionTakeAroundText;
    //�c�_�I������b
    [SerializeField] private DialogueText DiscussionFinishText;
    //�_�j���s����b
    [SerializeField] private DialogueText DiscussionFailureText;

    //�\������e�L�X�g
    [SerializeField] private GameObject speechText;
    
    //�Z���t
    [System.Serializable]
    public class SpeechSet
    {
        //�����҂̖��O
        public string SpeechName;

        //����
        public string NormalSpeechBefore;
        public string WeekPointSpeech;
        public string NormalSpeechAfter;

        //�_�j�����ӂ�
        public enum SpeechType
        {
            refute,
            consent,
            None
        }
        public SpeechType speechType = SpeechType.None;

        //�����̈ړ��p�^�[��
        public enum SpeechMovePattern
        {
            RightToLeft,//�E���獶
            LeftToRight,//������E
        }
        public SpeechMovePattern speechMove;

        //�R�g�_�}���ԈႦ�����̉�b
        public DialogueText DiscussionMistakeText;

    }
    [Header("�_�j�J���[�R�[�h#ffa500�@���ӃJ���[�R�[�h#41A2E1")]
    public SpeechSet[] speechSet;

    private string CurrentWeekPoint;
    //[NonSerialized] public TextMeshProUGUI SpeechAll;

    //�c�_�̐i�s�ԍ�
    private int DiscussionNum = 0;

    //�c�_�J�n�̍ۂ̎���
    private float elapsedTime = 0.0f;
    private float startTime = 5.0f;

    //�~�`�ɕ��Ԑ��k
    [SerializeField, Header("�c�_�ŕ��ԃL����")] private GameObject[] DiscussionMenber;
    //���k�����̐e�I�u�W�F�N�g
    [SerializeField, Header("�c�_�Ҕ����̐e�I�u�W�F�N�g")] private GameObject perentObj;
    //�����������k�̔z��
    private GameObject[] InstantiateMenber = new GameObject[0];

    private bool isDiscussionInitCalled = false;
    private bool isTextSetCalled = false;

    //�c�_�����ǂ���
    [NonSerialized] public bool discussionProgress = false;

    void Start()
    {
        MainCCVirtualCamera = MainVirtualCamera.GetComponent<CinemachineVirtualCamera>();
    }

    void Update()
    {
        if (gameManager.playerController == GameManager.PlayerController.DiscussionMode)
        {
            //�c�_����������
            DiscussionInit();
            //�c�_�̐i�s
            DiscussionPlay();
        }
    }
    
    //�c�_�t�F�C�Y�̏�����
    public void DiscussionInit()
    {
        if (!isDiscussionInitCalled)
        {
            isDiscussionInitCalled = true;

            //�c�_�ԍ�������
            DiscussionNum = 0;

            InstantiateMenber = new GameObject[DiscussionMenber.Length];
            //DiscussionMenber�����Ԃɐ���
            for (int i = 0; i < DiscussionMenber.Length; i++)
            {
                InstantiateMenber[i] = Instantiate(DiscussionMenber[i], perentObj.transform);
            }

            //���k�̕��т��~�`�ɕ��ׂ�
            circleDeployer.Deploy();

            //�c�_�J�n���̉�b�f�[�^������
            gameManager.OpenDiscussionWindow(DiscussionStartText);
            //�m���X�g�b�v�c�_�̊J�n
            discussion = DiscussionMode.Talk;
        }

    }

    private void DiscussionPlay()
    {
        //��b
        if (discussion == DiscussionMode.Talk)
        {
            //��b����UI�\��

        }
        //�c�_�J�n�O�̉��o
        else if(discussion == DiscussionMode.BeforeDiscussion)
        {
            //Debug.Log("�c�_�J�n�o�ߎ��ԁF" + elapsedTime);

            //�c�_�J�n�̕������o


            if (elapsedTime > startTime)
            {
                Debug.Log("=�c�_���J�n=");
                elapsedTime = 0;

                //�J�������~
                //DiscussionCamera.GetComponent<CameraRotation>().RotateOff();
                MainVirtualCamera.GetComponent<CameraRotation>().RotateOff();

                //�J��������l���Ɍ�����
                CameraPriority(0);

                //�ő唭���ԍ���ݒ�
                discussionUI.SpeechNumMaxSet(speechSet.Length);

                //��L�I����c�_�J�n
                discussion = DiscussionMode.Shooting;
                discussionProgress = true;
            }

            //���Ԍo��
            elapsedTime += Time.deltaTime;

        }
        //�c�_
        else if (discussion == DiscussionMode.Shooting && discussionProgress)
        {
            //�c�_����UI�\��

            //��莞�Ԃ��Ƃɕ�����ω�
            if (!isTextSetCalled)
            {
                isTextSetCalled = true;

                //���݂̔������擾
                //CurrentSpeech = speechSet[DiscussionNum];
                CurrentWeekPoint = speechSet[DiscussionNum].WeekPointSpeech;
                //�����̑傫���ƐF���Z�b�g
                speechSet[DiscussionNum].NormalSpeechBefore = "<size=60><color=white>" + speechSet[DiscussionNum].NormalSpeechBefore;

                if(speechSet[DiscussionNum].speechType == SpeechSet.SpeechType.refute)
                    speechSet[DiscussionNum].WeekPointSpeech = "<size=65><color=#ffa500>" + speechSet[DiscussionNum].WeekPointSpeech;
                else if(speechSet[DiscussionNum].speechType == SpeechSet.SpeechType.consent)
                    speechSet[DiscussionNum].WeekPointSpeech = "<size=65><color=#41A2E1>" + speechSet[DiscussionNum].WeekPointSpeech;
                else
                    speechSet[DiscussionNum].WeekPointSpeech = "<size=60><color=white>" + speechSet[DiscussionNum].WeekPointSpeech;

                speechSet[DiscussionNum].NormalSpeechAfter = "<size=60><color=white>" + speechSet[DiscussionNum].NormalSpeechAfter;
                //�S�Ă̕��������킹��1�̕���������
                string speech = speechSet[DiscussionNum].NormalSpeechBefore + " " + speechSet[DiscussionNum].WeekPointSpeech + " " + speechSet[DiscussionNum].NormalSpeechAfter;
                Debug.Log(speech);
                //�����̃f�[�^���Z�b�g����
                speechText.GetComponent<TextMeshProUGUI>().text = speech;

                //SpeechAll = speechText.GetComponent<TextMeshProUGUI>();

                //speechText.GetComponent<TextMeshPro>().text = speechSet[DiscussionNum].Speech;

                //�������擾 speechText.GetComponent<TextMeshProUGUI>().text.Length
                //SpeechType.None�̏ꍇ�E�B�[�N�|�C���g�͍��Ȃ�

                //���݂̔����҂��擾
                for(int i = 0; i < InstantiateMenber.Length; i++)
                {
                    if(speechSet[DiscussionNum].SpeechName == InstantiateMenber[i].GetComponent<SpeechName>().speechName)
                    {
                        //�J�����𔭌��҂Ɍ�����
                        CameraPriority(i);
                    }
                }

                //�����\����c�_�ԍ����Z
                DiscussionNum++;

                //�����ԍ��Ɣ����҂�ݒ�
                discussionUI.SpeechNumSet(DiscussionNum, speechSet[DiscussionNum].SpeechName);
            }

            //���Ԍo�߂Ŏ��̕����ɐi��
            currentTime += Time.deltaTime;
            if (currentTime > nextSpeechTime)
            {
                currentTime = 0;

                if (DiscussionNum < speechSet.Length)
                {

                    //�����̓����蔻����폜����
                    //Destroy(speechText.GetComponent<BoxCollider>());

                    isTextSetCalled = false;
                    Debug.Log(DiscussionNum + ":" + speechSet.Length);
                }
                else
                {
                    //�ꏄ������
                    TakeAround();
                }

            }
        }
    }

    //�����̈ړ�
    public void SpeechMove(SpeechSet speech)
    {
        //�ړ��p�^�[���ɂ���Ĕ����̔����ʒu�⓮�����ω�
        //�J�����̓������ω�

    }

    //���@�[�`�����J�����̗D��x��ω����ăJ������ω�
    private void CameraPriority(int charaNum)
    {
        //�c�_�����o�[�{���C���J����
        for (int i = 0; i < InstantiateMenber.Length + 1; i++)
        {
            if(i == InstantiateMenber.Length + 1)
            {
                //Debug.Log("i == InstantiateMenber.Length + 1");
                MainCCVirtualCamera.Priority = 1;
            }
            else if(i == charaNum)
            {
                //Debug.Log("i == charaNum");
                Debug.Log(InstantiateMenber[i].name);
                InstantiateMenber[i].transform.GetChild(0).gameObject.GetComponent<CinemachineVirtualCamera>().Priority = 1;
            }
            else
            {
                //Debug.Log("else");
                if(i < InstantiateMenber.Length)
                {
                    InstantiateMenber[i].transform.GetChild(0).gameObject.GetComponent<CinemachineVirtualCamera>().Priority = 0;
                }
                
                MainCCVirtualCamera.Priority = 0;
            }
        }
    }

    //�c�_���J�n���鏈��
    public void ShootingInit()
    {
        //�c�_�O�̉��o�̔����Ɉڍs
        discussion = DiscussionMode.BeforeDiscussion;

        //��莞�ԃJ��������
        //DiscussionCamera.GetComponent<CameraRotation>().RotateOn();
        MainVirtualCamera.GetComponent<CameraRotation>().RotateOn();
    }

    //��b�̔�����c�_���ꏄ������
    public void TakeAround()
    {
        //�c�_�ꏄ���̉�b�f�[�^������
        gameManager.OpenDiscussionWindow(DiscussionTakeAroundText);
        //��b�J�n
        discussion = DiscussionMode.Talk;

        DiscussionEnd();
        //�c�_�ԍ������Z�b�g
        DiscussionNum = 0;
    }

    //�c�_�Ŕ����������̂����Z�b�g
    public void DiscussionEnd()
    {
        //������������
        speechText.GetComponent<TextMeshProUGUI>().text = "";
        isTextSetCalled = false;
        //�c�_�̐��k������
        for (int i = 0; i < InstantiateMenber.Length; i++)
        {
            Destroy(InstantiateMenber[i]);
        }
        //�J�����̌��������Z�b�g
        MainVirtualCamera.transform.rotation = Quaternion.identity;
    }

    //�c�_���~���Ę_�j�摜��\�����鏈��
    public void ShootingFinish()
    {
        Debug.Log("�c�_�I���ŉ�b�ڍs");

        DiscussionEnd();//��ŉ�b�I�����ɔ�������悤�ɕς���
        //���������������̃t���O��߂�
        //isDiscussionInitCalled = false;

        //�v���C���[�̋c�_������I��
        discussionProgress = false;

        //�_�j���o�摜�\��

        //��ʂ̊���鉉�o

        //�c�_�I����̉�b�f�[�^������
        gameManager.OpenDiscussionWindow(DiscussionFinishText);
        //��b�J�n
        discussion = DiscussionMode.Talk;

    }

    //�c�_�t�F�C�Y�̏������𔭐�������t���O
    public void DiscInitCallOn()
    {
        isDiscussionInitCalled = false;
    }

    //�����ɓ������������肵�Đ������E�B�[�N�|�C���g���m�F
    public bool TextColWeek()
    {
        var index = TMP_TextUtilities.FindIntersectingWord(speechText.GetComponent<TextMeshProUGUI>(), Input.mousePosition, Camera.main);
        if (index < 0)
            return false;

        var wordInfo = speechText.GetComponent<TextMeshProUGUI>().textInfo.wordInfo[index];

        Debug.Log(wordInfo.GetWord() + " "+ CurrentWeekPoint/*CurrentSpeech.WeekPointSpeech*/);

        //�擾�����������E�B�[�N�|�C���g�Ɠ���������
        if(wordInfo.GetWord() == CurrentWeekPoint/*CurrentSpeech.WeekPointSpeech*/)
        {
            return true;
        }
        return false;
    }

    //�E�B�[�N�|�C���g���_�j�����ӂ�����

}