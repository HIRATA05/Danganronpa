using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DiscussionManager : MonoBehaviour
{
    //�m���X�g�b�v�c�_�̑S�̓I�ȓ�����Ǘ�

    //�Q�[���}�l�[�W���[
    [SerializeField] private GameManager gameManager;

    //�~�`�̕��т����X�N���v�g
    [SerializeField] private CircleDeployer circleDeployer;

    enum DiscussionMode
    {
        Talk,
        Shooting
    }
    DiscussionMode discussion;


    //�Ə�
    [SerializeField] private Image aimImage;
    //�Ə��̉摜
    [SerializeField] private Sprite aimImageNormal;
    [SerializeField] private Sprite aimImageColText;

    //�c�_�ꏊ�̒��S�_
    [SerializeField] private GameObject centerDiscussionPoint;

    //��]���x
    float rotSpeed = 1.0f;

    //���̔����܂ł̎���
    float nextSpeechTime = 5.0f;
    //���ݎ���
    float currentTime = 0.0f;

    //�R�g�_�}�V�����_�[


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

        //�����F�ω��͈̔�
        public int WeekRangeStart = 0;
        public int WeekRangeEnd = 1;
    }
    [Header("�_�j�J���[�R�[�h#ffa500�@���ӃJ���[�R�[�h#41A2E1")]
    public SpeechSet[] speechSet;

    private float textThickZ = 0.01f;

    //�c�_�̐i�s�ԍ�
    private int DiscussionNum = 0;

    //�~�`�ɕ��Ԑ��k
    [SerializeField, Header("�c�_�ŕ��ԃL����")] private GameObject[] DiscussionMenber;
    //���k�����̐e�I�u�W�F�N�g
    [SerializeField, Header("�c�_�Ҕ����̐e�I�u�W�F�N�g")] private GameObject perentObj;

    private bool isTextSetCalled = false;

    void Start()
    {
        //�c�_����������
        DiscussionInit();


    }

    void Update()
    {
        if (!isTextSetCalled)
        {
            isTextSetCalled = true;
            //�����̃f�[�^���Z�b�g����
            speechText.GetComponent<TextMeshProUGUI>().text = speechSet[DiscussionNum].Speech;

            //������ speechText.GetComponent<TextMeshProUGUI>().text.Length

            //�����̓����蔻���ݒ肷��
            speechText.AddComponent<BoxCollider>();
            speechText.GetComponent<BoxCollider>().center = new Vector3(0, 0, 0);
            speechText.GetComponent<BoxCollider>().size = new Vector3(0, 0, textThickZ);
        }
        

        //���Ԍo�߂Ŏ��̕����ɐi��
        currentTime += Time.deltaTime;
        if (currentTime > nextSpeechTime)
        {
            currentTime = 0;

            if (DiscussionNum < speechSet.Length)
            {
                DiscussionNum++;
                //�����̓����蔻����폜����
                Destroy(speechText.GetComponent<BoxCollider>());
                isTextSetCalled = false;
            }
            
        }
        
    }

    //�����̈ړ�
    public void SpeechMove(SpeechSet speech)
    {
        
    }

    //�c�_�t�F�C�Y�̏�����
    public void DiscussionInit()
    {
        //�c�_�ԍ�������
        DiscussionNum = 0;

        //DiscussionMenber�����Ԃɐ���
        for (int i = 0; i < DiscussionMenber.Length; i++) {
            Instantiate(DiscussionMenber[i], perentObj.transform);
        }
        //���k�̕��т��~�`�ɕ��ׂ�
        circleDeployer.Deploy();
    }
}
