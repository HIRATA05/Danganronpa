using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscussionManager : MonoBehaviour
{
    //�m���X�g�b�v�c�_�̑S�̓I�ȓ�����Ǘ�


    //�c�_�ꏊ�̒��S�_
    [SerializeField] private GameObject centerDiscussionPoint;
    //��]���x
    float rotSpeed = 1.0f;

    //�Ə�


    //�R�g�_�}�V�����_�[


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

    //�~�`�ɕ��Ԑ��k
    [SerializeField] private GameObject[] DiscussionMenber;



    //�c�_�t�F�C�Y�̏�����
    public void DiscussionInit()
    {
        
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
