using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScenarioManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText; // ��b�e�L�X�g�\���p
    public GameObject dialoguePanel; // ��b�p�l��
    public Button option1Button; // �I�����{�^��1
    public Button option2Button; // �I�����{�^��2
    public Button option3Button; // �I�����{�^��3

    private Queue<DialogueNode> dialogueQueue; // ��b�m�[�h�̃L���[
    private bool isDialogueActive = false; // ��b���A�N�e�B�u���ǂ���

    void Start()
    {
        dialogueQueue = new Queue<DialogueNode>();
        dialoguePanel.SetActive(false);
        option1Button.onClick.AddListener(() => OnOptionSelected(1));
        option2Button.onClick.AddListener(() => OnOptionSelected(2));
        option3Button.onClick.AddListener(() => OnOptionSelected(3));
    }

    public void StartDialogue(DialogueNode startNode)
    {
        dialogueQueue.Clear();
        dialogueQueue.Enqueue(startNode);
        isDialogueActive = true;
        dialoguePanel.SetActive(true);
        DisplayNextDialogue();
    }

    public void DisplayNextDialogue()
    {
        if (dialogueQueue.Count == 0)
        {
            EndDialogue();
            return;
        }

        DialogueNode currentNode = dialogueQueue.Dequeue();
        dialogueText.text = currentNode.text;

        // �I�����̐ݒ�
        option1Button.gameObject.SetActive(currentNode.options.Count > 0);
        option2Button.gameObject.SetActive(currentNode.options.Count > 1);
        option3Button.gameObject.SetActive(currentNode.options.Count > 2);

        if (currentNode.options.Count > 0)
        {
            option1Button.GetComponentInChildren<Text>().text = currentNode.options[0].text;
        }
        if (currentNode.options.Count > 1)
        {
            option2Button.GetComponentInChildren<Text>().text = currentNode.options[1].text;
        }
        if (currentNode.options.Count > 2)
        {
            option3Button.GetComponentInChildren<Text>().text = currentNode.options[2].text;
        }
    }

    void OnOptionSelected(int optionIndex)
    {
        if (dialogueQueue.Count > 0)
        {
            DialogueNode currentNode = dialogueQueue.Peek();
            if (optionIndex - 1 < currentNode.options.Count)
            {
                var nextNode = currentNode.options[optionIndex - 1].nextNode;
                if (nextNode != null)
                {
                    dialogueQueue.Enqueue(nextNode);
                }
            }
        }

        DisplayNextDialogue();
    }

    void EndDialogue()
    {
        isDialogueActive = false;
        dialoguePanel.SetActive(false);
    }
}
