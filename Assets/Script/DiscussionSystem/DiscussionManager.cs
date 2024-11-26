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
    [SerializeField, Header("�c�_�ŕ��ԃL����")] private GameObject[] DiscussionMenber;
    //���k�����̐e�I�u�W�F�N�g
    [SerializeField, Header("�c�_�Ҕ����̐e�I�u�W�F�N�g")] private GameObject perentObj;


    void Start()
    {
        //�c�_����������
        DiscussionInit();

        Text.text = speechSet[0].Speech;

        //�ŏ��ɃL�����̉~�`�ɉ����Đ��b�ԉ�]

        //�c�_�J�n�̉��o

        //�Ə��𓮂�����



        
    }

    void Update()
    {
        //���Ԍo�߂Ŏ��̕����ɐi��

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
