using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using Unity.VisualScripting;

public class DiscussionUI : MonoBehaviour
{
    //�c�_��UI

    //�Q�[���}�l�[�W���[
    [SerializeField] private GameManager gameManager;
    //�c�_�S�̂̊Ǘ�
    //[SerializeField] private DiscussionManager discussionManager;
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
    //���˂���R�g�_�}�̕\����
    [SerializeField] private TextMeshProUGUI ShotBulletText;
    //�����͂̉摜
    [SerializeField] private Sprite LifeImageNormal;
    [SerializeField] private Sprite LifeImageDamage;

    [SerializeField] private GameObject SpeechPoworPanel;

    [SerializeField] private Image[] LifePos;

    //�c�_�J�n���o
    [SerializeField] private GameObject NonStopDiscussionEffect;
    [SerializeField] private GameObject StartEffect;
    

    //�_�j���s���̃o���A
    [SerializeField] private GameObject Barrier;
    //�o���A���o�I���t���O
    [NonSerialized] public bool isBarrier = false;
    //�_�j���o�摜
    [SerializeField] private GameObject RefuteEffect;
    [SerializeField] private GameObject ConsentEffect;
    //���ӑ���̉摜
    [SerializeField] private Image ConsentChara;
    //�_�j���o�I���t���O
    [NonSerialized] public bool isRefuteFinish = false;
    //�_�j���鎞�̃o���A�̐F
    private Color32 refuteColor = new Color32(255, 200, 0, 255);
    private Color32 consentColor = new Color32(0, 200, 255, 255);
    //�o���A�̑ҋ@����
    //private float barrierWaitTime = 0.1f;

    //�R�g�_�}�̃^�C�v
    public enum SpeechType
    {
        refute,//�_�j
        consent,//����
        None//����
    }

    //�R�g�_�}�N���X
    [System.Serializable]
    public class TruthBulletCylinder
    {
        //�R�g�_�}���
        public TruthBullets truthBullets;

        //�R�g�_�}�̃^�C�v
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
    public int MaxLife = 5;

    //�t�F�[�h�p���o�p�l��
    [SerializeField] private Image FadeEffect;

    

    //�c�_UI�̕\��
    public void DiscussionDispUI(bool isActive)
    {
        if(DiscussionWindow.activeSelf != isActive)
        {
            DiscussionWindow.SetActive(isActive);
            SpeechPoworPanel.SetActive(isActive);
        }
    }

    //�R�g�_�}�̐ݒ�
    public void BulletSelectSet()
    {
        BulletCount = 0;

        //�R�g�_�}�̖��O�\����ݒ�
        BulletNameText.text = Bullet[BulletCount].truthBullets.bulletName;

        //���˂���R�g�_�}�̖��O�\����ς���
        ShotBulletText.text = Bullet[BulletCount].truthBullets.bulletName;
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

        //���˂���R�g�_�}�̖��O�\����ς���
        ShotBulletText.text = Bullet[BulletCount].truthBullets.bulletName;
    }

    //�R�g�_�}���ˎ��̃V�����_�[�̕�������
    public void CylinderTextErase()
    {
        //�R�g�_�}�̖��O�\���𓧖�
        BulletNameText.color = Color.clear;
        
    }

