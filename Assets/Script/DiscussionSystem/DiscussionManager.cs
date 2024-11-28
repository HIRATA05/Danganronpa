using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class DiscussionManager : MonoBehaviour
{
    //�m���X�g�b�v�c�_�̑S�̓I�ȓ�����Ǘ�

    //�Q�[���}�l�[�W���[
    [SerializeField] private GameManager gameManager;

    //�c�_���̃e�L�X�g�E�B���h�E
    [SerializeField] private DiscussionTalkModeWindow discussionTalkModeWindow;

    //�~�`�̕��т����X�N���v�g
    [SerializeField] private CircleDeployer circleDeployer;

    public enum DiscussionMode
    {
        Talk,//��b
        Shooting//�m���X�g�b�v�c�_
    }
    [NonSerialized] public DiscussionMode discussion;

    //�c�_�ꏊ�̒��S�_
    [SerializeField] private GameObject centerDiscussionPoint;

    //�J������]���x
    float rotSpeed = 1.0f;

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
    //�_�j�J���[�R�[�h#ffa500�@���ӃJ���[�R�[�h#41A2E1
    //�Z���t
    [System.Serializable]
    public class SpeechSet
    {
        //����
        public string Speech;

        //�����҂̖��O
        public string SpeechName;

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

        //�����F�ω��͈̔�
        public int WeekRangeStart = 0;
        public int WeekRangeEnd = 1;
    }
    [Header("�_�j�J���[�R�[�h#ffa500�@���ӃJ���[�R�[�h#41A2E1")]
    public SpeechSet[] speechSet;

    //�����̓����蔻��̌���
    private float textThickZ = 0.01f;

    //�c�_�̐i�s�ԍ�
    private int DiscussionNum = 0;

    //�c�_�J�n�̍ۂ̎���
    private float elapsedTime = 0.0f;
    private float startTime = 5.0f;

    //�~�`�ɕ��Ԑ��k
    [SerializeField, Header("�c�_�ŕ��ԃL����")] private GameObject[] DiscussionMenber;
    //���k�����̐e�I�u�W�F�N�g
    [SerializeField, Header("�c�_�Ҕ����̐e�I�u�W�F�N�g")] private GameObject perentObj;

    private bool isDiscussionInitCalled = false;
    private bool isTextSetCalled = false;

    //�c�_�����ǂ���
    [NonSerialized] public bool discussionProgress = false;

    void Start()
    {
        
    }

    void Update()
    {
        if (gameManager.playerController == GameManager.PlayerController.DiscussionMode)
        {
            //�c�_����������
            DiscussionInit();

            DiscussionPlay();
        }
    }

    private void DiscussionPlay()
    {
        //��b
        if (discussion == DiscussionMode.Talk)
        {
            //��b����UI�\��

        }
        //�c�_
        else if (discussion == DiscussionMode.Shooting && discussionProgress)
        {
            //�c�_����UI�\��

            //��莞�Ԃ��Ƃɕ�����ω�
            if (!isTextSetCalled)
            {
                isTextSetCalled = true;

                //�����̃f�[�^���Z�b�g����
                speechText.GetComponent<TextMeshProUGUI>().text = speechSet[DiscussionNum].Speech;

                //������ speechText.GetComponent<TextMeshProUGUI>().text.Length
                //SpeechType.None�̏ꍇ�E�B�[�N�|�C���g�͍��Ȃ�

                //�����̕��������當���̓����蔻���ݒ肷��
                /*
                speechText.AddComponent<BoxCollider>();
                speechText.GetComponent<BoxCollider>().center = new Vector3(0, 0, 0);
                speechText.GetComponent<BoxCollider>().size = new Vector3(0, 0, textThickZ);
                */
                //�����\����c�_�ԍ����Z
                DiscussionNum++;
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

    //�c�_�t�F�C�Y�̏�����
    public void DiscussionInit()
    {
        if (!isDiscussionInitCalled)
        {
            isDiscussionInitCalled = true;

            //�c�_�ԍ�������
            DiscussionNum = 0;

            //DiscussionMenber�����Ԃɐ���
            for (int i = 0; i < DiscussionMenber.Length; i++)
            {
                Instantiate(DiscussionMenber[i], perentObj.transform);
            }
            //���k�̕��т��~�`�ɕ��ׂ�
            circleDeployer.Deploy();

            //�c�_�J�n���̉�b�f�[�^������
            gameManager.OpenDiscussionWindow(DiscussionStartText);
            //�m���X�g�b�v�c�_�̊J�n
            discussion = DiscussionMode.Talk;
        }
    }

    //�c�_���J�n���鏈��
    public void ShootingInit()
    {
        discussion = DiscussionMode.Shooting;

        while (!discussionProgress)
        {
            elapsedTime += Time.deltaTime;

            //��莞�ԃJ��������

            //�c�_�J�n�̕������o


            if (elapsedTime > startTime)
            {
                Debug.Log("=�c�_���J�n=");
                elapsedTime = 0;

                //��L�I����c�_�J�n
                //discussion = DiscussionMode.Shooting;
                discussionProgress = true;
            }
        }
        
    }

    //��b�̔�����c�_���ꏄ������
    public void TakeAround()
    {
        //�c�_�ꏄ���̉�b�f�[�^������
        gameManager.OpenDiscussionWindow(DiscussionTakeAroundText);
        //��b�J�n
        discussion = DiscussionMode.Talk;
        //�c�_�ԍ������Z�b�g
        DiscussionNum = 0;
    }

    //�c�_���~���Ę_�j�摜��\�����鏈��
    public void ShootingFinish()
    {
        Debug.Log("�c�_�I���ŉ�b�ڍs");
        //�v���C���[�̋c�_������I��
        discussionProgress = false;

        //��ʂ̊���鉉�o

        //�_�j���o�摜�\��

        //�c�_�I����̉�b�f�[�^������
        gameManager.OpenDiscussionWindow(DiscussionFinishText);
        //��b�J�n
        discussion = DiscussionMode.Talk;
    }
}