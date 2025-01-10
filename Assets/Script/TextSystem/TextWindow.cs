using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using static TalkCameraManager;
using Cysharp.Threading.Tasks;

public class TextWindow : MonoBehaviour
{
    //�T�����̃e�L�X�g�E�B���h�E

    //�Q�[���}�l�[�W���[
    [SerializeField] private GameManager gameManager;

    //��b���̃J�����̊Ǘ�
    [SerializeField] private TalkCameraManager talkCameraManager;
    //�����̃I�u�W�F�N�g���Ǘ�
    RoomObjectManager roomObjectManager;

    [NonSerialized] public DialogueText dialogueText;
    [SerializeField, Header("�����\���E�B���h�E�̃I�u�W�F�N�g")] private GameObject roomObject;
    [SerializeField, Header("���ԕ\���E�B���h�E�̃I�u�W�F�N�g")] private GameObject timeObject;
    [SerializeField, Header("�e�L�X�g�E�B���h�E�̃I�u�W�F�N�g")] private GameObject panelObject;
    //�\������e�L�X�g�ɂ���ĕω�����e�L�X�g�E�B���h�E
    [SerializeField, Header("�e�L�X�g�E�B���h�E�̉摜")] private Sprite textWindowNormal;
    [SerializeField] private Sprite textWindowDark;

    [SerializeField, Header("�b��")] private TextMeshProUGUI speakerNameText;
    [SerializeField, Header("�e�L�X�g")] private TextMeshProUGUI speakerDialogueText;
    int index = 0;
    bool isTyping;

    private Coroutine dialogueCoroutine;

    //��b���s����l���L�����̃I�u�W�F�N�g�p�[�c
    [SerializeField, Header("���ׂ鎖�̂Ȃ���l���̃I�u�W�F�N�g")] private GameObject mainTalkChara;

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
    private float rectMoveSpeed = 0.05f;
    //�\���͈͕ω����e�l
    private double RectSetAllow = 0.0001;

    //���݂̃J���������ݒ�
    TalkCameraManager.CameraSet.CameraDivision currentCameraDivision = TalkCameraManager.CameraSet.CameraDivision.CenterOnly;

