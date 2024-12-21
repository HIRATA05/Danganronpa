using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DiscussionUI : MonoBehaviour
{
    //�c�_��UI


    //�e�L�X�g�Q��
    //���݂̔����ԍ�
    [SerializeField] private TextMeshProUGUI SpeechNumCurrentText;
    //�����ԍ��̍ő�
    [SerializeField] private TextMeshProUGUI SpeechNumMaxText;
    //�����҂̖��O
    [SerializeField] private TextMeshProUGUI SpeakerNameText;
    //�����͂̉摜
    [SerializeField] private Sprite LifeImageNormal;
    [SerializeField] private Sprite LifeImageDamage;

    [SerializeField] private Image[] LifePos;

    //�R�g�_�}�N���X
    public class TruthBulletCylinder
    {
        //�R�g�_�}�̖���
        string BulletName;

        //�R�g�_�}�̃^�C�v
        public enum SpeechType
        {
            refute,
            consent
        }
        public SpeechType BulletType;

        //�R�g�_�}�̕��ΏۂƂȂ锭���@BulletType��TargetSpeech�������ƈ�v�������_�j�o����
        public string TargetSpeech;
    }
    //�R�g�_�}�V�����_�[�@�R�g�_�}�̃N���X����ō��
    [SerializeField] private TruthBulletCylinder[] Bullet;

    //��l���̔�����
    private int Life;
    private int MaxLife = 5;


    //�����͂̐ݒ�
    public void SpeechPowerSet()
    {
        Life = MaxLife;
    }

    //���݂̔����ԍ���ݒ�
    public void SpeechNumSet(int num, string name)
    {
        SpeechNumCurrentText.text = num.ToString();
        SpeakerNameText.text = name;
    }

    //�����ԍ��̍ő��ݒ�
    public void SpeechNumMaxSet(int num)
    {
        SpeechNumMaxText.text = num.ToString();
    }

    //�Ăяo���Ɣ����͂�1����摜���؂�ւ��
    public void SpeechPoworDamage()
    {
        //�����͂Ƀ_���[�W
        Life--;

        if (Life > 0)
        {
            //���݂̔����͂ɂ���ă��C�t�摜��ω�
            LifeImageChange(Life);

        }
        else
        {
            //�c�_�I���̃e�L�X�g�Ə������Ă�
            //�_�j���s����b DiscussionFailureText

        }
    }

    //���C�t�摜��ς���
    public void LifeImageChange(int Life)
    {
        //for�ŏ��ԂɃ��C�t�摜��ς���
        for (int i = 0; i < LifePos.Length - Life; i++)
        {
            //�_���[�W�n�[�g�ɍ����ւ���
            LifePos[i].sprite = LifeImageDamage;
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
