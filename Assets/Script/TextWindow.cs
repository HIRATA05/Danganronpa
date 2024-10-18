using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextWindow : MonoBehaviour
{
    //�e�L�X�g�E�B���h�E

    [SerializeField] private TextMeshProUGUI text;

    [SerializeField] private DialogueText dialogueText;
    [SerializeField] private GameObject panelObject;
    [SerializeField] private TextMeshProUGUI speakerNameText;
    [SerializeField] private TextMeshProUGUI speakerDialogueText;
    int index = 0;
    bool isTyping;

    private Coroutine dialogueCoroutine;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)/*Input.GetMouseButtonDown(0)*/)
        {
            // ��b���\�����������s����
            displayDialogueText();
        }
    }

    /// <summary>
    /// ��b���\������
    /// </summary>
    public void displayDialogueText()
    {
        // �p�l������\���Ȃ�\������
        if (!panelObject.activeSelf)
        {
            panelObject.SetActive(true);
        }

        // scriptableObject�̏����p�l���ɕ\������
        speakerNameText.text = dialogueText.speakerName;
        if (dialogueText.paragraphs.Length > index)
        {
            if (!isTyping)
            {
                dialogueCoroutine = StartCoroutine(TypeDialogueText(dialogueText.paragraphs[index]));
            }
            else
            {
                stopTyping();
            }
        }
        else
        {
            // ��b���I���������߃p�l�����\���ɂ���
            speakerNameText.text = "";
            speakerDialogueText.text = "";
            panelObject.SetActive(false);
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
        index++; //���̉�b���C���f�b�N�X
    }

    /// <summary>
    /// 1�����Â\�����I�����e�L�X�g�S����\������
    /// </summary>
    private void stopTyping()
    {
        StopCoroutine(dialogueCoroutine);
        // ���\���̉�b����S���\������
        speakerDialogueText.text = dialogueText.paragraphs[index];
        isTyping = false;
        index++; //���̉�b���C���f�b�N�X
    }
}
