using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static DiscussionEventManager;
using static TalkCameraManager;

public class DiscussionTalkModeWindow : MonoBehaviour
{
    //�c�_���̃e�L�X�g�E�B���h�E

    //�Q�[���}�l�[�W���[
    [SerializeField] private GameManager gameManager;
    [SerializeField] private DiscussionManager discussionManager;
    [SerializeField] private DiscussionEventManager discussionEventManager;

    /*[NonSerialized] */public DialogueText dialogueText;

    [SerializeField] private GameObject panelObject;
    [SerializeField] private TextMeshProUGUI speakerNameText;
    [SerializeField] private TextMeshProUGUI speakerDialogueText;
    int index = 0;
    bool isTyping;

    [SerializeField, Header("�b�҂̖��O�摜")] private Image speakerName;
    [SerializeField] private Sprite speakerNameDetective;//�T��
    [SerializeField] private Sprite speakerNameLacky;//�K�^
    [SerializeField] private Sprite speakerNameHacker;//�n�b�J�[
    [SerializeField] private Sprite speakerNameArcher;//�|����
    [SerializeField] private Sprite speakerNameThief;//����

    private Coroutine dialogueCoroutine;

    //�c�_�Q�������o�[�̏��z��
    [NonSerialized] public GameObject[] DiscussionMenber;

    //��b�I����̈ڍs��
    public enum TalkFinish
    {
        DiscussionMode,//�c�_�̍ŏ��Ɉڍs
        AdventureMode,//�T���Ɉڍs
        TakeAroundDiscussionMode,//�ꏄ���ċc�_�̍ŏ��Ɉڍs
    }
    [NonSerialized] public TalkFinish talkFinish;

    //��b�J�����̎���
    private Dictionary<string, DiscussionEventSet> talkSetDictionary = new();


    

    void Start()
    {
        //�����̏�����
        for (int loop = 0; loop < discussionEventManager.discussionEventSet.Length; loop++)
        {
            var talkset = discussionEventManager.discussionEventSet[loop];
            talkSetDictionary.Add(talkset.textinfo, talkset);
        }
    }

    void Update()
    {
        //�e�L�X�g��\��
        if (gameManager.playerController == GameManager.PlayerController.DiscussionMode && discussionManager.discussion == DiscussionManager.DiscussionMode.Talk)
        {
            //��b���\�����������s����
            ProgressText();
        }
    }

    //��b�����Z�b�g
    public void TextSet(DialogueText talkText)
    {
        dialogueText = talkText;
    }

    //��b�̐i�s
    public void ProgressText()
    {
        //�p�l������\���Ȃ�\������
        if (!panelObject.activeSelf)
        {
            panelObject.SetActive(true);

            //�ŏ��̉�b�̕\��
            DisplayDialogueText();
        }

        if (Input.GetMouseButtonDown(0) /*Input.GetKeyDown(KeyCode.Space) gameManager.KeyInputSpace()*/)
        {
            //���̉�b�̕\��
            DisplayDialogueText();
        }
    }

