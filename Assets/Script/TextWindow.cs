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

    [Header("�J�����ݒ�")]
    //��b�J����
    [SerializeField] private Camera TaklCamera_1;//����
    [SerializeField] private Camera TaklCamera_2;//�E
    [SerializeField] private Camera TaklCamera_3;//��

    //�J�����̕\���͈�
    [Header("�����̂�")]//�����̂݁@���E�͉�ʊO
    [SerializeField] private Rect rect_CenterOnly_Left;
    [SerializeField] private Rect rect_CenterOnly_Center;
    [SerializeField] private Rect rect_CenterOnly_Right;

    [Header("�����ƉE")]//�����ƉE�@���͉�ʊO
    [SerializeField] private Rect rect_CenteringRight_Left;
    [SerializeField] private Rect rect_CenteringRight_Center;
    [SerializeField] private Rect rect_CenteringRight_Right;

    [Header("�����ƍ�")]//�����ƍ��@�E�͉�ʊO
    [SerializeField] private Rect rect_CenteringLeft_Left;
    [SerializeField] private Rect rect_CenteringLeft_Center;
    [SerializeField] private Rect rect_CenteringLeft_Right;

    [Header("�����ƍ��E")]//�����ƍ��E�@3�Ƃ���ʓ�
    [SerializeField] private Rect rect_All_Left;
    [SerializeField] private Rect rect_All_Center;
    [SerializeField] private Rect rect_All_Right;

    //���݂̃J�����\���͈�
    private Rect rect_current_Left;
    private Rect rect_current_Center;
    private Rect rect_current_Right;
    //�\���͈͕ω����x
    private float rectMoveSpeed = 0.01f;
    //�\���͈͕ω����e�l
    private double RectSetAllow = 0.0001;

    //���݂̃J���������ݒ�
    TalkCameraManager.CameraSet.CameraDivision currentCameraDivision = TalkCameraManager.CameraSet.CameraDivision.CenterOnly;

    //�q�I�u�W�F�N�g���w�肷�邽�߂�3�̃��@�[�`�����J�����ԍ�
    private const int vcamNumCenter = 0, vcamNumRight = 1, vcamNumLeft = 2;

    //��b�J�����̎���
    private Dictionary<string, TalkCameraManager.TalkSet> talkSetDictionary = new();



    private void Start()
    {
        rect_current_Center = TaklCamera_1.rect = rect_CenterOnly_Center;
        rect_current_Right = TaklCamera_2.rect = rect_CenterOnly_Right;
        rect_current_Left = TaklCamera_3.rect = rect_CenterOnly_Left;

        roomObjectManager = GetComponent<RoomObjectManager>();

        //�����̏�����
        for (int loop = 0; loop < talkCameraManager.talkSet.Length; loop++)
        {
            var talkset = talkCameraManager.talkSet[loop];
            talkSetDictionary.Add(talkset.textinfo, talkset);
        }
        
    }

    void Update()
    {
        //�e�L�X�g��\��
        if (gameManager.playerController == GameManager.PlayerController.TextWindowMode)
        {
            //��b���\�����������s����
            //DisplayDialogueText();
            ProgressText();
            /*
            if (Input.GetKeyDown(KeyCode.Space))
            {

            }
            */
        }   
    }

    //3�̃J�����̕\���͈͂��ړ�����
    void TalkCameraRectMove()
    {
        if (currentCameraDivision == TalkCameraManager.CameraSet.CameraDivision.CenterOnly)
        {
            //��b�J�����\���͈͂̕ω��@�����E�E�E���̏��Ŏw��
            StartCoroutine(CameraRectMove(rect_current_Center, rect_current_Right, rect_CenterOnly_Left,
                                rect_CenterOnly_Center, rect_CenterOnly_Right, rect_CenterOnly_Left));
        }
        else if (currentCameraDivision == TalkCameraManager.CameraSet.CameraDivision.CenterAndRight)
        {
            //��b�J�����\���͈͂̕ω��@�����E�E�E���̏��Ŏw��
            StartCoroutine(CameraRectMove(rect_current_Center, rect_current_Right, rect_current_Left,
                                rect_CenteringRight_Center, rect_CenteringRight_Right, rect_CenteringRight_Left));
        }
        else if (currentCameraDivision == TalkCameraManager.CameraSet.CameraDivision.CenterAndLeft)
        {
            //��b�J�����\���͈͂̕ω��@�����E�E�E���̏��Ŏw��
            StartCoroutine(CameraRectMove(rect_current_Center, rect_current_Right, rect_current_Left,
                                rect_CenteringLeft_Center, rect_CenteringLeft_Right, rect_CenteringLeft_Left));
        }
        else if (currentCameraDivision == TalkCameraManager.CameraSet.CameraDivision.All)
        {
            //��b�J�����\���͈͂̕ω��@�����E�E�E���̏��Ŏw��
            StartCoroutine(CameraRectMove(rect_current_Center, rect_current_Right, rect_current_Left,
                                rect_All_Center, rect_All_Right, rect_All_Left));
        }

    }

    //��b�̐i�s
    public void ProgressText()
    {
        //�p�l������\���Ȃ�\������
        if (!panelObject.activeSelf)
        {
            panelObject.SetActive(true);

            //��l���̕\������
            if (mainTalkChara.GetComponent<Image>().enabled == false) mainTalkChara.GetComponent<Image>().enabled = true;

            //��b�p�J�����̋N��
            CameraEnabled();

            //�ŏ��̉�b�̕\��
            DisplayDialogueText();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            //���̉�b�̕\��
            DisplayDialogueText();
        }
    }

    /// <summary>
    /// ��b���\������
    /// </summary>
    public void DisplayDialogueText()
    {
        
        /*
        //�p�l������\���Ȃ�\������
        if (!panelObject.activeSelf)
        {
            panelObject.SetActive(true);

            //��l���̕\������
            if (mainTalkChara.GetComponent<Image>().enabled == false) mainTalkChara.GetComponent<Image>().enabled = true;

            //��b�p�J�����̋N��
            CameraEnabled();
        }
        */
        //scriptableObject�̏����p�l���ɕ\������
        if (dialogueText.textInfomations.Length > index)
        {
#if false
            /*
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
            */
#else
            //�e�L�X�g�ԍ��̃L�[���猻�݂̃J�����ݒ���擾
            var talkSet = talkSetDictionary[dialogueText.textinfo];
            var cameraSet = talkSet.cameraSet[index];

            //�J�����̒����Ώۂ�ݒ�
            //�����̃J����
            roomObjectManager.RoomObjectPriorityChange(cameraSet.camLookCenter, vcamNumCenter);
            //�E�̃J����
            roomObjectManager.RoomObjectPriorityChange(cameraSet.camLookRight, vcamNumRight);
            //���̃J����
            roomObjectManager.RoomObjectPriorityChange(cameraSet.camLookLeft, vcamNumLeft);

            //�J����������ݒ�
            if (currentCameraDivision != cameraSet.camDivision)
            {
                Debug.Log("�J�����؂�ւ�");
                //���ꂼ��̃J�����̕\���͈͂��w��̈ʒu�܂ňړ�����
                currentCameraDivision = cameraSet.camDivision;

                TalkCameraRectMove();

            }
#endif

            //�b�҂̖��O��\��
            speakerNameText.text = dialogueText.textInfomations[index].speakerName;

            if (!isTyping)
            {
                //��b�C�x���g�𔭐�
                cameraSet.OnTalkEvent.Invoke();

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

            //��l���̓��ߏ���
            if (mainTalkChara.GetComponent<Image>().enabled != false) mainTalkChara.GetComponent<Image>().enabled = false;

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
    private void StopTyping()
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
        //���݂̃A�N�e�B�u�𔽓]������
        TaklCamera_1.enabled = !TaklCamera_1.enabled;
        TaklCamera_2.enabled = !TaklCamera_2.enabled;
        TaklCamera_3.enabled = !TaklCamera_3.enabled;
    }

    //3�̃J�����̕\���͈͂𓯎��ɕω�
    private IEnumerator CameraRectMove(Rect currentRect_Center, Rect currentRect_Right, Rect currentRect_Left,
                                       Rect SetRect_Center, Rect SetRect_Right, Rect SetRect_Left)
    {
        //�S�Ă�Rect�̈ړ������𔻒肷��t���O
        bool setCompletion = false;
        //XYWH�ݒ芮���̃t���O
        bool flgCenter_x = false, flgCenter_y = false, flgCenter_w = false, flgCenter_h = false;
        bool flgRight_x = false, flgRight_y = false, flgRight_w = false, flgRight_h = false;
        bool flgLeft_x = false, flgLeft_y = false, flgLeft_w = false, flgLeft_h = false;
        //�������[�v�j�~�p�̒l
        int noEndlessLoop = 0, LoopStopNum = 10000;

        while (!setCompletion)
        {
            //�ݒ肳�ꂽ�l�ɋ߂������׈Ⴄ�ꍇ���Z�����Z����
            //�����J����X
            if ((SetRect_Center.x - RectSetAllow) <= currentRect_Center.x && currentRect_Center.x <= (SetRect_Center.x + RectSetAllow))
                flgCenter_x = true;
            else
            {
                if (currentRect_Center.x < SetRect_Center.x) currentRect_Center.x += rectMoveSpeed;//���Z
                else currentRect_Center.x -= rectMoveSpeed;//���Z
            }
            //�����J����Y
            if ((SetRect_Center.y - RectSetAllow) <= currentRect_Center.y && currentRect_Center.y <= (SetRect_Center.y + RectSetAllow))
                flgCenter_y = true;
            else
            {
                if (currentRect_Center.y < SetRect_Center.y) currentRect_Center.y += rectMoveSpeed;//���Z
                else currentRect_Center.y -= rectMoveSpeed;//���Z
            }
            //�����J����W
            if ((SetRect_Center.width - RectSetAllow) <= currentRect_Center.width && currentRect_Center.width <= (SetRect_Center.width + RectSetAllow))
                flgCenter_w = true;
            else
            {
                if (currentRect_Center.width < SetRect_Center.width) currentRect_Center.width += rectMoveSpeed;//���Z
                else currentRect_Center.width -= rectMoveSpeed;//���Z
            }
            //�����J����H
            if ((SetRect_Center.height - RectSetAllow) <= currentRect_Center.height && currentRect_Center.height <= (SetRect_Center.height + RectSetAllow))
                flgCenter_h = true;
            else
            {
                if (currentRect_Center.height < SetRect_Center.height) currentRect_Center.height += rectMoveSpeed;//���Z
                else currentRect_Center.height -= rectMoveSpeed;//���Z
            }
            //�����J�����ɑ��
            TaklCamera_1.rect = currentRect_Center;

            //�E�J����X
            if ((SetRect_Right.x - RectSetAllow) <= currentRect_Right.x && currentRect_Right.x <= (SetRect_Right.x + RectSetAllow))
                flgRight_x = true;
            else
            {
                if (currentRect_Right.x < SetRect_Right.x) currentRect_Right.x += rectMoveSpeed;//���Z
                else currentRect_Right.x -= rectMoveSpeed;//���Z
            }
            //�E�J����Y
            if ((SetRect_Right.y - RectSetAllow) <= currentRect_Right.y && currentRect_Right.y <= (SetRect_Right.y + RectSetAllow))
                flgRight_y = true;
            else
            {
                if (currentRect_Right.y < SetRect_Right.y) currentRect_Right.y += rectMoveSpeed;//���Z
                else currentRect_Right.y -= rectMoveSpeed;//���Z
            }
            //�E�J����W
            if ((SetRect_Right.width - RectSetAllow) <= currentRect_Right.width && currentRect_Right.width <= (SetRect_Right.width + RectSetAllow))
                flgRight_w = true;
            else
            {
                if (currentRect_Right.width < SetRect_Right.width) currentRect_Right.width += rectMoveSpeed;//���Z
                else currentRect_Right.width -= rectMoveSpeed;//���Z
            }
            //�E�J����H
            if ((SetRect_Right.height - RectSetAllow) <= currentRect_Right.height && currentRect_Right.height <= (SetRect_Right.height + RectSetAllow))
                flgRight_h = true;
            else
            {
                if (currentRect_Right.height < SetRect_Right.height) currentRect_Right.height += rectMoveSpeed;//���Z
                else currentRect_Right.height -= rectMoveSpeed;//���Z
            }
            //�E�J�����ɑ��
            TaklCamera_2.rect = currentRect_Right;

            //���J����X
            if ((SetRect_Left.x - RectSetAllow) <= currentRect_Left.x && currentRect_Left.x <= (SetRect_Left.x + RectSetAllow))
                flgLeft_x = true;
            else
            {
                if (currentRect_Left.x < SetRect_Left.x) currentRect_Left.x += rectMoveSpeed;//���Z
                else currentRect_Left.x -= rectMoveSpeed;//���Z
            }
            //���J����Y
            if ((SetRect_Left.y - RectSetAllow) <= currentRect_Left.y && currentRect_Left.y <= (SetRect_Left.y + RectSetAllow))
                flgLeft_y = true;
            else
            {
                if (currentRect_Left.y < SetRect_Left.y) currentRect_Left.y += rectMoveSpeed;//���Z
                else currentRect_Left.y -= rectMoveSpeed;//���Z
            }
            //���J����W
            if ((SetRect_Left.width - RectSetAllow) <= currentRect_Left.width && currentRect_Left.width <= (SetRect_Left.width + RectSetAllow))
                flgLeft_w = true;
            else
            {
                if (currentRect_Left.width < SetRect_Left.width) currentRect_Left.width += rectMoveSpeed;//���Z
                else currentRect_Left.width -= rectMoveSpeed;//���Z
            }
            //���J����H
            if ((SetRect_Left.height - RectSetAllow) <= currentRect_Left.height && currentRect_Left.height <= (SetRect_Left.height + RectSetAllow))
                flgLeft_h = true;
            else
            {
                if (currentRect_Left.height < SetRect_Left.height) currentRect_Left.height += rectMoveSpeed;//���Z
                else currentRect_Left.height -= rectMoveSpeed;//���Z
            }
            //���J�����ɑ��
            TaklCamera_3.rect = currentRect_Left;

            noEndlessLoop++;
            //�S�Ă̒l���w��l�ɂȂ��������[�v�I��
            if (noEndlessLoop > LoopStopNum ||
                flgCenter_x && flgCenter_y && flgCenter_w && flgCenter_h &&
                flgRight_x && flgRight_y && flgRight_w && flgRight_h &&
                flgLeft_x && flgLeft_y && flgLeft_w && flgLeft_h)
            {
                //���[�v�I���̃t���O
                setCompletion = true;
            }

            yield return null;
        }

    }
}
