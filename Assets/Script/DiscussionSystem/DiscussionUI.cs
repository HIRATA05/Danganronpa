using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;

public class DiscussionUI : MonoBehaviour
{
    //�c�_��UI


    //UI�̃E�B���h�E
    [SerializeField] private GameObject DiscussionWindow;
    //���݂̔����ԍ�
    [SerializeField] private TextMeshProUGUI SpeechNumCurrentText;
    //�����ԍ��̍ő�
    [SerializeField] private TextMeshProUGUI SpeechNumMaxText;
    //�����҂̖��O
    [SerializeField] private TextMeshProUGUI SpeakerNameText;
    //���݂̃R�g�_�}�̖��O
    [SerializeField] private TextMeshProUGUI BulletNameText;
    //�����͂̉摜
    [SerializeField] private Sprite LifeImageNormal;
    [SerializeField] private Sprite LifeImageDamage;

    [SerializeField] private GameObject SpeechPoworPanel;

    [SerializeField] private Image[] LifePos;

    //�_�j���s���̃o���A
    [SerializeField] private GameObject Barrier;
    //�o���A���o�I���t���O
    [NonSerialized] public bool isBarrier = false;
    //�_�j���鎞�̃o���A�̐F
    private Color refuteColor = new Color(255, 200, 0, 255);
    private Color consentColor = new Color(255, 200, 0, 255);
    //�o���A�̑ҋ@����
    private float barrierWaitTime = 1.0f;

    //�R�g�_�}�̃^�C�v
    public enum SpeechType
    {
        refute,
        consent,
        None
    }

    //�R�g�_�}�N���X
    [System.Serializable]
    public class TruthBulletCylinder
    {
        //�R�g�_�}���
        public TruthBullets truthBullets;
        
        public SpeechType BulletType;

        //�R�g�_�}�̕��ΏۂƂȂ锭���@BulletType��TargetSpeech�������ƈ�v�������_�j�o����
        public string TargetSpeech;

        //�_�j���s����b
        //public DialogueText DiscussionFailureText;
    }
    //�R�g�_�}�V�����_�[�@�R�g�_�}�̃N���X����ō��
    public TruthBulletCylinder[] Bullet;

    public int BulletCount;//�R�g�_�}�̔ԍ���ς���֐�����邱��

    //��l���̔�����
    private int Life;
    private int MaxLife = 5;


    //�c�_UI�̕\��
    public void DiscussionDispUI(bool isActive)
    {
        if(DiscussionWindow.activeSelf != isActive)
        {
            DiscussionWindow.SetActive(isActive);
        }
        
    }

    //�R�g�_�}�̐ݒ�
    public void BulletSelectSet()
    {
        BulletCount = 0;

        //�R�g�_�}�̖��O�\����ݒ�
        BulletNameText.text = Bullet[BulletCount].truthBullets.bulletName;
    }

    //�R�g�_�}�̑I����؂�ւ���
    public void BulletSelectChange()
    {
        //�ԍ������Z
        BulletCount++;
        Debug.Log("�ԍ�:" + BulletCount + " Length"+ Bullet.Length);
        //�ő吔���߂�0�ɂ���
        if (BulletCount >= Bullet.Length)
        {
            BulletCount = 0;
        }
        Debug.Log("�R�g�_�}�̔ԍ�:"+ BulletCount);
        //�R�g�_�}�̖��O�\�����X�V
        BulletNameText.text = Bullet[BulletCount].truthBullets.bulletName;
    }

    //�����͂̐ݒ�
    public void SpeechPowerSet()
    {
        Life = MaxLife;
    }

    //���݂̔����ԍ���ݒ�
    public void SpeechNumSet(int num, string name)
    {
        num++;
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
        //������UI��\��
        if (!SpeechPoworPanel.activeSelf)
        {
            SpeechPoworPanel.SetActive(true);
        }

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
            //�_�j�Q�[���I�[�o�[����b DiscussionGameOverText
            Debug.Log("�_�j�Q�[���I�[�o�[����b");
        }
    }

    //���C�t�摜��ς���
    public void LifeImageChange(int Life)
    {
        //���݃��C�t�̈�ԏ����u�L�k������


        //for�ŏ��ԂɃ��C�t�摜��ς���
        for (int i = 0; i < LifePos.Length - Life; i++)
        {
            //�_���[�W�n�[�g�ɍ����ւ���
            LifePos[i].sprite = LifeImageDamage;
        }
    }

    /*
    //�_�j���s���o
    public async void DiscussionFailureBarrier(Vector3 BulletPos)
    {
        //���s���o
        //���e�ʒu�Ƀo���A�̈ʒu��ω�
        BarrierPosSet(BulletPos);

        //��������


        //�����x0�ɂȂ�܂Œቺ
        BarrierAlpha();

        //�񓯊��ŏ��������������܂őҋ@
        await UniTask.WaitUntil(() => isBarrier);

    }*/

    //�o���A�̈ʒu���Z�b�g
    public void BarrierPosSet(Vector3 Pos, DiscussionUI.SpeechType speechType)
    {
        //�ʒu��ω�
        Barrier.transform.position = Pos;

        //�����̃^�C�v�ɂ���ăo���A�̐F��ς���
        if(speechType == SpeechType.refute)
        {
            Barrier.GetComponent<Image>().color = refuteColor;
        }
        else if (speechType == SpeechType.consent)
        {
            Barrier.GetComponent<Image>().color = consentColor;
        }

        //�o���A���o�I���t���O��False
        isBarrier = false;
    }

    //�o���A�̓����x��i�X�ƒቺ
    public /*async*/ void BarrierAlpha()
    {
        //�����x0�ɂȂ�܂Œቺ
        for (int i = 0; i < 255; i++)
        {
            //�����x�����炷
            Barrier.GetComponent<Image>().color = Barrier.GetComponent<Image>().color - new Color32(0, 0, 0, 1);
            //���ԑҋ@
            //await UniTask.Delay(TimeSpan.FromSeconds(barrierWaitTime));
        }
        //�o���A���o�I���t���O��True
        isBarrier = true;
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
