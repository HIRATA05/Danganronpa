using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScenarioManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText; // 会話テキスト表示用
    public GameObject dialoguePanel; // 会話パネル
    public Button option1Button; // 選択肢ボタン1
    public Button option2Button; // 選択肢ボタン2
    public Button option3Button; // 選択肢ボタン3

    private Queue<DialogueNode> dialogueQueue; // 会話ノードのキュー
    private bool isDialogueActive = false; // 会話がアクティブかどうか

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

        // 選択肢の設定
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
