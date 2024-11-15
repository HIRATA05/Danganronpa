using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.Experimental.GraphView;

public class DiscussionManager : MonoBehaviour
{
    //�m���X�g�b�v�c�_�̑S�̓I�ȓ�����Ǘ�

    //�~�`�̕��т����X�N���v�g
    [SerializeField] private CircleDeployer circleDeployer;

    //�c�_�ꏊ�̒��S�_
    [SerializeField] private GameObject centerDiscussionPoint;

    //��]���x
    float rotSpeed = 1.0f;

    //�Ə�


    //�R�g�_�}�V�����_�[


    //�\������e�L�X�g
    [SerializeField] private TextMeshProUGUI Text;

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
    public SpeechSet[] speechSet;

    //�c�_�̐i�s�ԍ�
    private int DiscussionNum = 0;

    //�~�`�ɕ��Ԑ��k
    [SerializeField, Header("���̃I�u�W�F�N�g���c�_�Ŕ�������")] private GameObject[] DiscussionMenber;
    //���k�����̐e�I�u�W�F�N�g
    [SerializeField, Header("�c�_�Ҕ����̐e�I�u�W�F�N�g")] private GameObject perentObj;


    void Start()
    {
        //�c�_����������
        DiscussionInit();

        Text.text = speechSet[0].Speech;

        string part = speechSet[0].Speech.Substring(1, 5);

        Debug.Log(part);

    }

    void Update()
    {
        
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