    //��b���\������
    public void DisplayDialogueText()
    {
        //scriptableObject�̏����p�l���ɕ\������
        if (dialogueText.textInfomations.Length > index)
        {
            //�e�L�X�g�ԍ��̃L�[���猻�݂̃J�����ݒ���擾
            var talkSet = talkSetDictionary[dialogueText.textinfo];
            var talkEvent = talkSet.discussionEvent.OnTalkEvent[index];

            //�\�����镶���̐F��ݒ�ɂ���ĕς���
            if(dialogueText.textInfomations[index].colorType == TextInfomation.TextColorType.Blue)
                speakerDialogueText.color = Color.cyan;
            else speakerDialogueText.color = Color.white;

            
            //�b�҂̖��O��\��
            speakerNameText.text = dialogueText.textInfomations[index].speakerName;
            if (dialogueText.textInfomations[index].speakerName == "�V�m�m�� �g�I��")
            {
                speakerName.color = Color.white;
                speakerName.sprite = speakerNameDetective;
            }
            else if (dialogueText.textInfomations[index].speakerName == "�R�g�M �}�i")
            {
                speakerName.color = Color.white;
                speakerName.sprite = speakerNameLacky;
            }
            else if (dialogueText.textInfomations[index].speakerName == "�~�i�Z �c�~�J")
            {
                speakerName.color = Color.white;
                speakerName.sprite = speakerNameHacker;
            }
            else if (dialogueText.textInfomations[index].speakerName == "���C�[�C ����")
            {
                speakerName.color = Color.white;
                speakerName.sprite = speakerNameArcher;
            }
            else if (dialogueText.textInfomations[index].speakerName == "�j�J�C�h�E �n���m")
            {
                speakerName.color = Color.white;
                speakerName.sprite = speakerNameThief;
            }
            else
            {
                speakerName.color = Color.clear;
                speakerName.sprite = null;
            }

            //��b���̃L�����ɃJ������������
            SpeechCameraSet(speakerNameText);

            if (!isTyping)
            {
                //��b�C�x���g�𔭐�
                talkEvent.Invoke();

                dialogueCoroutine = StartCoroutine(TypeDialogueText(dialogueText.textInfomations[index].paragraphs));
            }
            else
            {
                StopTyping();
            }
        }
        else
        {
            //��b���I���������߃p�l�����\���ɂ���
            speakerNameText.text = "";
            speakerDialogueText.text = "";
            panelObject.SetActive(false);

            //��Ԃ����������ăm���X�g�b�v�c�_���T���Ɉڍs����
            switch (talkFinish)
            {
                case TalkFinish.DiscussionMode:
                    //�c�_�Ɉڍs
                    discussionManager.ShootingInit();
                    Debug.Log("�c�_�Ɉڍs");
                    break;
                    
                case TalkFinish.TakeAroundDiscussionMode:
                    //�c�_���ĊJ
                    discussionManager.DiscussionStart();
                    Debug.Log("�ꏄ���ċc�_���ĊJ");
                    break;
                    
                case TalkFinish.AdventureMode:
                    //�T���Ɉڍs
                    gameManager.ReticleModeChange();
                    Debug.Log("�Ə�����T���Ɉڍs");
                    break;
                
            }

            index = 0;
        }
    }

    //�_�C�A���O�̃e�L�X�g���ꕶ���Â\��
    private IEnumerator TypeDialogueText(string paragraph)
    {
        string displayText = "";
        isTyping = true;
        int colorIndex = 0;
        foreach (char c in paragraph)
        {
            colorIndex++;
            speakerDialogueText.text = paragraph;
            //#006AB6 #0095d9
            displayText = speakerDialogueText.text.Insert(colorIndex, "<color=#00000000>");
            speakerDialogueText.text = displayText;
            yield return new WaitForSeconds(0.05f);
        }
        isTyping = false;
        //���̉�b���C���f�b�N�X��i�߂�
        index++;
    }

    //1�����Â\�����I�����e�L�X�g�S����\������
    private void StopTyping()
    {
        StopCoroutine(dialogueCoroutine);
        //���\���̉�b����S���\������
        speakerDialogueText.text = dialogueText.textInfomations[index].paragraphs;
        isTyping = false;
        //���̉�b���C���f�b�N�X��i�߂�
        index++;
    }

    //�����҂̃J������ݒ�
    public void SpeechCameraSet(TextMeshProUGUI speakerName)
    {
        for (int i = 0; i < DiscussionMenber.Length; i++)
        {
            Debug.Log("speakerName.text:"+ speakerName.text + "  <SpeechName>().speechName"+ DiscussionMenber[i].GetComponent<SpeechName>().speechName);
            if (speakerName.text == DiscussionMenber[i].GetComponent<SpeechName>().speechName)
            {
                Debug.Log("CameraPriority(i)�Ăяo��");
                //�J�����𔭌��҂Ɍ�����
                discussionManager.CameraPriority(i);
            }
        }
    }
}