    //�q�I�u�W�F�N�g���w�肷�邽�߂�3�̃��@�[�`�����J�����ԍ�
    private const int vcamNumCenter = 2, vcamNumRight = 3, vcamNumLeft = 4;

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
            ProgressText();
            
        }
    }

    //3�̃J�����̕\���͈͂��ړ�����
    void TalkCameraRectMove()
    {
        if (currentCameraDivision == TalkCameraManager.CameraSet.CameraDivision.CenterOnly)
        {
            CameraEnabledOn();
            //��b�J�����\���͈͂̕ω��@�����E�E�E���̏��Ŏw��
            StartCoroutine(CameraRectMove(rect_current_Center, rect_current_Right, rect_CenterOnly_Left,
                                rect_CenterOnly_Center, rect_CenterOnly_Right, rect_CenterOnly_Left));
        }
        else if (currentCameraDivision == TalkCameraManager.CameraSet.CameraDivision.CenterAndRight)
        {
            CameraEnabledOn();
            //��b�J�����\���͈͂̕ω��@�����E�E�E���̏��Ŏw��
            StartCoroutine(CameraRectMove(rect_current_Center, rect_current_Right, rect_current_Left,
                                rect_CenteringRight_Center, rect_CenteringRight_Right, rect_CenteringRight_Left));
        }
        else if (currentCameraDivision == TalkCameraManager.CameraSet.CameraDivision.CenterAndLeft)
        {
            CameraEnabledOn();
            //��b�J�����\���͈͂̕ω��@�����E�E�E���̏��Ŏw��
            StartCoroutine(CameraRectMove(rect_current_Center, rect_current_Right, rect_current_Left,
                                rect_CenteringLeft_Center, rect_CenteringLeft_Right, rect_CenteringLeft_Left));
        }
        else if (currentCameraDivision == TalkCameraManager.CameraSet.CameraDivision.All)
        {
            CameraEnabledOn();
            //��b�J�����\���͈͂̕ω��@�����E�E�E���̏��Ŏw��
            StartCoroutine(CameraRectMove(rect_current_Center, rect_current_Right, rect_current_Left,
                                rect_All_Center, rect_All_Right, rect_All_Left));
        }
        else if(currentCameraDivision == TalkCameraManager.CameraSet.CameraDivision.None)
        {
            //��b�J��������
            CameraEnabledOff();
            Debug.Log("�J����enabled false");
            /*
            TaklCamera_1.enabled = false;
            TaklCamera_2.enabled = false;
            TaklCamera_3.enabled = false;
            */
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
            var mainChara = mainTalkChara.transform.GetChild(0).gameObject.transform.GetChild(0);
            var mainCharaShadow = mainTalkChara.transform.GetChild(1).gameObject.transform.GetChild(0);
            if (mainChara.GetComponent<Image>().enabled == false)
            { mainChara.GetComponent<Image>().enabled = true; mainCharaShadow.GetComponent<Image>().enabled = true; }

            //��b�p�J�����̋N��
            //CameraEnabledOn();
            //CameraEnabled();

            //�ŏ��̉�b�̕\��
            DisplayDialogueText();
        }

        //�J��������\���Ȃ�\������
        if (!TaklCamera_1.enabled)
        {
            /*
            Debug.Log("�J����enabled true");
            TaklCamera_1.enabled = true;
            TaklCamera_2.enabled = true;
            TaklCamera_3.enabled = true;
            */
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            //���̉�b�̕\��
            DisplayDialogueText();
        }
    }

    //��b���\������
    public async void DisplayDialogueText()
    {

        //scriptableObject�̏����p�l���ɕ\������
        if (dialogueText.textInfomations.Length > index)
        {
            //�e�L�X�g�ԍ��̃L�[���猻�݂̃J�����ݒ���擾
            var talkSet = talkSetDictionary[dialogueText.textinfo];
            var cameraSet = talkSet.cameraSet[index];

            //�\�����镶���̐F��ݒ�ɂ���ĕς���
            if (dialogueText.textInfomations[index].colorType == TextInfomation.TextColorType.Blue)
                speakerDialogueText.color = Color.cyan;
            else speakerDialogueText.color = Color.white;

            //�e�L�X�g�E�B���h�E�̐ݒ�ɂ���ĕ\����ω�
            WindowSpriteSetiing();

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
                //���ꂼ��̃J�����̕\���͈͂��w��̈ʒu�܂ňړ�����
                currentCameraDivision = cameraSet.camDivision;

                TalkCameraRectMove();
            }

            //�b�҂̖��O��\��
            speakerNameText.text = dialogueText.textInfomations[index].speakerName;

            //��b�C�x���g�𔭐�
            cameraSet.OnTalkEvent.Invoke();
            //StartCoroutine(NextTalkEvent(cameraSet));

            //���ȏЉ�֐��Ŏ��ȏЉ���t���O��True
            //True�̎���UniTask�őҋ@
            if (GameManager.isTalkPause)
            {
                //�ݒ肵���C�x���g����������܂őҋ@
                await UniTask.WaitUntil(() => GameManager.isTalkEvent);
            }

            if (!isTyping)
            {
                //��b�C�x���g�𔭐�
                //cameraSet.OnTalkEvent.Invoke();

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
            var mainChara = mainTalkChara.transform.GetChild(0).gameObject.transform.GetChild(0);
            var mainCharaShadow = mainTalkChara.transform.GetChild(1).gameObject.transform.GetChild(0);
            if (mainChara.GetComponent<Image>().enabled != false) 
            { 
                mainChara.GetComponent<Image>().enabled = false; mainCharaShadow.GetComponent<Image>().enabled = false; 
            }

            //��ʌ��ʂ̐؂�ւ�
            gameManager.SwitchDepthOfField(false);

            //��b�p�J�����̔�\��
            CameraEnabledOff();
            //CameraEnabled();

            //����UI��\��
            UIWindowActive(true);

            //��Ԃ����������ďƏ����[�h�ɂ���
            gameManager.playerController = GameManager.PlayerController.ReticleMode;

            index = 0;
            
            //�t���O������ꍇ���̕����̋c�_������
            if (gameManager.isDiscussionStart)
            {
                gameManager.isDiscussionStart = false;
                gameManager.DiscussionModeChange();
            }
        }
    }

    private IEnumerator NextTalkEvent(CameraSet cameraSet)
    {
        GameManager.isTalkEvent = false;
        //��b�C�x���g�𔭐�
        cameraSet.OnTalkEvent.Invoke();
        //��������̑O�Ɉ�莞�ԉ摜��\�����Ă����������C�x���g��ǉ�������

        //if(cameraSet.OnTalkEvent != null)

        yield return new WaitUntil(() => GameManager.isTalkEvent);

        if (!isTyping)
        {
            //��b�C�x���g�𔭐�
            //cameraSet.OnTalkEvent.Invoke();

            dialogueCoroutine = StartCoroutine(TypeDialogueText(dialogueText.textInfomations[index].paragraphs));
        }
        else
        {
            StopTyping();
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

    //��b�J�����̋N��
    public void CameraEnabledOn()
    {
        //��ʌ��ʂ̐؂�ւ�
        gameManager.SwitchDepthOfField(true);
        //���݂̃A�N�e�B�u��TRUE
        /*
        TaklCamera_1.enabled = !TaklCamera_1.enabled;
        TaklCamera_2.enabled = !TaklCamera_2.enabled;
        TaklCamera_3.enabled = !TaklCamera_3.enabled;
        */
        TaklCamera_1.enabled = true;
        TaklCamera_2.enabled = true;
        TaklCamera_3.enabled = true;
    }
    //��b�J�����̒�~
    public void CameraEnabledOff()
    {
        //��ʌ��ʂ̐؂�ւ�
        gameManager.SwitchDepthOfField(false);
        //���݂̃A�N�e�B�u��FALSE
        TaklCamera_1.enabled = false;
        TaklCamera_2.enabled = false;
        TaklCamera_3.enabled = false;
    }

    //�e�L�X�g�E�B���h�E�̐ݒ�ɂ���ĕ\����ω�
    private void WindowSpriteSetiing()
    {
        if (dialogueText.textInfomations[index].windowType == TextInfomation.TextWindowType.Normal)
        {
            panelObject.GetComponent<Image>().sprite = textWindowNormal;
            UIWindowActive(true);
        }
        else if (dialogueText.textInfomations[index].windowType == TextInfomation.TextWindowType.Dark)
        {
            panelObject.GetComponent<Image>().sprite = textWindowDark;
            UIWindowActive(true);
        }
        else if (dialogueText.textInfomations[index].windowType == TextInfomation.TextWindowType.Normal_NonUI)
        {
            panelObject.GetComponent<Image>().sprite = textWindowNormal;
            UIWindowActive(false);
        }
        else if (dialogueText.textInfomations[index].windowType == TextInfomation.TextWindowType.Dark_NonUI)
        {
            panelObject.GetComponent<Image>().sprite = textWindowDark;
            UIWindowActive(false);
        }
    }

    //�����Ǝ��Ԃ�UI��\���E��\��
    private void UIWindowActive(bool isChange)
    {
        if (isChange == true)
        {
            roomObject.SetActive(true);
            timeObject.SetActive(true);
        }
        else if (isChange == false)
        {
            roomObject.SetActive(false);
            timeObject.SetActive(false);
        }
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
