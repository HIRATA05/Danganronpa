using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.UIElements;
using Cinemachine;
using Cysharp.Threading.Tasks;
using static UnityEditor.PlayerSettings;
using static DiscussionUI;

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
        Shooting,//�m���X�g�b�v�c�_
        Effect//�c�_�����o
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

    //�c�_�J�n����b
    [SerializeField, Header("�J�n�̉�b")] private DialogueText DiscussionStartText;
    //�c�_�ꏄ����b
    [SerializeField, Header("�ꏄ���̉�b")] private DialogueText DiscussionTakeAroundText;
    //�c�_�I������b
    [SerializeField, Header("�c�_�I�����̉�b")] private DialogueText DiscussionFinishText;
    //�_�j�Q�[���I�[�o�[����b
    [SerializeField, Header("�Q�[���I�[�o�[���̉�b")] private DialogueText DiscussionGameOverText;
    //�c�_�I����̏Ə����[�h�ł̉�b
    [Header("�c�_�I����̉�b")] public DialogueText AfterText;

    //������\������e�L�X�g
    [SerializeField, Header("�����Z���t")] private GameObject speechText;
    
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

        /*
        //�_�j�����ӂ�
        public enum SpeechType
        {
            refute,
            consent,
            None
        }
        */
        public DiscussionUI.SpeechType speechType = DiscussionUI.SpeechType.None;

        //�����̈ړ��p�^�[��
        public enum SpeechMovePattern
        {
            RightToLeft,//�E���獶
            LeftToRight,//������E
            TopToBottom,//�ォ�牺
            BottomToTop,//�������
            Stop,//��~
        }
        public SpeechMovePattern speechMove;

        //�R�g�_�}���ԈႦ�����̉�b
        public DialogueText DiscussionMistakeText;

    }
    [Header("�_�j�J���[�R�[�h#ffa500�@���ӃJ���[�R�[�h#41A2E1")]
    public SpeechSet[] speechSet;

    private SpeechSet CurrentSpeech;
    private string CurrentSpeechWeekPoint;
    private DiscussionUI.SpeechType CurrentSpeechType;

    private string CurrentWeekPoint;
    //[NonSerialized] public TextMeshProUGUI SpeechAll;

    //�c�_�̐i�s�ԍ�
    private int DiscussionNum = 0;

    //�c�_�J�n�̍ۂ̎���
    private float elapsedTime = 0.0f;
    private float startTime = 6.0f;

    //�~�`�ɕ��Ԑ��k
    [SerializeField, Header("�c�_�ŕ��ԃL����")] private GameObject[] DiscussionMenber;
    //���k�����̐e�I�u�W�F�N�g
    [SerializeField, Header("�c�_�Ҕ����̐e�I�u�W�F�N�g")] private GameObject perentObj;
    //�����������k�̔z��
    private GameObject[] InstantiateMenber = new GameObject[0];

    private bool isDiscussionInitCalled = false;
    private bool isTextSetCalled = false;

    //�c�_�O�̉��o�����̃t���O
    private bool BeforeDiscussion = false;

    //�c�_�����ǂ���
    [NonSerialized] public bool discussionProgress = false;

    //������\������e�L�X�g�̈ړ��ʒu
    //��
    private Vector3 speechPosLeft = new Vector3(-600,0,0);
    //�E
    private Vector3 speechPosRight = new Vector3(600, 0, 0);
    //��
    private Vector3 speechPosTop = new Vector3(0, 400, 0);
    //��
    private Vector3 speechPosBottom = new Vector3(0, -400, 0);


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

            //�R�g�_�}�̃Z�b�g�A�b�v
            discussionUI.BulletSelectSet();

            //�c�_�J�n���̉�b�f�[�^������
            gameManager.OpenDiscussionWindow(DiscussionStartText, DiscussionTalkModeWindow.TalkFinish.DiscussionMode);
            //�m���X�g�b�v�c�_�̊J�n
            discussion = DiscussionMode.Talk;
        }

    }

    //�c�_���̏���
    private void DiscussionPlay()
    {
        //��b
        if (discussion == DiscussionMode.Talk)
        {
            //��b����UI�\��

        }
        //�c�_�J�n�O�̉��o
        else if (discussion == DiscussionMode.BeforeDiscussion)
        {
            //Debug.Log("�c�_�J�n�o�ߎ��ԁF" + elapsedTime);

            //�c�_�J�n�̕������o����
            if (!BeforeDiscussion)
            {
                BeforeDiscussion = true;

                //�c�_�J�n�̕������o
                StartCoroutine(discussionUI.DiscussionBeforeStartEffect());
            }

            if (elapsedTime > startTime)
            {
                Debug.Log("=�c�_���J�n=");
                elapsedTime = 0;

                BeforeDiscussion = false;

                //�J�������~
                //DiscussionCamera.GetComponent<CameraRotation>().RotateOff();
                MainVirtualCamera.GetComponent<CameraRotation>().RotateOff();

                //�c�_�J�n����
                DiscussionStart();
                
            }

            //���Ԍo��
            elapsedTime += Time.deltaTime;
        }
        //�c�_
        else if (discussion == DiscussionMode.Shooting && discussionProgress)
        {
            
            //�E�N���b�N�ŃR�g�_�}��؂�ւ�
            if (Input.GetMouseButtonDown(1))
            {
                //�R�g�_�}�����ւ�
                discussionUI.BulletSelectChange();
            }

            //��莞�Ԃ��Ƃɕ�����ω�
            if (!isTextSetCalled)
            {
                isTextSetCalled = true;

                //���݂̔������擾
                CurrentSpeech = speechSet[DiscussionNum];
                CurrentSpeechWeekPoint = speechSet[DiscussionNum].WeekPointSpeech;
                Debug.Log("CurrentSpeech�E�B�[�N�|�C���g�F" + CurrentSpeech.WeekPointSpeech + " CurrentSpeechWeekPoint:" + CurrentSpeechWeekPoint);
                CurrentSpeechType = speechSet[DiscussionNum].speechType;
                //CurrentWeekPoint = speechSet[DiscussionNum].WeekPointSpeech;
                //�����̑傫���ƐF���Z�b�g
                speechSet[DiscussionNum].NormalSpeechBefore = "<size=60><color=white>" + speechSet[DiscussionNum].NormalSpeechBefore;

                if(speechSet[DiscussionNum].speechType == DiscussionUI.SpeechType.refute)
                    speechSet[DiscussionNum].WeekPointSpeech = "<size=65><color=#ffa500>" + speechSet[DiscussionNum].WeekPointSpeech;
                else if(speechSet[DiscussionNum].speechType == DiscussionUI.SpeechType.consent)
                    speechSet[DiscussionNum].WeekPointSpeech = "<size=65><color=#41A2E1>" + speechSet[DiscussionNum].WeekPointSpeech;
                else
                    speechSet[DiscussionNum].WeekPointSpeech = "<size=60><color=white>" + speechSet[DiscussionNum].WeekPointSpeech;

                speechSet[DiscussionNum].NormalSpeechAfter = "<size=60><color=white>" + speechSet[DiscussionNum].NormalSpeechAfter;
                //�S�Ă̕��������킹��1�̕���������
                string speech = speechSet[DiscussionNum].NormalSpeechBefore + " " + speechSet[DiscussionNum].WeekPointSpeech + " " + speechSet[DiscussionNum].NormalSpeechAfter;
                //Debug.Log(speech);

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

                //�������ړ�
                StartCoroutine(SpeechMove(speechSet[DiscussionNum]));

                //�����ԍ��Ɣ����҂�ݒ�
                discussionUI.SpeechNumSet(DiscussionNum, speechSet[DiscussionNum].SpeechName);

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
                    //���������̕ω��t���O
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
    public IEnumerator SpeechMove(SpeechSet speech)
    {
        //�ړ��p�^�[���ɂ���Ĕ����̔����ʒu�⓮�����ω�

        //RectTransform���擾
        RectTransform rectTransform = speechText.GetComponent<RectTransform>();

        float duration = nextSpeechTime;//�I�_�܂ł̎���
        float time = 0.0f;//�o�ߎ���
        //�n�_�ƏI�_�̈ʒu
        Vector3 startPos = Vector3.zero, goalPos = Vector3.zero;

        //������E
        if (speech.speechMove == SpeechSet.SpeechMovePattern.LeftToRight)
        {
            //�n�_�ƏI�_��ݒ�
            startPos = speechPosLeft; goalPos = speechPosRight;
        }
        //�E���獶
        else if (speech.speechMove == SpeechSet.SpeechMovePattern.RightToLeft)
        {
            startPos = speechPosLeft; goalPos = speechPosRight;
        }
        //�ォ�牺
        else if (speech.speechMove == SpeechSet.SpeechMovePattern.TopToBottom)
        {
            startPos = speechPosTop; goalPos = speechPosBottom;
        }
        //�������
        else if (speech.speechMove == SpeechSet.SpeechMovePattern.BottomToTop)
        {
            startPos = speechPosBottom; goalPos = speechPosTop;
        }
        //��~
        else if (speech.speechMove == SpeechSet.SpeechMovePattern.Stop)
        {
            startPos = Vector3.zero; goalPos = Vector3.zero;
        }

        //�����ʒu��ݒ�
        rectTransform.localPosition = startPos;
        
        while (time < duration)
        {
            time += Time.deltaTime;
            float t = Mathf.Clamp01(time / duration);

            //�ړI�̈ʒu�܂�duration�b�������ĕ�Ԃňړ�
            rectTransform.localPosition = Vector3.Lerp(startPos, goalPos, t);//�ړI�̈ʒu�Ɉړ�
            yield return null;
        }
        
        //yield return null;
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
        //�c�_����Ԃ�����
        discussionProgress = false;

        //�c�_�ꏄ���̉�b�f�[�^������
        gameManager.OpenDiscussionWindow(DiscussionTakeAroundText, DiscussionTalkModeWindow.TalkFinish.TakeAroundDiscussionMode);
        //��b�J�n
        discussion = DiscussionMode.Talk;

        //DiscussionEnd();
        //UI���\��
        discussionUI.DiscussionDispUI(false);
        //�����̃f�[�^��������
        speechText.GetComponent<TextMeshProUGUI>().text = "";
        currentTime = 0;
        isTextSetCalled = false;
        //�c�_�ԍ������Z�b�g
        DiscussionNum = 0;
    }

    //�c�_�J�n
    public void DiscussionStart()
    {
        //�J��������l���Ɍ�����
        CameraPriority(0);

        //�ő唭���ԍ���ݒ�
        discussionUI.SpeechNumMaxSet(speechSet.Length);

        //UI��\��
        discussionUI.DiscussionDispUI(true);

        //��L�I����c�_�J�n
        discussion = DiscussionMode.Shooting;
        discussionProgress = true;
    }

    //�c�_�Ŕ����������̂����Z�b�g
    public void DiscussionEnd()
    {
        //������������
        speechText.GetComponent<TextMeshProUGUI>().text = "";
        currentTime = 0;
        isTextSetCalled = false;
        //�c�_�ԍ������Z�b�g
        DiscussionNum = 0;

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

        //���o�`�ʂ̏�ԂɕύX
        discussion = DiscussionMode.Effect;

        //�_�j���o�摜�\��
        //�p�x��ς���A�j���[�V����
        discussionUI.RefuteImageEffect(CurrentSpeechType);
        //��ʂ̊���鉉�o

        //�񓯊��ŏ��������������܂őҋ@
        //await UniTask.WaitUntil(() => discussionUI.);

        //UI���\��
        discussionUI.DiscussionDispUI(false);

        //�c�_�I����̉�b�f�[�^������
        gameManager.OpenDiscussionWindow(DiscussionFinishText, DiscussionTalkModeWindow.TalkFinish.AdventureMode);
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

        Debug.Log("�擾��������:"+wordInfo.GetWord() + " "+ CurrentSpeechWeekPoint /*CurrentSpeech.WeekPointSpeech CurrentSpeech.WeekPointSpeech*/);

        //�擾�����������E�B�[�N�|�C���g�Ɠ���������
        if(wordInfo.GetWord() == CurrentSpeechWeekPoint /*CurrentSpeech.WeekPointSpeech CurrentSpeech.WeekPointSpeech*/)
        {
            //��������������True��Ԃ�
            return true;
        }
        return false;
    }

    //���݂̃R�g�_�}�ŃE�B�[�N�|�C���g�𔻒�
    public bool TruthBulletCompare()
    {
        //�R�g�_�}�̃^�C�v�ƃE�B�[�N�|�C���g���_�j�����ӂ�����
        if (discussionUI.Bullet[discussionUI.BulletCount].BulletType == CurrentSpeechType/*CurrentSpeech.speechType*/)
        {
            Debug.Log("�R�g�_�}�̕W�I:" + discussionUI.Bullet[discussionUI.BulletCount].TargetSpeech + " �擾�����E�B�[�N�|�C���g�F" + CurrentSpeechWeekPoint);
            //�R�g�_�}�Ɣ�����������������
            if (discussionUI.Bullet[discussionUI.BulletCount].TargetSpeech == CurrentSpeechWeekPoint /*CurrentSpeech.WeekPointSpeech*/)
            {
                return true;
            }
        }

        return false;
    }

    //�E�B�[�N�|�C���g��_�j�o���Ȃ���������b�Ɉڍs
    public async void DiscussionFailureChange(Vector3 BulletPos)
    {
        //���s���o�`�ʂ̏�ԂɕύX
        discussion = DiscussionMode.Effect;
        discussionProgress = false;

        //���e�ʒu�Ƀo���A�̈ʒu��ω�
        discussionUI.BarrierPosSet(BulletPos, CurrentSpeechType/*CurrentSpeech.speechType*/);

        //�����x0�ɂȂ�܂Œቺ
        StartCoroutine(discussionUI.BarrierAlpha());
        
        //�񓯊��ŏ��������������܂őҋ@
        await UniTask.WaitUntil(() => discussionUI.isBarrier);

        //������������
        speechText.GetComponent<TextMeshProUGUI>().text = "";
        currentTime = 0;
        isTextSetCalled = false;
        //�c�_�ԍ������Z�b�g
        DiscussionNum = 0;
        //UI���\��
        discussionUI.DiscussionDispUI(false);
        //�c�_���s���̉�b�f�[�^������
        gameManager.OpenDiscussionWindow(CurrentSpeech.DiscussionMistakeText, DiscussionTalkModeWindow.TalkFinish.TakeAroundDiscussionMode);
        //��b�̊J�n
        discussion = DiscussionMode.Talk;
    }
    
}