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

    [NonSerialized] public DialogueText dialogueText;
    [SerializeField] private GameObject panelObject;
    [SerializeField] private TextMeshProUGUI speakerNameText;
    [SerializeField] private TextMeshProUGUI speakerDialogueText;
    int index = 0;
    bool isTyping;

    private Coroutine dialogueCoroutine;


    //��b���s����l���L�����̃I�u�W�F�N�g�p�[�c
    [SerializeField] private GameObject mainTaklChara;
    [SerializeField] private Vector3 mainTaklCharaPos;
    GameObject mainChara;
    //�z�u�����l���̐e�I�u�W�F�N�g
    [SerializeField] private GameObject charaSetCanvas;


    //�J����
    [SerializeField] private Camera TaklCamera_1;
    [SerializeField] private Camera TaklCamera_2;
    [SerializeField] private Camera TaklCamera_3;

    

    private void Start()
    {
        

        TaklCamera_1.rect = new Rect(0.25f, 0.0f, 0.5f, 1f);//�^��
        TaklCamera_2.rect = new Rect(0.0f, 0.0f, 0.25f, 0.9f);//��
        TaklCamera_3.rect = new Rect(0.75f, 0.0f, 0.25f, 0.8f);//�E

        

    }

    void Update()
    {
        //�J�����e�X�g
        if (Input.GetKeyUp(KeyCode.R))
        {
            TaklCamera_1.rect = new Rect(0.25f, 0.0f, 0.5f, 1f);//�^��
            TaklCamera_2.rect = new Rect(0.0f, 0.0f, 0.25f, 0.9f);//��
            TaklCamera_3.rect = new Rect(0.75f, 0.0f, 0.25f, 0.8f);//�E
        }
        if (Input.GetKeyUp(KeyCode.T))
        {
            TaklCamera_1.rect = new Rect(0.15f, 0.0f, 0.5f, 1f);//�^��
            TaklCamera_2.rect = new Rect(-0.1f, 0.0f, 0.25f, 0.9f);//��ʊO
            TaklCamera_3.rect = new Rect(0.65f, 0.0f, 0.25f, 0.8f);//�E
        }
        


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

    /// <summary>
    /// ��b���\������
    /// </summary>
    public void displayDialogueText()
    {
        //�p�l������\���Ȃ�\������
        if (!panelObject.activeSelf)
        {
            panelObject.SetActive(true);


            Debug.Log("��l���̃I�u�W�F�N�g�z�u");
            // �z�u������W��ݒ�
            Vector3 placePosition = new Vector3(Camera.main.transform.position.x + mainTaklCharaPos.x,
                0+ mainTaklCharaPos.y, Camera.main.transform.position.z + mainTaklCharaPos.z);
            // �z�u�����]�p��ݒ�
            Quaternion quate = new Quaternion();
            quate = Quaternion.identity;
            //�e�I�u�W�F�N�g�ݒ�
            var parent = charaSetCanvas.transform;
            // �u���b�N�̕���
            mainChara = Instantiate(mainTaklChara, placePosition, quate, parent);
            //�V�l�}�V�[���ɐݒ�

            //

            //�J�����̒������ݒ�

            //��b�p�J�����̋N��
            CameraEnabled();


        }

        //scriptableObject�̏����p�l���ɕ\������
        if (dialogueText.textInfomations.Length > index)
        {
            

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
            if(mainChara != null) Destroy(mainChara);

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
