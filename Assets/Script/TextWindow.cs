using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class TextWindow : MonoBehaviour
{
    //�e�L�X�g�E�B���h�E

    //�Q�[���}�l�[�W���[
    [SerializeField] private GameManager gameManager;
    //��b���̃J�����̊Ǘ�
    [SerializeField] private TalkCameraManager talkCameraManager;
    //�����̃I�u�W�F�N�g���Ǘ�
    RoomObjectManager roomObjectManager;

    [NonSerialized] public DialogueText dialogueText;
    [SerializeField] private GameObject panelObject;
    [SerializeField] private TextMeshProUGUI speakerNameText;
    [SerializeField] private TextMeshProUGUI speakerDialogueText;
    int index = 0;
    bool isTyping;

    private Coroutine dialogueCoroutine;

    //��b���s����l���L�����̃I�u�W�F�N�g�p�[�c
    [SerializeField] private GameObject mainTalkChara;
    

    //�J����
    [SerializeField] private Camera TaklCamera_1;
    [SerializeField] private Camera TaklCamera_2;
    [SerializeField] private Camera TaklCamera_3;

    [Header("�J�����̕\���͈�")]//�J�����̕\���͈�
    //�����̂݁@���E�͉�ʊO
    [SerializeField] private Rect rect_Left = new Rect(0.0f, 0.0f, 0.25f, 0.9f);
    [SerializeField] private Rect rect_Center = new Rect(0.25f, 0.0f, 0.5f, 1f);
    [SerializeField] private Rect rect_Right = new Rect(0.5f, 0.0f, 0.25f, 0.9f);

    //�����ƉE�@���͉�ʊO
    [SerializeField] private Rect rect_CenteringRight_Left = new Rect(-0.25f, 0.0f, 0.25f, 0.9f);
    [SerializeField] private Rect rect_CenteringRight_Center = new Rect(0.1f, 0.0f, 0.5f, 1f);
    [SerializeField] private Rect rect_CenteringRight_Right = new Rect(0.5f, 0.0f, 0.5f, 0.9f);

    //�����ƍ��@�E�͉�ʊO
    [SerializeField] private Rect rect_CenteringLeft = new Rect(0.25f, 0.0f, 0.5f, 1f);
    //�����ƍ��E�@3�Ƃ���ʓ�


    //���݂̃J���������ݒ�
    TalkCameraManager.CameraSet.CameraDivision currentCameraDivision = TalkCameraManager.CameraSet.CameraDivision.CenterOnly;

    //�q�I�u�W�F�N�g���w�肷�邽�߂�3�̃��@�[�`�����J�����ԍ�
    private const int vcamNumCenter = 0, vcamNumRight = 1, vcamNumLeft = 2;

    //��b�J�����̎���
    private Dictionary<int, TalkCameraManager.TalkSet> talkSetDictionary = new();

    private void Start()
    {
        TaklCamera_1.rect = rect_Center;
        TaklCamera_2.rect = rect_Right;
        TaklCamera_3.rect = rect_Left;

        roomObjectManager = GetComponent<RoomObjectManager>();

        // �����̏�����
        for (int loop = 0; loop < talkCameraManager.talkSet.Length; loop++)
        {
            var talkset = talkCameraManager.talkSet[loop];
            talkSetDictionary.Add(talkset.number, talkset);
        }

    }

    void Update()
    {

        //�e�L�X�g��\��
        if (gameManager.playerController == GameManager.PlayerController.TextWindowMode)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //��b���\�����������s����
                displayDialogueText();

            }
        }   
    }

    //3�̃J�����̕\���͈͂��ړ�����
    void TalkCameraRectMove()
    {
        if (currentCameraDivision == TalkCameraManager.CameraSet.CameraDivision.CenterOnly)
        {
            TaklCamera_1.rect = rect_Center;
            TaklCamera_2.rect = rect_Right;
            TaklCamera_3.rect = rect_Left;
        }
        else if (currentCameraDivision == TalkCameraManager.CameraSet.CameraDivision.CenterAndRight)
        {
            TaklCamera_1.rect = rect_CenteringRight_Center;
            TaklCamera_2.rect = rect_CenteringRight_Right;
            TaklCamera_3.rect = rect_CenteringRight_Left;
        }
        
    }

    /// <summary>
    /// ��b���\������
    /// </summary>
    public void displayDialogueText()
    {
        //�p�l������\���Ȃ�\������
        if (!panelObject.activeSelf)
        {
            panelObject.SetActive(true);

            //��l���̃}�b�v�I�u�W�F�N�g�폜
            if (mainTalkChara.GetComponent<Image>().enabled == false) mainTalkChara.GetComponent<Image>().enabled = true;

            //��b�p�J�����̋N��
            CameraEnabled();

        }

        //scriptableObject�̏����p�l���ɕ\������
        if (dialogueText.textInfomations.Length > index)
        {
#if false
            //��b���̃J������ݒ�
            for(int loop = 0; loop < talkCameraManager.talkSet.Length; loop++)
            {
                //�e�L�X�g�̔ԍ��ƃJ�����ݒ�̔ԍ�����v���Ă��鎞
                if (dialogueText.number == talkCameraManager.talkSet[loop].number)
                {
                    //�J�����̒����Ώۂ�ݒ�
                    //�����̃J����
                    roomObjectManager.RoomObjectPriorityChange(talkCameraManager.talkSet[loop].cameraSet[index].cameraLookObjectCenter, vcamNumCenter);
                    //�E�̃J����
                    roomObjectManager.RoomObjectPriorityChange(talkCameraManager.talkSet[loop].cameraSet[index].cameraLookObjectRight, vcamNumRight);
                    //���̃J����
                    roomObjectManager.RoomObjectPriorityChange(talkCameraManager.talkSet[loop].cameraSet[index].cameraLookObjectLeft, vcamNumLeft);

                    //�J����������ݒ�
                    if(currentCameraDivision != talkCameraManager.talkSet[loop].cameraSet[index].camDivision)
                    {
                        Debug.Log("��l���̃I�u�W�F�N�g�z�u");
                        //���ꂼ��̃J�����̕\���͈͂��w��̈ʒu�܂ňړ�����
                        currentCameraDivision = talkCameraManager.talkSet[loop].cameraSet[index].camDivision;

                        TalkCameraRectMove();

                    }

                }
            }
#else
            var talkSet = talkSetDictionary[dialogueText.number];
            var cameraSet = talkSet.cameraSet[index];

            //�J�����̒����Ώۂ�ݒ�
            //�����̃J����
            roomObjectManager.RoomObjectPriorityChange(cameraSet.cameraLookObjectCenter, vcamNumCenter);
            //�E�̃J����
            roomObjectManager.RoomObjectPriorityChange(cameraSet.cameraLookObjectRight, vcamNumRight);
            //���̃J����
            roomObjectManager.RoomObjectPriorityChange(cameraSet.cameraLookObjectLeft, vcamNumLeft);

            //�J����������ݒ�
            if (currentCameraDivision != cameraSet.camDivision)
            {
                Debug.Log("��l���̃I�u�W�F�N�g�z�u");
                //���ꂼ��̃J�����̕\���͈͂��w��̈ʒu�܂ňړ�����
                currentCameraDivision = cameraSet.camDivision;

                TalkCameraRectMove();

            }
#endif

            //�b�҂̖��O��\��
            speakerNameText.text = dialogueText.textInfomations[index].speakerName;

            if (!isTyping)
            {
                dialogueCoroutine = StartCoroutine(TypeDialogueText(dialogueText.textInfomations[index].paragraphs));
            }
            else
            {
                stopTyping();
            }
        }
        else
        {
            //��b���I���������߃p�l�����\���ɂ���
            speakerNameText.text = "";
            speakerDialogueText.text = "";
            panelObject.SetActive(false);

            //��l���̃}�b�v�I�u�W�F�N�g�폜
            if(mainTalkChara.GetComponent<Image>().enabled != false) mainTalkChara.GetComponent<Image>().enabled = false;

            //��b�p�J�����̔�\��
            CameraEnabled();

            //��Ԃ����������ďƏ����[�h�ɂ���
            gameManager.playerController = GameManager.PlayerController.ReticleMode;

            index = 0;
        }
        
    }

    /// <summary>
    /// �_�C�A���O�̃e�L�X�g���ꕶ���Â\�����Ă����܂�
    /// </summary>
    /// <param name="paragraph">��b��1��</param>
    /// <returns>IEnumerator</returns>
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

    /// <summary>
    /// 1�����Â\�����I�����e�L�X�g�S����\������
    /// </summary>
    private void stopTyping()
    {
        StopCoroutine(dialogueCoroutine);
        //���\���̉�b����S���\������
        speakerDialogueText.text = dialogueText.textInfomations[index].paragraphs;
        isTyping = false;
        //���̉�b���C���f�b�N�X��i�߂�
        index++;
    }

    //��b�J�����̋N��
    public void CameraEnabled()
    {
        //FALSE�̎�TRUE
        //    if (!TaklCamera_1.enabled) TaklCamera_1.enabled = true;
        //    if (!TaklCamera_2.enabled) TaklCamera_2.enabled = true;
        //    if (!TaklCamera_3.enabled) TaklCamera_3.enabled = true;

        //    //TRUE�̎�FALSE
        //    if (TaklCamera_1.enabled) TaklCamera_1.enabled = false;
        //    if (TaklCamera_2.enabled) TaklCamera_2.enabled = false;
        //    if (TaklCamera_3.enabled) TaklCamera_3.enabled = false;

        TaklCamera_1.enabled = !TaklCamera_1.enabled;
        TaklCamera_2.enabled = !TaklCamera_2.enabled;
        TaklCamera_3.enabled = !TaklCamera_3.enabled;

    }
}
