using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static DiscussionEventManager;
using static TalkCameraManager;

public class DiscussionTalkModeWindow : MonoBehaviour
{
    //�c�_���̃e�L�X�g�E�B���h�E

    //�Q�[���}�l�[�W���[
    [SerializeField] private GameManager gameManager;
    [SerializeField] private DiscussionManager discussionManager;
    [SerializeField] private DiscussionEventManager discussionEventManager;

    public DialogueText dialogueText;

    [SerializeField] private GameObject panelObject;
    [SerializeField] private TextMeshProUGUI speakerNameText;
    [SerializeField] private TextMeshProUGUI speakerDialogueText;
    int index = 0;
    bool isTyping;

    private Coroutine dialogueCoroutine;

    //��b�I����̈ڍs��
    public enum TalkFinish
    {
        DiscussionMode,
        AdventureMode,
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

        if (/*Input.GetKeyDown(KeyCode.Space)*/gameManager.KeyInputSpace())
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

            //�b�҂̖��O��\��
            speakerNameText.text = dialogueText.textInfomations[index].speakerName;

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
                    discussionManager.ShootingInit();
                    Debug.Log("�c�_�Ɉڍs");
                    break;
                case TalkFinish.AdventureMode:
                    gameManager.ReticleModeChange();
                    Debug.Log("�Ə�����T���Ɉڍs");
                    break;
                default:
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
}