    //�R�g�_�}���ˎ��̃V�����_�[�̕�������
    public void CylinderTextReturn()
    {
        //�F�����ɕς��ĉ���
        BulletNameText.color = Color.black;
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
        if(Life < 0)//0�ȉ��ɂȂ�����0�ɂ���
        {
            Life = 0;
        }

        if (Life > 0)
        {
            //���݂̔����͂ɂ���ă��C�t�摜��ω�
            LifeImageChange(Life);

        }
        /*
        else
        {
            //�c�_�I���̃e�L�X�g�Ə������Ă�
            //�_�j�Q�[���I�[�o�[����b DiscussionGameOverText
            //discussionManager.DiscussionGameOver();
            Debug.Log("�_�j�Q�[���I�[�o�[����b");
        }
        */
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

    //���C�t���ɂ���ăX�R�A�����Z
    public void LifeCountScoreUp()
    {
        gameManager.eventFlagData.Score += (Life * 5);
    }

    //�_�j���o
    public IEnumerator RefuteImageEffect(DiscussionUI.SpeechType speechType)
    {
        //�_�j���o�J�n
        isRefuteFinish = false;
        //�����̃^�C�v�ɂ���ĕ\������摜��ς���
        if (speechType == SpeechType.refute)
        {
            //�����_�j���o�摜�\��
            RefuteEffect.SetActive(true);
        }
        else if (speechType == SpeechType.consent)
        {
            //���Ә_�j���o�摜�\��
            ConsentEffect.SetActive(true);
        }

        //�ҋ@���Ԑݒ�
        float waitTime = 3;
        //��莞�ԑҋ@
        yield return new WaitForSeconds(waitTime);
        /*
        Color color = FadeEffect.color;
        float duration = 1.0f;
        int FadeInAlpha = 1;
        int FadeOutAlpha = 0;
        //�t�F�[�h�C��������
        while (!Mathf.Approximately(color.a, FadeInAlpha))
        {
            float changePerFrame = Time.deltaTime / duration;
            color.a = Mathf.MoveTowards(color.a, FadeInAlpha, changePerFrame);
            FadeEffect.color = color;
            yield return null;
        }
        */
        

        //�t�F�[�h�C��������
        StartCoroutine(gameManager.FadeScreen(true));

        //�_�j���o�摜���\��
        if (RefuteEffect.activeSelf)
        {
            RefuteEffect.SetActive(false);
        }
        if (ConsentEffect.activeSelf)
        {
            ConsentEffect.SetActive(false);
        }
        //�_�j���o�I��
        isRefuteFinish = true;

        //�t�F�[�h�A�E�g������
        StartCoroutine(gameManager.FadeScreen(false));
        /*
        //�t�F�[�h�A�E�g������
        while (!Mathf.Approximately(color.a, FadeOutAlpha))
        {
            float changePerFrame = Time.deltaTime / duration;
            color.a = Mathf.MoveTowards(color.a, FadeOutAlpha, changePerFrame);
            FadeEffect.color = color;
            yield return null;
        }
        */
    }

    //���ӑ���̉摜��ݒ�
    public void ConsentCharaSetting(Sprite consentChara)
    {
        ConsentChara.sprite = consentChara;
    }

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
        else if (speechType == SpeechType.None)
        {
            Barrier.GetComponent<Image>().color = refuteColor;
        }
        
        //Barrier.GetComponent<Image>().color = Color.white;
        //�o���A���o�I���t���O��False
        isBarrier = false;
    }

    //�o���A�̓����x��i�X�ƒቺ
    public IEnumerator BarrierAlpha()
    {
        //�����x0�ɂȂ�܂Œቺ
        for (int i = 0; i < 255; i++)
        {
            Debug.Log("�o���A�̓����x�ቺ��");
            //�����x�����炷
            Barrier.GetComponent<Image>().color -= new Color32(0, 0, 0, 1);
            //1�t���[���ҋ@
            yield return null;
        }
        Debug.Log("�o���A�̓����x�ቺ�I��");
        //�o���A���o�I���t���O��True
        isBarrier = true;
    }

    //�c�_�J�n���o
    public IEnumerator DiscussionBeforeStartEffect()
    {
        //�m���X�g�b�v�c�_�\��

        NonStopDiscussionEffect.SetActive(true);
        //�A�j���[�V����

        yield return new WaitForSeconds(3);

        //�m���X�g�b�v�c�_�\������
        NonStopDiscussionEffect.SetActive(false);
        //�J�n�����\��
        StartEffect.SetActive(true);
        //�A�j���[�V����

        yield return new WaitForSeconds(2);

        //�J�n�����\������
        StartEffect.SetActive(false);
    }

    
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
