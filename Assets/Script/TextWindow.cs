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


    void Update()
    {
        //�e�L�X�g��\��
        if(gameManager.playerController == GameManager.PlayerController.TextWindowMode)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // ��b���\�����������s����
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
}
